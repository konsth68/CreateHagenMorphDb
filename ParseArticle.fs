namespace CreateHagenMorphDb


open CreateHagenMorphDb.ParseLine

type DbMorph =
    | HagenRawRec of HagenRaw
    | EmptyStr


module ParseArticle =
    
    let convertLemmaToHagenRaw (data :LemmaStr) =
        let hr :HagenRaw =
            {
                Id = 0
                HagenId = data.HagenId
                LemmaId = 0
                UpMorphId = 0
                Type = int data.Type
                Word = data.Word
                DispWord = data.DispWord
                NotUsed = if data.NotUsed then 1 else 0
                GrammarStr = data.GramarStr
                Stem = ""
                AccentPosMain = data.AccentMain
                AccentPosSecond = data.AccentSecond
                PosTag = data.Grammar.PosTag
                GenderTag = data.Grammar.GenderTag
                NumberTag = data.Grammar.NumberTag
                CaseTag = data.Grammar.CaseTag
                TenseTag = data.Grammar.TenseTag
                PersonTag = data.Grammar.PersonTag
                AnimalTag = data.Grammar.AnimalTag
                OverTag = data.Grammar.OverTag
                Frequency = data.Frequency
                SignOfOnce = data.SignOfOnce
                Semantic = data.Semantic
            }
        hr

    let convertWordToHagenRaw (data :WordStr) (lemmaId :int64) =
        let hr :HagenRaw =
            {
                Id = 0
                HagenId = data.HagenId
                LemmaId = lemmaId
                UpMorphId = 0
                Type = int data.Type
                Word = data.Word
                DispWord = data.DispWord
                NotUsed = if data.NotUsed then 1 else 0
                GrammarStr = data.GramarStr
                Stem = ""
                AccentPosMain = data.AccentMain
                AccentPosSecond = data.AccentSecond
                PosTag = data.Grammar.PosTag
                GenderTag = data.Grammar.GenderTag
                NumberTag = data.Grammar.NumberTag
                CaseTag = data.Grammar.CaseTag
                TenseTag = data.Grammar.TenseTag
                PersonTag = data.Grammar.PersonTag
                AnimalTag = data.Grammar.AnimalTag
                OverTag = data.Grammar.OverTag
                Frequency = 0.0
                SignOfOnce = ""
                Semantic = ""
            }
        hr

    let convertSubWordToHagenRaw (data :WordStr) (lemmaId :int64) (wordId :int64) =
        let hr :HagenRaw =
            {
                Id = 0
                HagenId = data.HagenId
                LemmaId = lemmaId
                UpMorphId = if wordId = 0L then lemmaId else wordId
                Type = int data.Type
                Word = data.Word
                DispWord = data.DispWord
                NotUsed = if data.NotUsed then 1 else 0
                GrammarStr = data.GramarStr
                Stem = ""
                AccentPosMain = data.AccentMain
                AccentPosSecond = data.AccentSecond
                PosTag = data.Grammar.PosTag
                GenderTag = data.Grammar.GenderTag
                NumberTag = data.Grammar.NumberTag
                CaseTag = data.Grammar.CaseTag
                TenseTag = data.Grammar.TenseTag
                PersonTag = data.Grammar.PersonTag
                AnimalTag = data.Grammar.AnimalTag
                OverTag = data.Grammar.OverTag
                Frequency = 0.0
                SignOfOnce = ""
                Semantic = ""
            }
        hr
                    
    let dispatcherLine (strArr :string array) =
        
        let mutable curLemmaId = 0L
        let mutable curWordId = 0L
       
        
        let dbMorphSeq = seq {    
            for s in strArr do
                let md = parseString s
                let dm = match md with
                         | EmptyString -> 
                                          curLemmaId <- 0L
                                          curWordId <- 0L
                                          DbMorph.EmptyStr
                         | LemmaData x ->
                                          curLemmaId <- x.HagenId
                                          curWordId <- 0L
                                          DbMorph.HagenRawRec(convertLemmaToHagenRaw x)
                         | WordData x ->
                                          curWordId <- x.HagenId
                                          DbMorph.HagenRawRec(convertWordToHagenRaw x curLemmaId)                         
                         | SubWordData x -> DbMorph.HagenRawRec(convertSubWordToHagenRaw x curLemmaId curWordId)
                         | Error -> DbMorph.EmptyStr
                yield dm
        }
        
        dbMorphSeq  