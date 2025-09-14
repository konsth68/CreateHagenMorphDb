namespace CreateHagenMorphDb

open System
open CreateHagenMorphDb.TagData

type MorphStr =
    | Lemma of string array   
    | Word of string array
    | NotCorrect

type Grammar =
    {
        CaseTag :int
        GenderTag :int
        NumberTag :int
        PersonTag :int
        PosTag :int
        TenseTag :int
        AnimalTag :int
        OverTag :int64
    }

type LemmaStr =
    {
        Word :string
        NotUsed :bool
        Grammar :Grammar
        AccentMain :int
        AccentSecond :int
        Frequency : float
        SignOfOnce : string
        Semantic : string
        HagenId :int64
    }

type WordStr =
    {
        Word :string
        NotUsed :bool
        Grammar :Grammar
        AccentMain :int
        AccentSecond :int
        HagenId :int64        
    }


type GrammarStrArr =
    {
        Pos :string
        Grmm :string array
    }

module ParseLine =
    
    let splitLine (str :string) =
        let s = str.Trim()
        let sArr = s.Split("|")
        match sArr.Length with
        | 4 -> Word(sArr)           
        | 7 -> Lemma(sArr)
        | _ -> NotCorrect
    
    let parseWord (str :string) =
        let s = str.Trim()
        if s[0] = '*' then
            s[1..]
        else
            s
              
    let parseNotUsed (str :string) =
        let s = str.Trim()
        s[0] = '*'


    let parseFrequency (str :string) =
        let s = str.Replace(",",".").Trim()
        match Double.TryParse(s) with
        | true, n -> n
        | false, _ -> 0.0
        
    let parseHagenId (str :string) =
        let s = str.Trim()
        match Int64.TryParse(s) with
        | true, n -> n
        | false, _ -> 0
    
    let parseAccentMain (str :string) =
        let s = str.Trim() 
        let iAccM = s.IndexOf("'")
        let iAccS = s.IndexOf("`")
        match (iAccM,iAccS) with
        | m,s  when m > 0 && s > 0 && m < s -> iAccM - 1 
        | m,s  when m > 0 && s > 0 && m > s -> iAccM - 2
        | m,s  when m > 0 && s < 0 -> iAccM - 1
        | _ -> -1    

    let parseAccentSecond (str :string) =
        let s = str.Trim() 
        let iAccM = s.IndexOf("'")
        let iAccS = s.IndexOf("`")
        match (iAccM,iAccS) with
        | m,s  when m > 0 && s > 0 && m < s -> iAccS - 2 
        | m,s  when m > 0 && s > 0 && m > s -> iAccS - 1
        | m,s  when s > 0 && m < 0 -> iAccS - 1
        | _ -> -1    

    let splitGrammar (str :string) =
        let s = str.Trim()
        let ga = s.Split(" ")
        if ga.Length > 0 then
            let gmaR :GrammarStrArr =
                {
                    Pos = ga[0]
                    Grmm = ga[1..]
                }
            gmaR
        else
            let gmaN :GrammarStrArr =
                {
                    Pos = ""
                    Grmm = [|""|]
                }
            gmaN
        
    let testMainGrammar (arr :MainTagDict array) (tag :string) =
        let t = tag.Trim().ToLower()
        let pos = arr
                  |> Array.tryFind (fun x -> x.Tag = t)
        match pos with
        | Some x -> x.Cod
        | None -> 0

    
    let testPos (tag :string) =
        testMainGrammar PosTagDataArr tag

    let testCase (tagArr :string array) =        
        tagArr
        |> Array.fold (fun acc tag -> acc + (testMainGrammar CaseTagDataArr tag)) 0

    let testGender (tagArr :string array) =        
        tagArr
        |> Array.fold (fun acc tag -> acc + (testMainGrammar GenderTagDataArr tag)) 0

    let testNumber (tagArr :string array) =        
        tagArr
        |> Array.fold (fun acc tag -> acc + (testMainGrammar NumberTagDataArr tag)) 0

    let testPerson (tagArr :string array) =        
        tagArr
        |> Array.fold (fun acc tag -> acc + (testMainGrammar PersonTagDataArr tag)) 0
                    
    let testTense (tagArr :string array) =        
        tagArr
        |> Array.fold (fun acc tag -> acc + (testMainGrammar TenseTagDataArr tag)) 0

    let testAnimal (tagArr :string array) =        
        tagArr
        |> Array.fold (fun acc tag -> acc + (testMainGrammar AnimalTagDataArr tag)) 0

                    
    let testOverGrammarOne (tag :string) =
        let t = tag.Trim().ToLower()
        let ovi = OverTagDataArr
                  |> Array.tryFind (fun x -> x.Tag = t)
        match ovi with
        | Some x -> x.IntVal
        | None -> 0L
    
    let testOverGrammarAll (tagArr :string array) =
        tagArr
        |> Array.fold (fun acc x -> acc + testOverGrammarOne x ) 0L
            
    let parseGrammar (gramm :string) =
        let gsa = splitGrammar gramm
        let gr :Grammar =
            {
                PosTag = testPos gsa.Pos
                CaseTag = testCase gsa.Grmm
                GenderTag = testGender gsa.Grmm
                NumberTag = testNumber gsa.Grmm
                PersonTag = testPerson gsa.Grmm
                TenseTag = testTense gsa.Grmm
                AnimalTag = testAnimal gsa.Grmm
                OverTag = testOverGrammarAll gsa.Grmm
            }
        gr
                
    let parseLemmaStr (sArr :string array) =
        let lm :LemmaStr =
            {
                Word = parseWord sArr[0]
                NotUsed = parseNotUsed sArr[0]
                Grammar = parseGrammar (sArr[1].Trim())
                AccentMain = parseAccentMain sArr[2]
                AccentSecond = parseAccentSecond sArr[2]
                Frequency = parseFrequency sArr[3]
                SignOfOnce = sArr[4].Trim()
                Semantic = sArr[5].Trim()
                HagenId = parseHagenId sArr[6]
            }
        lm
    
    let parseWordStr (sArr :string array) =
        let wr :WordStr =
            {
                Word = parseWord sArr[0]
                NotUsed = parseNotUsed sArr[0]
                Grammar = parseGrammar (sArr[1].Trim())
                AccentMain = parseAccentMain sArr[2]
                AccentSecond = parseAccentSecond sArr[2]
                HagenId = parseHagenId sArr[3]
            }
        wr
