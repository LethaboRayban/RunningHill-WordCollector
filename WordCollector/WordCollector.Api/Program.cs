using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using WordCollector.Api.Data;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers()
        .AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=words.db"));


// Allow Blazor Client origin
builder.Services.AddCors(options =>
{
    options.AddPolicy("BlazorPolicy", policy =>
         policy.WithOrigins("https://localhost:7112")
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


app.UseBlazorFrameworkFiles();

var defaultFilesOptions = new DefaultFilesOptions();
defaultFilesOptions.DefaultFileNames.Clear();
defaultFilesOptions.DefaultFileNames.Add("Home.html");
app.UseDefaultFiles(defaultFilesOptions);

app.UseStaticFiles();
app.UseCors("BlazorPolicy");
app.MapControllers();
app.MapFallbackToFile("Home.html");
app.Run();
