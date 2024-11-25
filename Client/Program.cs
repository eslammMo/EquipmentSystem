global using Microsoft.AspNetCore.Components.Authorization;
using EquipmentsSystem.Client;
using EquipmentsSystem.Client.Services;
using EquipmentsSystem.Client.Services.EquipmentService;
using EquipmentsSystem.Client.Services.AuthService;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using System.Globalization;
using EquipmentsSystem.Client.Services.ReportService;
using FileDownloader;
using Blazored.LocalStorage;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("ar-EG");
CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("ar-EG");
builder.Services.AddMudServices();
builder.Services.AddScoped<IEquipmentService,EquipmentService >();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddBlazorDownloadFile();
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();



await builder.Build().RunAsync();
