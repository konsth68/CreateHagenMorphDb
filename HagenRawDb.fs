namespace CreateHagenMorphDb

open CreateHagenMorphDb.DbModel

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
