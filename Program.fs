namespace CreateHagenMorphDb

open System
open System.IO
open CreateHagenMorphDb.ParseLine
open CreateHagenMorphDb.TagData

module Program =
    
    let lemmaTestStr = "алфавит | сущ неод ед муж им | алфави'т | 4.778909 |  |  | 211"
    let wordTestStr = "алуштинских | прл мн род | алу'штинских | 4627364"
    
    let TestParse () =
        let s1 = splitLine lemmaTestStr
        let s2 = splitLine wordTestStr
        let s4 = splitLine ""
        let s3 = splitLine "jklsdfsjkl;dfjklds"
        let s5 = splitLine "aaa | nnn | ffff"
                        
        printfn "-- %A -- %A -- %A -- %A -- %A -- " s1 s2 s3 s4 s5
        
        let f1 = parseFrequency " 123.1 "
        let f2 = parseFrequency " 124,5 "
        let f3 = parseFrequency " 0"
        let f4 = parseFrequency ""
        let f5 = parseFrequency "dsaвфы23213    "

        printfn "-- %A -- %A -- %A -- %A -- %A -- " f1 f2 f3 f4 f5
        
    
        let d1 = parseHagenId "  1263333 "
        let d2 = parseHagenId " 215 "
        let d3 = parseHagenId " 0"
        let d4 = parseHagenId ""
        let d5 = parseHagenId "dsaвфы23213    "

        printfn "-- %A -- %A -- %A -- %A -- %A -- " d1 d2 d3 d4 d5
    
         
        let a1 = parseAccentMain " а-ля` фурше'т "
        let a2 = parseAccentSecond " а-ля` фурше'т "
            
        printfn " -- %A -- %A " a1 a2
         
        let a3 = parseAccentMain " а-ля' фурше`т "
        let a4 = parseAccentSecond " а-ля' фурше`т "
            
        printfn " -- %A -- %A " a3 a4

        let a5 = parseAccentMain " а-ля' фуршет "
        let a6 = parseAccentSecond " а-ля' фуршет "
            
        printfn " -- %A -- %A " a5 a6
        
        let a5 = parseAccentMain " а-ля` фуршет "
        let a6 = parseAccentSecond " а-ля` фуршет "
                        
        printfn " -- %A -- %A " a5 a6

        let a7 = parseAccentMain " а-ля фуршет "
        let a8 = parseAccentSecond " а-ля фуршет "
                        
        printfn " -- %A -- %A " a7 a8
        
    let TestGrammar () =
        
        let g1 = parseGrammar  " сущ неод ед муж им " 
        let g2 = parseGrammar  "  прл мн род " 
        let g3 = parseGrammar  "сущ неод мн род" 
        let g4 = parseGrammar  " прч несов непер наст ед муж род "
        
        printfn "%A" g1
        printfn "----------------------------------------"
        printfn "%A" g2
        printfn "----------------------------------------"
        printfn "%A" g3
        printfn "----------------------------------------"
        printfn "%A" g4
        printfn "----------------------------------------"
        
        
    [<EntryPoint>]
    let main argv =
        
        printfn "__START__"

        DapperDb.InitDb "HagenMorph.db"
                        
        //TestParse()
        
        TestGrammar ()
        
                            
        (*
        let curDir = Directory.GetCurrentDirectory()
        
        printfn $"{curDir}"
                
        //let db = curDir + "\\HagenMorph.db"        
                
        DapperDb.InitDb "HagenMorph.db"
        
        let resArr = fillTag
        
        for ra in resArr do
            printfn "--------------------"
            for r in ra do
                match r with
                | Some i -> printfn $"{i}"
                | None -> printfn "None"
        
        *)

        printfn "__END_"
        0 