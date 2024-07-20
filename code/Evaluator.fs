module Evaluator


open AST



let gen (count: int) (max: int) (r: System.Random): int list = List.init count (fun _ -> r.Next(minValue = 1, maxValue = max+1))


let roll (t: Term) (r: System.Random) (b: bool): int =
    if not b then
        if t.rollNum > 0 then gen t.rollNum t.sideNum r |> List.fold(fun total n -> total + n) 0
        else t.added
    else
        if t.rollNum > 0 then
            gen t.rollNum t.sideNum r |> List.fold(fun total n -> printf "%d + " n; total + n) 0
        else
            printf "%d + " t.added; t.added


// let rec calc (eq: Equation) (r: System.Random): int =
//     match eq with
//     | x::xs -> roll x r + calc xs r
//     | [] -> 0

let rec calc (ts: Term list) (r: System.Random) (b: bool): int =
    match ts with
    | x::xs -> roll x r b + calc xs r b
    | [] -> printf "\b\b  "; 0


let eval (eq: Equation): string = 
    let r = System.Random()
    $"\n{calc eq.content r eq.printflag}"