namespace CreateHagenMorphDb

open CreateHagenMorphDb.DbModel
open System.Collections.Generic

module HagenRawDb =

    let CreateDb (hagenRawSeq :DbMorph seq) =
        let r = ClearTable "HagenRaw"
        if r >= 0 then
            hagenRawSeq
            |> Seq.choose (fun x -> 
                                    match x with
                                        | HagenRawRec hr -> Some (insertHagenRaw hr)
                                        | EmptyStr -> None
                                        )
        else
            [|None|]

    let CreateSqlFileDb (hagenRawSeq :DbMorph seq) (filePath :string) =       
        use sw = new System.IO.StreamWriter(filePath, false, System.Text.Encoding.UTF8)
        hagenRawSeq
        |> Seq.iter (fun x -> 
                        match x with
                        | HagenRawRec hr -> sw.WriteLine (hagenRawInsertString hr)
                        | EmptyStr -> ()
                        )
        sw.Close()

    let CreateCsvFileDb (hagenRawSeq :DbMorph seq) (filePath :string) =       
        use sw = new System.IO.StreamWriter(filePath, false, System.Text.Encoding.UTF8)
        sw.WriteLine "Id;HagenId;LemmaId;UpMorphId;Type;NotUsed;Word;DispWord;GrammarStr;Stem;AccentPosMain;AccentPosSecond;PosTag;GenderTag;NumberTag;CaseTag;TenseTag;PersonTag;AnimalTag;OverTag;Frequency;SignOfOnce;Semantic"
        hagenRawSeq
        |> Seq.iteri (fun i x -> 
                        match x with
                        | HagenRawRec hr -> sw.WriteLine
                                                $"{i};{hr.HagenId};{hr.LemmaId};{hr.UpMorphId};{hr.Type};{hr.NotUsed};{hr.Word};{hr.DispWord};{hr.GrammarStr};{hr.Stem};{hr.AccentPosMain};{hr.AccentPosSecond};{hr.PosTag};{hr.GenderTag};{hr.NumberTag};{hr.CaseTag};{hr.TenseTag};{hr.PersonTag};{hr.AnimalTag};{hr.OverTag};{hr.Frequency};{hr.SignOfOnce};{hr.Semantic}"
                        | EmptyStr -> ()
                        )
        sw.Close()
        
    let createDict (hagenRawSeq :DbMorph seq) (filePath :string) =
        let d :Dictionary<int64,HagenRaw> = Dictionary<int64,HagenRaw>()        
        hagenRawSeq
            |> Seq.iter(fun x ->
                        match x with
                        | HagenRawRec hr -> d.Add(hr.HagenId,hr)
                        | EmptyStr -> ()
                        )
        d