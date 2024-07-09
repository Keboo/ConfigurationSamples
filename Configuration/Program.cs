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
        //config.AddInMemoryCollection(new Dictionary<string, string?>
        //{
        //    ["WebServices:ServiceUrl"] = "http://myurl.com"
        //});
    })
    .ConfigureServices(services =>
    {
        services.AddTransient<MyCoolConfigService>();

        services.AddOptions<WebServices>()
                //There is also .Configure
                .PostConfigure<IConfiguration, MyCoolConfigService>((webServices, configuration, myServices) =>
                {
                    webServices.ServiceUrl = new Uri(webServices.ServiceUrl.AbsoluteUri + myServices.GetValue());
                })
                .BindConfiguration(WebServices.SectionName)
                .ValidateDataAnnotations()
                .ValidateOnStart();

        services.AddOptions<WebServices>(WebServices.OtherSectionName)
                .BindConfiguration(WebServices.OtherSectionName)
                .ValidateDataAnnotations()
                .ValidateOnStart();
    })
    .Build();

//There is also IOptionsSnapshot, that recomputes on each DI resolution
IOptionsMonitor<WebServices> webServices = host.Services.GetRequiredService<IOptionsMonitor<WebServices>>();
do
{
    WebServices other = webServices.Get(WebServices.OtherSectionName);
    Console.WriteLine($"Service URL: {other.ServiceUrl}");

    Console.WriteLine("Press ESC to exit");
} while (Console.ReadKey().Key != ConsoleKey.Escape);



public sealed record WebServices
{
    public const string SectionName = "WebServices";
    public const string OtherSectionName = "OtherServices";

    [Required]
    public required Uri ServiceUrl { get; set; }
}

public class MyCoolConfigService
{
    public string GetValue() => "42";
}

[OptionsValidator]
public partial class WebServicesValidator : IValidateOptions<WebServices>
{
    
}