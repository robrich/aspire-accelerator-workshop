using AspireBrownfield.Web;
using AspireBrownfield.Web.Components;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.

// local debugging: "localhost:6379"
string? redisConnStr = builder.Configuration.GetConnectionString("Redis");
ArgumentNullException.ThrowIfNullOrWhiteSpace(redisConnStr);
builder.Services.AddStackExchangeRedisOutputCache(options => {
	options.Configuration = redisConnStr;
});

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddHttpClient<WeatherApiClient>(client =>
    {
        string? apiUrl = builder.Configuration.GetValue<string>("services:apiservice:https:0") ?? builder.Configuration.GetValue<string>("services:apiservice:http:0");
        ArgumentNullException.ThrowIfNullOrWhiteSpace(apiUrl);
        client.BaseAddress = new Uri(apiUrl);
    });

//

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAntiforgery();

app.UseOutputCache();

app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
