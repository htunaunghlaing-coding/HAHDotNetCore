// See https://aka.ms/new-console-template for more information
using Serilog;
using Serilog.Sinks.MSSqlServer;

Console.WriteLine("Hello, World!");

 Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .WriteTo.File("logs/myapp.txt", rollingInterval: RollingInterval.Hour)
            .WriteTo
            .MSSqlServer(
             connectionString: "Server=localhost;Database=HAHDotNetCore;User Id=SA;Password=Password123;TrustServerCertificate=true",
             sinkOptions: new MSSqlServerSinkOptions
             {
                 TableName = "Tbl_LogEvents",
                 AutoCreateSqlTable = true
             })
            .CreateLogger();
Log.Information("Hello, world!");

int a = 10, b = 0;
try
{
    Log.Debug("Dividing {A} by {B}", a, b);
    Console.WriteLine(a / b);
}
catch (Exception ex)
{
    Log.Error(ex, "Something went wrong");
}
finally
{
    await Log.CloseAndFlushAsync();
}

Console.ReadKey();