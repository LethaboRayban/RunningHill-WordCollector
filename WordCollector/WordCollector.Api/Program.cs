using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using WordCollector.Api.Data;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers()
        .AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));


// Allow Blazor Client origin
builder.Services.AddCors(options =>
{
    options.AddPolicy("BlazorPolicy", policy =>
         policy.WithOrigins("http://localhost:5201","https://localhost:7112")
              .AllowAnyHeader()
              .AllowAnyMethod());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    DbInitializer.Seed(dbContext);
}

app.UseCors("BlazorPolicy");
app.MapStaticAssets();
//app.UseHttpLogging();
app.MapControllers();
app.MapFallbackToFile("index.html");
app.Run();
