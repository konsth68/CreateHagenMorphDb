namespace CreateHagenMorphDb

open System
open System.Security.Cryptography
open CreateHagenMorphDb.TagData
open System.Collections.Generic

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
        DispWord :string
        Type :TypeRec
        NotUsed :bool
        GramarStr :string
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
        DispWord :string
        Type :TypeRec
        NotUsed :bool
        GramarStr :string
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

type MorphData =
    | LemmaData of LemmaStr
    | WordData of WordStr
    | SubWordData of WordStr
    | EmptyString
    | Error
    

module ParseLine =
    
    let accented = dict [ ("а́","а"); ("я́","я"); ("у́","у"); ("ю́","ю"); ("о́","о"); ("е́","е"); ("э́","э"); ("и́","и"); ("ы́","ы");
                      ("А́","А"); ("Я́","Я"); ("У́","У"); ("Ю́","Ю"); ("О́","О"); ("Е́","Е"); ("Э́","Э"); ("И́","И") ]   

    let no_accented = dict [ ("а","а́"); ("я","я́"); ("у","у́"); ("ю","ю́"); ("о","о́"); ("е","е́"); ("э","э́"); ("и","и́"); ("ы","ы́");
                      ("А","А́"); ("Я","Я́"); ("У","У́"); ("Ю","Ю́"); ("О","О́"); ("Е","Е́"); ("Э","Э́"); ("И","И́") ]


    let setAccent (str :string) (acc :int)  = 
        let ar = str.ToCharArray()
        if acc < 0 ||  acc > str.Length  then
            str
        elif (ar[acc] = 'ё' || ar[acc] = 'Ё') then
            str
        else    
            let s = ar[acc].ToString()
            if no_accented.ContainsKey s then
                $"{System.String(ar[..acc-1])}{(no_accented[s])}{System.String(ar[acc+1..])}"
            else
                str

    let setAccented (str :string) (acm :int) (acs :int) =
        let s1 = setAccent str acm
        let s2 = setAccent s1 acs
        s2
        

    
    
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

    
    let parseDispWord (str :string) =
        let s = str.Trim()
        let acm = parseAccentMain s
        let acs = parseAccentSecond s
        let s1 = s.Replace("'","").Replace("`","")
        let s2 = setAccented s1 acm acs
        s2

    
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
                DispWord = parseDispWord sArr[2]
                Type = TypeRec.Lemma
                NotUsed = parseNotUsed sArr[0]
                GramarStr = sArr[1].Trim()
                Grammar = parseGrammar (sArr[1].Trim())
                AccentMain = parseAccentMain sArr[2]
                AccentSecond = parseAccentSecond sArr[2]
                Frequency = parseFrequency sArr[3]
                SignOfOnce = sArr[4].Trim()
                Semantic = sArr[5].Trim()
                HagenId = parseHagenId sArr[6]
            }
        lm
    
    let CheckWordSubword (subFl :bool) =
        if subFl = true then
            TypeRec.SubWord
        else
            TypeRec.Word
    
    let parseWordStr (sArr :string array) (subFl :bool) =
        let wr :WordStr =
            {
                Word = parseWord sArr[0]
                DispWord = parseDispWord sArr[2]
                Type = CheckWordSubword subFl
                NotUsed = parseNotUsed sArr[0]
                GramarStr = sArr[1].Trim()
                Grammar = parseGrammar (sArr[1].Trim())
                AccentMain = parseAccentMain sArr[2]
                AccentSecond = parseAccentSecond sArr[2]
                HagenId = parseHagenId sArr[3]
            }
        wr
    
    let testEmptyString (str :string) =
        String.IsNullOrEmpty str
    
    let testSubStr (str :string) =
        str[0] = ' ' &&  not (String.IsNullOrEmpty str)
    
    let parseString (str :string) =
        let subFl = testSubStr str            
        let empty = testEmptyString str
        
        let mrfStr = splitLine str
        match mrfStr,subFl,empty with
        | Lemma x,_,false  -> LemmaData(parseLemmaStr x)
        | Word x,false,false -> WordData(parseWordStr x subFl)
        | Word x,true,false ->  SubWordData(parseWordStr x subFl)
        | _,_,true -> EmptyString
        | NotCorrect,_,_ -> Error 