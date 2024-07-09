// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;



using IHost host = Host.CreateDefaultBuilder()
    .ConfigureHostConfiguration(config =>
    {
        //IConfiguration for the host itself
    })
    .ConfigureAppConfiguration((hostingContext, config) =>
    {
        config.AddInMemoryCollection(new Dictionary<string, string?>
        {
            ["WebServices:ServiceUrl"] = "http://myurl.com"
        });
    })
    .ConfigureServices(services =>
    {
        services.AddOptions<WebServices>()
                .BindConfiguration(WebServices.SectionName)
                .ValidateDataAnnotations()
                .ValidateOnStart();
    })
    .Build();

do
{
    IOptions<WebServices> webServices = host.Services.GetRequiredService<IOptions<WebServices>>();

    Console.WriteLine($"Service URL: {webServices.Value.ServiceUrl}");

    Console.WriteLine("Press ESC to exit");
} while (Console.ReadKey().Key != ConsoleKey.Escape);



public sealed record WebServices
{
    public const string SectionName = "WebServices";

    [Required]
    public required Uri ServiceUrl { get; set; }
}