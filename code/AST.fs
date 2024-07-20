module AST


// Add more operators later
// type Operator = 
//     | Add
//     | Multiply
//     | Subtract
//     | Divide


// type Roll = {
//     num: int;
//     numSides: int;
// }

// type Term = {
//     left: Roll;
//     sign: Operator;
//     right: Roll;
// }

type Term = {
    rollNum: int;
    sideNum: int;
    added: int;
}

// type Equation =
//     | Eq of Term
//     | Single of Roll

type Equation = {
    content: Term list;
    printflag: bool;
}