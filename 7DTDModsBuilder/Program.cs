using _7DTDModsBuilder.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json");

builder.Services.AddSingleton<IConfigurationOptionsLoader, ConfigurationOptionsLoader>();
builder.Services.AddSingleton<IModBuilder, ModBuilder>();

var app = builder.Build();

await app.StartAsync();
var modBuilder = app.Services.GetRequiredService<IModBuilder>();
modBuilder.Build();
await app.StopAsync();
