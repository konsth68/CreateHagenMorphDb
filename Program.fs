namespace CreateHagenMorphDb

open System
open System.IO
open CreateHagenMorphDb.HagenRawDb
open ParseFile
open ParseLine
open TagDataFunc
open ParseArticle

module Program =

    let prinntProblemOperation (res :int option array) =
        for i in res do                
            match i with
            | Some i -> ()
            | None -> printfn "Problem insert"
            
        
                   
    [<EntryPoint>]
    let main argv =
        
        printfn "__START__"

        DapperDb.InitDb "Db\\HagenMorph.db"
                                                   
        let curDir = Directory.GetCurrentDirectory()
        
        printfn $"{curDir}"
                        
        let resTagArr = fillTag
        
        let dt = ReadDataFile ()
        
        let hr = dispatcherLine dt
        
        let resHagenSeq = CreateDb hr
        
        printfn "Dictionary Add :"
        for ra in resTagArr do
            printfn "--------------------"
            prinntProblemOperation ra        
        
        let HrReaArr :int option array = Seq.toArray resHagenSeq
        
        printfn "Hagen Raw Add :"
        prinntProblemOperation HrReaArr
        
        
        printfn "__END__"
        0 