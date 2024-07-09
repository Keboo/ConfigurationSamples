// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Configuration;

ConfigurationBuilder builder = new();
builder.AddInMemoryCollection(new Dictionary<string, string?>
{
    ["serviceUrl"] = "http://myurl.com"
});

IConfigurationRoot configuration = builder.Build();

string? serviceUrl = configuration["serviceUrl"];
Console.WriteLine($"Service URL: {serviceUrl}");