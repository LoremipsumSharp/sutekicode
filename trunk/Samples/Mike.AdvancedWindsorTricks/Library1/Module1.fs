#light

type report = { id:int; title:string; body:string }

let myReportRepository id =
    { id = id; title = "Great Report"; body = "The report body" }

let myReportSender report =
    printfn "report = %A" report

let reporter (reportRepository:int -> report) (reportSender:report -> unit) (id:int) =
    let report = reportRepository id
    reportSender report
    
let myReporter = reporter myReportRepository myReportSender

myReporter 10