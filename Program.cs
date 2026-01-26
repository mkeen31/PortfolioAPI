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
{
    var connectionString = builder.Configuration.GetConnectionString("PortfolioContext") 
        ?? throw new InvalidOperationException("Connection string 'PortfolioContext' not found.");
    options.UseNpgsql(connectionString);
}
);

builder.Services.AddProblemDetails();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    // Set up reverse proxy settings for prod environment
    app.UseForwardedHeaders(new ForwardedHeadersOptions
    {
        ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
    });

    app.UseExceptionHandler(handler =>
    {
        handler.Run(async context => await Results.Problem().ExecuteAsync(context));
    });
}

app.UseStatusCodePages();

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

    var pendingMigrations = context.Database.GetPendingMigrations();
    if (pendingMigrations.Any())
    {
        throw new InvalidOperationException("Database migrations are pending. Please run the migrations before starting the application.");
    }
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseCors();

app.UseMiddleware<ApiKeyMiddleware>();

app.MapControllers();

app.Run();
