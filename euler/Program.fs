open Swensen.Unquote

let euler0001() =
    let f x = x % 3 = 0 || x % 5 = 0
    seq { 1 .. 1000 - 1 } |> Seq.filter f |> Seq.sum

[<Xunit.Fact>]
let testMain() =
    test <@ euler0001() = 233168 @>

[<EntryPoint>]
let main argv =
    match Array.head argv with
    | "1" -> euler0001()
    | bad -> failwith <| sprintf "bad selection %s" bad
    |> printfn "%d"
    0