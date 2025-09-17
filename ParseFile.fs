namespace CreateHagenMorphDb

open System.IO
open System.IO.Compression

module ParseFile =

    let defaultDataFile = "hagen.txt"
    let defaultZipFile = "\\Data\\hagen.zip"    
    let mutable dataFile = defaultDataFile 
    let mutable zipFile = defaultZipFile
    
    
    let createTmpDirectory () =
        let curDir = Directory.GetCurrentDirectory()
        let tmpDir = curDir + "\\Tmp"
        
        if Directory.Exists tmpDir then
            tmpDir
        else
            Directory.CreateDirectory(tmpDir) |> ignore
            tmpDir
    
    let deleteTmpDirectory (dir :string) =
        if Directory.Exists dir then
            Directory.Delete(dir)
    
    let deleteAllFileInTmpDir (dir :string) =
        let files = Directory.GetFiles(dir)
        for filePath in files do
            File.Delete(filePath)
    
    let unzipFile (tmpDir :string) (zipFile :string) =
         let curDir = Directory.GetCurrentDirectory()
         let zFile = curDir + zipFile
         let dFile = tmpDir + "\\" + dataFile
         
         if File.Exists zFile then
            ZipFile.ExtractToDirectory(zFile, tmpDir)
         else
             failwith $"Not found zip file = {zFile}"
         
         if not (File.Exists dFile) then
            failwith $"Not found data file = {dFile}"
            
         dFile
         
         
    let ReadDataFile () =
        let tmpdir = createTmpDirectory ()
        
        let df = unzipFile tmpdir zipFile
        
        let HagenData = File.ReadAllLines df
              
        deleteAllFileInTmpDir tmpdir
        deleteTmpDirectory tmpdir
        
        HagenData
