using Microsoft.EntityFrameworkCore;
using MintaProjektAPI.Data;
using MintaProjektAPI.Repositories;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Environment
        var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

        // Configuration
        var configuration = new ConfigurationBuilder()
         .SetBasePath(Directory.GetCurrentDirectory())
         .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
         .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true)
         .AddEnvironmentVariables()
         .Build();


        // Add Services
        AddDbContext(builder, configuration);
        AddScopedServices(builder);

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Configure CORS
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowReactApp", policy =>
            {
                policy.WithOrigins("http://localhost:5173") // Your React app URL
                      .AllowAnyHeader()
                      .AllowAnyMethod()
                      .AllowCredentials();
            });
        });

        var app = builder.Build();

        // Ensure that EF does not try to apply migrations at runtime
        DBMigration(app);

        // Use CORS policy
        app.UseCors("AllowReactApp");

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }

    // Add DbContext
    private static void AddDbContext(WebApplicationBuilder builder, IConfiguration configuration)
    {
        builder.Services.AddDbContext<EmployeeDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("MSSQLConnection")));
    }

    // Add Scoped Services
    private static void AddScopedServices(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
    }

    // Migrate any database changes on startup (includes initial db creation)
    private static void DBMigration(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var inventoryContext = scope.ServiceProvider.GetRequiredService<EmployeeDbContext>();
        inventoryContext.Database.Migrate();
    }
}