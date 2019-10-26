open Xunit
open Swensen.Unquote

////////////////////////////////////////////////////////////////////////////////

type Util =
    static member Range(init: int32, ?step: int32, ?term: int32) =
        let step = defaultArg step 1
        Seq.initInfinite (((*) step) >> ((+) init))
        |> match term with Some(t) -> Seq.takeWhile ((>) t) | None -> id

////////////////////////////////////////////////////////////////////////////////

type TestHalfRangeData() as this =
    inherit TheoryData<int>()
    do for n in -3 .. 3 do
        this.Add(n)

[<Theory>]
[<ClassData(typeof<TestHalfRangeData>)>]
let testHalfRange n =
    let m = 5
    Util.Range(n) |> Seq.take m |> Seq.toList =! (seq { for i in n .. n + m - 1 -> i } |> Seq.toList)

type TestRangeData() as this =
    inherit TheoryData<int, int>()
    do for m in -3 .. 6 do
        for n in -3 .. 3 do
            this.Add(m, n)

[<Theory>]
[<ClassData(typeof<TestRangeData>)>]
let testRange m n =
    Util.Range(m, term=n) |> Seq.toList =! (seq { for i in m .. n - 1 -> i } |> Seq.toList)

////////////////////////////////////////////////////////////////////////////////

module Seq =
    let tryTake n s = Seq.indexed s |> Seq.takeWhile (fun (i, x) -> i < n) |> Seq.map (fun (i, x) -> x)

////////////////////////////////////////////////////////////////////////////////

type TestTryTakeData() as this =
    inherit TheoryData<int, int>()
    do for m in 0 .. 3 do
        for n in -2 .. 5 do
             this.Add(m, n)

[<Theory>]
[<ClassData(typeof<TestTryTakeData>)>]
let testTryTake m n =
    Seq.init m id |> Seq.tryTake n |> Seq.toList =! List.init (max 0 (min m n)) id

////////////////////////////////////////////////////////////////////////////////

let euler0001() =
    let f x = x % 3 = 0 || x % 5 = 0
    seq { 1 .. 1000 - 1 } |> Seq.filter f |> Seq.sum

let euler0002() =
    let rec fib a b sum =
        if a < 4_000_000 then
            fib b (a + b) (sum + if a % 2 = 0 then a else 0)
        else
            sum
    fib 1 2 0

////////////////////////////////////////////////////////////////////////////////

let eratos32 n =
    let arr = Array.create (n + 1) true
    arr.[0] <- false
    arr.[1] <- false
    for i in 2 .. n do
        if arr.[i] then
            for e in Seq.initInfinite (((+) 2) >> ((*) i)) |> Seq.takeWhile ((>=) n) do
                arr.[e] <- false
    arr

////////////////////////////////////////////////////////////////////////////////

type TestEratos32Data() as this =
    inherit TheoryData<bool[]>()
    do for lst in [
                    [|false; false; true|]
                    [|false; false; true; true|]
                    [|false; false; true; true; false|]
                    [|false; false; true; true; false; true|]
                    [|false; false; true; true; false; true; false|]
                    [|false; false; true; true; false; true; false; true|]
                    [|false; false; true; true; false; true; false; true; false|]
                    [|false; false; true; true; false; true; false; true; false; false|]
                    [|false; false; true; true; false; true; false; true; false; false; false|]
                    ] do
        this.Add(lst)

[<Theory>]
[<ClassData(typeof<TestEratos32Data>)>]
let testEratos32 (lst: bool[]) =
    eratos32 (lst.Length - 1) =! lst

////////////////////////////////////////////////////////////////////////////////

let euler0003() =
    let x = 600851475143L
    // let x = 13195L
    let arr = (sqrt >> ceil >> int32) (double x) |> eratos32
    // printfn "%A" arr
    for i in 1 .. arr.Length - 1 do
        let ii = int64 i
        if x / ii * ii <> x then
            arr.[i] <- false
    // printfn "%A" arr
    Array.findIndexBack id arr

////////////////////////////////////////////////////////////////////////////////

let nDigits x =
    let rec loop k d =
        if x < d then
            k
        else
            loop (k + 1) (10 * d)
    loop 1 10

type TestNDigitsData() as this =
    inherit TheoryData<int, int>()
    do for n, d in [
                    0, 1
                    1, 1
                    2, 1
                    3, 1
                    9, 1
                    10, 2
                    11, 2
                    12, 2
                    99, 2
                    100, 3
                    101, 3
                    102, 3
                    999, 3
                    1000, 4
                    1001, 4
                    1002, 4
                    9999, 4
                    ] do
        this.Add(n, d)

[<Theory>]
[<ClassData(typeof<TestNDigitsData>)>]
let testNDigits n d =
    nDigits n =! d

////////////////////////////////////////////////////////////////////////////////

let parind x =
    let d = nDigits x

    let get i =
        let a = pown 10 i
        let b = a * 10
        let q = x % b
        let r = q / a
        r

    seq { for i in 0 .. d / 2 -> i } |> Seq.forall (fun i -> get i = get (d - i - 1))

////////////////////////////////////////////////////////////////////////////////

type TestParidData() as this =
    inherit TheoryData<int, bool>()
    do for n, b in [
                    1, true
                    2, true
                    3, true
                    10, false
                    11, true
                    12, false
                    100, false
                    101, true
                    102, false
                    110, false
                    111, true
                    112, false
                    1000, false
                    1001, true
                    1002, false
                    1010, false
                    1100, false
                    1101, false
                    1111, true
                    ] do
        this.Add(n, b)

[<Theory>]
[<ClassData(typeof<TestParidData>)>]
let testParind n b =
    parind n =! b

////////////////////////////////////////////////////////////////////////////////

let euler0004() =
    seq {
        for i in 2 .. 999 do
            for j in 1 .. i do
                let p = i * j
                if parind p then
                    yield p
    } |> Seq.max

////////////////////////////////////////////////////////////////////////////////

[<Fact>]
let testMain() =
    euler0001() =! 233168
    euler0002() =! 4613732
    euler0003() =! 6857
    euler0004() =! 906609

[<EntryPoint>]
let main argv =
    let sw = System.Diagnostics.Stopwatch.StartNew()
    let funcs = dict [
        "1", euler0001
        "2", euler0002
        "3", euler0003
        "4", euler0004
    ]
    funcs.[Array.head argv]() |> printfn "%d"
    sw.Stop()
    printfn "%d ms" sw.ElapsedMilliseconds
    0
