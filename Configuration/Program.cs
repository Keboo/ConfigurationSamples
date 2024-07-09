// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Configuration;

ConfigurationBuilder builder = new();

builder.AddInMemoryCollection(new Dictionary<string, string?>
{
    ["WebServices:serviceUrl"] = "http://myurl.com"
});
builder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
builder.AddCommandLine(args);

IConfiguration configuration = builder.Build();

do
{
    WebServices? webServices = configuration.GetSection("WebServices").Get<WebServices>();

    Console.WriteLine($"Service URL: {webServices?.ServiceUrl}");

    Console.WriteLine("Press ESC to exit");
} while (Console.ReadKey().Key != ConsoleKey.Escape);



public sealed record WebServices(Uri ServiceUrl);