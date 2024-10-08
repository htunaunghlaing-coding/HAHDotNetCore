using Serilog;
using Serilog.Sinks.MSSqlServer;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

string outputFolderPath = AppDomain.CurrentDomain.BaseDirectory;
string logFilePath = Path.Combine(outputFolderPath, "logs/DotNetTrainingBatch3.WebApi_.txt");

var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json",
    optional: false,
    reloadOnChange: true).Build();

string connectionString = configuration.GetConnectionString("DbConnection")!;
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File(logFilePath, rollingInterval: RollingInterval.Hour)
    .WriteTo
    .MSSqlServer(
        connectionString: connectionString,
        sinkOptions: new MSSqlServerSinkOptions
        {
            TableName = "Tbl_LogEvents",
            AutoCreateSqlTable = true,
        })
    .CreateLogger();
try
{
    Log.Information("Starting web application");
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: MyAllowSpecificOrigins,
                          policy =>
                          {
                              policy.WithOrigins("https://localhost:7033",
                                                  "http://localhost:5204")
                              .AllowAnyMethod()
                              .AllowAnyHeader();
                          });
    });

    builder.Host.UseSerilog(); // <-- Add this line

    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseCors(MyAllowSpecificOrigins);

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}