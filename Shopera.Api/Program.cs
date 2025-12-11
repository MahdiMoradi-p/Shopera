using Shopera.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// خواندن کانکشن‌استرینگ از appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddInfrastructure(connectionString);

builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
