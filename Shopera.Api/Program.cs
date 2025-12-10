using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// --------------------
// Add services
// --------------------

builder.Services.AddControllers();


// builder.Services.AddSwaggerGen();

// --------------------
// Build
// --------------------

var app = builder.Build();

// --------------------
// Middlewares
// --------------------

if (app.Environment.IsDevelopment())
{
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
