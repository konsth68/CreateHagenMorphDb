namespace  CreateHagenMorphDb

open ParseLine
open ParseArticle
open ParseFile
    
module Test =

    let lemmaTestStr = "алфавит | сущ неод ед муж им | алфави'т | 4.778909 |  |  | 211"
    let wordTestStr = "алуштинских | прл мн род | алу'штинских | 4627364"
    
    let minFileTestArr :string array = [|
        "аббатский | прл ед муж им | абба'тский | 0.043484 |  |  | 100272"
        "  аббатского | прл ед муж род | абба'тского | 455549"
        "  аббатскому | прл ед муж дат | абба'тскому | 455550"
        "  аббатского | прл ед муж вин одуш | абба'тского | 455554"
        "  аббатский | прл ед муж вин неод | абба'тский | 455551"
        "  аббатским | прл ед муж тв | абба'тским | 455552"
        "  аббатском | прл ед муж пр | абба'тском | 455553"
        "аббатская | прл ед жен им | абба'тская | 618260"
        "  аббатской | прл ед жен род | абба'тской | 618261"
        "  аббатской | прл ед жен дат | абба'тской | 618262"
        "  аббатскую | прл ед жен вин | абба'тскую | 618263"
        "  аббатскою | прл ед жен тв | абба'тскою | 618266"
        "  аббатской | прл ед жен тв | абба'тской | 618264"
        "  аббатской | прл ед жен пр | абба'тской | 618265"
        "аббатское | прл ед ср им | абба'тское | 823177"
        "  аббатского | прл ед ср род | абба'тского | 823178"
        "  аббатскому | прл ед ср дат | абба'тскому | 823179"
        "  аббатское | прл ед ср вин | абба'тское | 823180"
        "  аббатским | прл ед ср тв | абба'тским | 823181"
        "  аббатском | прл ед ср пр | абба'тском | 823182"
        "аббатские | прл мн им | абба'тские | 1033077"
        "  аббатских | прл мн род | абба'тских | 1033078"
        "  аббатским | прл мн дат | абба'тским | 1033079"
        "  аббатские | прл мн вин неод | абба'тские | 1033080"
        "  аббатских | прл мн вин одуш | абба'тских | 1033083"
        "  аббатскими | прл мн тв | абба'тскими | 1033081"
        "  аббатских | прл мн пр | абба'тских | 1033082"
        "                                                " 
        "аббатство | сущ неод ед ср им | абба'тство | 1.478461 |  |  | 99856"
        "  аббатства | сущ неод ед ср род | абба'тства | 99857"
        "  аббатству | сущ неод ед ср дат | абба'тству | 99858"
        "  аббатство | сущ неод ед ср вин | абба'тство | 99859"
        "  аббатством | сущ неод ед ср тв | абба'тством | 99860"
        "  аббатстве | сущ неод ед ср пр | абба'тстве | 99861"
        "аббатства | сущ неод мн им | абба'тства | 1511480"
        "  аббатств | сущ неод мн род | абба'тств | 1511481"
        "  аббатствам | сущ неод мн дат | абба'тствам | 1511482"
        "  аббатства | сущ неод мн вин | абба'тства | 1511483"
        "  аббатствами | сущ неод мн тв | абба'тствами | 1511484"
        "  аббатствах | сущ неод мн пр | абба'тствах | 1511485"
    |]
    
    
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
    
    let TestDispatcherLine () =
    
        let hr1 = dispatcherLine minFileTestArr
        
        for h in hr1 do
            printfn "%A" h
        
    let TestDispatcherLineForId () =
    
        let hr1 = dispatcherLine minFileTestArr
        
        for h in hr1 do
            printfn "----------------------------"
            match h with
            | EmptyStr -> printfn "-- Empty String --"
            | HagenRawRec x -> printfn $"Type = {x.Type}"
                               printfn $"HagenId = {x.HagenId}"
                               printfn $"\tLemmaId = {x.LemmaId}"
                               printfn $"\t\tWordId = {x.UpMorphId}"
            printfn "------------------------------"

    let TestParseFile =
        
        let dataStrArr = ReadDataFile () 
        
        printfn $"{dataStrArr.Length}"
