using System.Runtime.InteropServices;
using System.Text.Encodings.Web;
using System.Text.Json;

string student = "Старух Даніїл, ФЕІ-35";
string osDescription = RuntimeInformation.OSDescription;
string osEnvironment = Environment.OSVersion.ToString();
string processArchitecture = RuntimeInformation.ProcessArchitecture.ToString();
string dotNetVersion = Environment.Version.ToString();
string runtime = RuntimeInformation.FrameworkDescription;
string appDirectory = AppContext.BaseDirectory;
string currentDirectory = Environment.CurrentDirectory;
string domain = "Бібліотека";

if (args.Contains("--json"))
{
    var info = new
    {
        Student = student,
        OSDescription = osDescription,
        OSEnvironment = osEnvironment,
        ProcessArchitecture = processArchitecture,
        DotNetVersion = dotNetVersion,
        Runtime = runtime,
        AppDirectory = appDirectory,
        CurrentDirectory = currentDirectory,
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

    Console.WriteLine($"ОС (OSDescription)      : {osDescription}");
    Console.WriteLine($"ОС (Environment)        : {osEnvironment}");
    Console.WriteLine($"Архітектура процесу     : {processArchitecture}");
    Console.WriteLine($"Версія .NET (CLR)       : {dotNetVersion}");
    Console.WriteLine($"Runtime                 : {runtime}");
    Console.WriteLine($"Каталог застосунку      : {appDirectory}");
    Console.WriteLine($"Поточний каталог        : {currentDirectory}");

    Console.WriteLine(new string('-', 52));
    Console.WriteLine($"Предметна область: {domain}");
}