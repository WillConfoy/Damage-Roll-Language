module Parser

open Combinator
open AST



// let psign: Parser<char> = pchar '+' <|> pchar '-' <|> pchar '*' <|> pchar '/'
let psign = pchar '+'

let pad p = pbetween pws0 p pws0

let pnum: Parser<int> = pmany1 pdigit |>> (fun ds -> stringify ds |> int)

let pterm: Parser<Term> = 
    pseq
        (pnum)
        (pright (pchar 'd') pnum)
        (fun (x,y) -> 
            {rollNum = x; sideNum = y; added = 0})

let pnumterm: Parser<Term> = 
    pseq
        (pmany0 (pchar 'p'))
        (pnum)
        (fun (_,x) -> {rollNum = -1; sideNum = -1; added = x})
    // {rollNum = 1; sideNum = 1; added = y}

let psignterm = 
    pleft
        (pad (pterm <|> pnumterm))
        (pad psign)
        // (fun x -> x)

let pflag = pstr "PRINTALL"

let expr = 
    pseq
        (pmany0 psignterm)
        // (pseq
        (pad (pterm <|> pnumterm))
            // (psat (fun c -> c <> '!'))
            // (fun (x,b) -> (x,b)))
        (fun (xs, x) -> 
            let xs = xs |> List.rev
            let content = x::xs |> List.rev
            {content = content;
             printflag = true;
            })

let grammar = pleft expr peof

let parse (s: string): Equation option =
    let input = prepare s
    match grammar input with
    | Success(ast, _) -> Some ast
    | Failure(pos, rule) ->
        printfn "Invalid Expression"
        let msg =
            sprintf "Cannot parse input at position %d in rule '%s':" pos rule
        let diag = diagnosticMessage 20 (pos - 1) s msg
        printf "%s" diag
        None

let prettyprint (eq: Equation): string =
    System.String.Join(separator = " + ", values = List.map (fun t-> 
        if t.rollNum > 0 then $"{t.rollNum}d{t.sideNum}" else $"{t.added}") eq.content)