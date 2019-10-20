open Swensen.Unquote

let euler0001() =
    let f x = x % 3 = 0 || x % 5 = 0
    seq { 1 .. 1000 - 1 } |> Seq.filter f |> Seq.sum

let euler0002() =
    let fib (a, b) =
        if a < 4_000_000 then
            Some(a, (b, a + b))
        else
            None
    Seq.unfold fib (1, 2) |> Seq.filter (fun x -> x % 2 = 0)
    |> Seq.sum

let funcs = dict [
    "1", euler0001;
    "2", euler0002;
]

[<Xunit.Fact>]
let testMain() =
    test <@ funcs.["1"]() = 233168 @>
    test <@ funcs.["2"]() = 4613732 @>

[<EntryPoint>]
let main argv =
    funcs.[Array.head argv]() |> printfn "%d"
    0