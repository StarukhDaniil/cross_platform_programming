using System.Text.Encodings.Web;
using System.Text.Json;
using Core;

string student = "Старух Даніїл, ФЕІ-35";
string domain = "Бібліотека";

var report = EnvironmentInfo.Collect();

Console.WriteLine($"Build Note: {EnvironmentInfo.BuildNote}");

if (args.Contains("--json"))
{
    var info = new
    {
        Student = student,
        report.OsDescription,
        report.OsEnvironment,
        report.ProcessArchitecture,
        report.DotNetVersion,
        report.FrameworkDescription,
        report.BaseDirectory,
        report.DetectedRid,
        report.ReportedRid,
        Domain = domain
    };

    var options = new JsonSerializerOptions
    {
        WriteIndented = true,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };

    Console.WriteLine(JsonSerializer.Serialize(info, options));
}
else
{
    Console.WriteLine("CrossApp – практикум з крос-платформного програмування");
    Console.WriteLine($"Студент: {student}");
    Console.WriteLine(new string('-', 52));

    Console.WriteLine($"ОС (OSDescription)      : {report.OsDescription}");
    Console.WriteLine($"ОС (Environment)        : {report.OsEnvironment}");
    Console.WriteLine($"Архітектура процесу     : {report.ProcessArchitecture}");
    Console.WriteLine($"Версія .NET (CLR)       : {report.DotNetVersion}");
    Console.WriteLine($"Runtime                 : {report.FrameworkDescription}");
    Console.WriteLine($"Каталог застосунку      : {report.BaseDirectory}");

    Console.WriteLine(new string('-', 52));
    Console.WriteLine($"Предметна область: {domain}");
}