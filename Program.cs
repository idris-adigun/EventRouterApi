using EventRouterApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddSingleton<EventRouterService>();

// Add controllers
builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
