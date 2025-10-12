var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddProblemDetails();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<FrameworkApiContext>(o =>
    o.UseNpgsql(builder.Configuration.GetConnectionString("voting")));

builder.AddRedisOutputCache("cache");

builder.Services.AddControllers();

builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
});

builder.Services.AddScoped<IFrameworkRepository, FrameworkRepository>();
builder.Services.AddScoped<IFrameworkApiContext>(provider => provider.GetRequiredService<FrameworkApiContext>());


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi("/swagger/v1/swagger.json");
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/error");
    app.UseHsts();
}

app.MapGet("/", () => "Backend is running!");
app.MapControllers();

app.MapDefaultEndpoints();

app.Map("/error", () => Results.Problem());
app.Map("/{*url}", () => Results.NotFound(new { message = "Not Found", status = 404 }));

app.UseOutputCache();

app.Run();
