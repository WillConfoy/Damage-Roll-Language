open Parser
open Evaluator
open Combinator
open System.IO


[<EntryPoint>]

let main (args: string[]): int =
  try
    match args.Length with
        | 1 ->
            let file = args[0]
            let text = File.ReadAllText file
            let asto = parse text
            match asto with
                | Some ast ->
                    printfn "%s" (prettyprint ast)
                    printfn "%s" (eval ast)
                    0
                | None -> 0
        | _ ->
            printfn "Must include path to a program"
            1
  with
  | e -> printfn $"Error thrown! \n\n{e.Message}"; 1