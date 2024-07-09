// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Configuration;

ConfigurationBuilder builder = new();

builder.AddInMemoryCollection(new Dictionary<string, string?>
{
    ["serviceUrl"] = "http://myurl.com"
});
builder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
builder.AddCommandLine(args);

IConfigurationRoot configuration = builder.Build();

do
{
    string? serviceUrl = configuration["serviceUrl"];
    Console.WriteLine($"Service URL: {serviceUrl}");

    Console.WriteLine("Press ESC to exit");
} while (Console.ReadKey().Key != ConsoleKey.Escape);