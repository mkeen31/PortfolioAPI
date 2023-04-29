using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.HttpOverrides;
using PortfolioAPI.Data;
using PortfolioAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

string corsUrl = Environment.GetEnvironmentVariable("CORS_URL") ?? string.Empty;
builder.Services.AddCors(options => 
{
    options.AddDefaultPolicy(policy => 
    {
        policy.WithOrigins(corsUrl)
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PortfolioContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("PortfolioContext"))
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// If database does not exist then create it
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<PortfolioContext>();
    context.Database.EnsureCreated();
}

if (!app.Environment.IsDevelopment())
{
    // Set up reverse proxy settings for prod environment
    app.UseForwardedHeaders(new ForwardedHeadersOptions
    {
        ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseCors();

app.UseMiddleware<ApiKeyMiddleware>();

app.MapControllers();

app.Run();
