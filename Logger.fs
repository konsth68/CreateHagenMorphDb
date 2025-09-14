namespace CreateHagenMorphDb

open Serilog

[<RequireQualifiedAccess>]
module Log =

    let loggerConfig =
        LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.File(".\ParseFranDict.log")
            .CreateLogger()

    
    let Logger = loggerConfig
