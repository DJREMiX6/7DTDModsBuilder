using _7DTDModsBuilder.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

var appLocation = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

builder.Configuration.AddJsonFile(Path.Combine(appLocation!, "appsettings.json"));

builder.Services.AddSingleton<IConfigurationOptionsLoader, ConfigurationOptionsLoader>();
builder.Services.AddSingleton<IModBuilder, ModBuilder>();

var app = builder.Build();

await app.StartAsync();
var modBuilder = app.Services.GetRequiredService<IModBuilder>();
modBuilder.Build();
await app.StopAsync();
