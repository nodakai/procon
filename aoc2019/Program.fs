﻿open System

let calc1 x = x / 3 - 2

let test1() =
    printfn "%d %d" (calc1 1969) (calc1 100756)

let solve1() =
    IO.File.ReadAllLines "1.in.txt" |> Seq.map int |> Seq.sumBy calc1

let rec calc2 tot f =
    if f <= 6 then tot + f
    else
    let ff = calc1 f
    calc2 (tot + f) ff

let test1'() =
    printfn "%d %d" (calc1 1969 |> calc2 0) (calc1 100756 |> calc2 0)

let solve1'() =
    IO.File.ReadAllLines "1.in.txt" |> Seq.map int |> Seq.sumBy (calc1 >> calc2 0)

let solve2() =
    // use strm = new IO.StreamReader(1y)
    0

let test2() =
    ()

let printHelp() =
    printfn "Program (-h | --help)"
    printfn "    Print this help"
    printfn "Program dl <num>"
    printfn "    Download the input data for Quiz <num>"
    printfn "Program test <num>"
    printfn "    Run some tests for Quiz <num>"
    printfn "Program <num>"
    printfn "    Solve Quiz <num>"

type System.Threading.Tasks.Task with
    member this.Await() = this |> Async.AwaitTask

type Microsoft.FSharp.Control.AsyncBuilder with
    member x.Bind(t: Threading.Tasks.Task<'T>, f:'T -> Async<'R>)  = 
        async.Bind(Async.AwaitTask t, f)
    member x.ReturnFrom(t: Threading.Tasks.Task<'T>)  = 
        async.ReturnFrom(Async.AwaitTask t)

let download n =
    use hdl = new Net.Http.HttpClientHandler()
    hdl.UseCookies <- false
    use cli = new Net.Http.HttpClient(hdl)
    let uri = Uri(sprintf "https://adventofcode.com/2019/day/%s/input" n)
    use msg = new Net.Http.HttpRequestMessage(Net.Http.HttpMethod.Get, uri)
    let txt =
        async {
            let sess =
                let sessFname = "session.txt"
                try IO.File.ReadAllText(sessFname)
                with :? IO.IOException -> failwithf "failed to read the session cookie file %s" sessFname
            msg.Headers.Add("cookie", sprintf "session=%s" sess)
            let! resp = cli.SendAsync(msg)
            if resp.StatusCode <> Net.HttpStatusCode.OK then
                failwithf "%A %s => %A" uri sess resp
            return! resp.Content.ReadAsStringAsync()
        } |> Async.RunSynchronously
    let fname = sprintf "%s.in.txt" n
    IO.File.WriteAllText(fname, txt)

type Op(solve: (unit -> int), test: (unit -> unit)) =
    member _.Solve = solve
    member _.Test = test
    
[<EntryPoint>]
let main argv =
    let ops = dict[
        "1", Op(solve1, test1)
        "1+", Op(solve1', test1')
        "2", Op(solve2, test2)
    ]

    match argv with
    | [|"-h"|]
    | [|"--help"|] -> printHelp()
    | [|n|] ->
        let ok, found = ops.TryGetValue n
        if ok then
            found.Solve() |> printfn "%d"
        else
            failwithf "problem #%s not solved yet" n
    | [|"dl"; n|] -> download n
    | [|"test"; n|] ->
        let ok, found = ops.TryGetValue n
        if ok then
            found.Test()
        else
            failwithf "problem #%s not solved yet" n

    | _ -> failwithf "failed to parse: %A" argv
    0
