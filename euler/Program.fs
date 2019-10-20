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

let euler0003() =
    let x = 600851475143L
    // let x = 13195
    let upper = (sqrt >> ceil >> int) (double x)
    // printfn "x %d -> upper %d" x upper
    let arr = Array.zeroCreate upper
    for i in 2 .. upper - 1 do
        if not arr.[i] then
            let a = Seq.unfold (fun state -> if state < upper then Some(state, state + i) else None) (2 * i) |> Seq.toArray
            // printfn "%d: %A" i a
            Array.toSeq a |> Seq.iter (fun e -> arr.[e] <- true)
            if x % int64 i <> 0L then
                arr.[i] <- true
    Array.findIndexBack not arr

let parind x =
    let nDigits = log10 <| double x |> ceil |> int
                
    let get i =
        let a = pown 10 i
        let b = a * 10
        let q = x % b
        let r = q / a
        r
    seq { for i in 0 .. nDigits / 2 - (nDigits % 2) do yield i } |> Seq.forall (fun i -> get i = get (nDigits - i - 1))

let euler0004() =
    seq {
        for i in 2 .. 999 do
            for j in 1 .. i do
                let p = i * j
                if parind p then
                    yield p
    } |> Seq.max

let funcs = dict [
    "1", euler0001;
    "2", euler0002;
    "3", euler0003;
    "4", euler0004;
]

[<Xunit.Fact>]
let testMain() =
    test <@ funcs.["1"]() = 233168 @>
    test <@ funcs.["2"]() = 4613732 @>
    test <@ funcs.["3"]() = 6857 @>
    test <@ funcs.["4"]() = 906609 @>

[<EntryPoint>]
let main argv =
    let sw = System.Diagnostics.Stopwatch.StartNew()
    funcs.[Array.head argv]() |> printfn "%d"
    sw.Stop()
    printfn "%d ms" sw.ElapsedMilliseconds
    0