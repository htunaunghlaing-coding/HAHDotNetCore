using System.Data;
using System.Data.SqlClient;
using HAHDotNetCore.ConsoleApp;
using HAHDotNetCore.ConsoleApp.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

Console.WriteLine("Hello, World!");

//AdoDotNetExample adoDotNetExample = new AdoDotNetExample();
//adoDotNetExample.Create("test 11", "test author", "test content");
//adoDotNetExample.Update(1005, "title", "author", "content");
//adoDotNetExample.Delete(1005);
//adoDotNetExample.RetrieveDataById(1004);

//DapperExample dapper = new DapperExample();
//dapper.Run();

//EFCoreExample eFCore = new EFCoreExample();
//eFCore.Run();
var connectionString = SqlConnectionString.stringBuilder.ConnectionString;
var sqlConnectionStringBuilder = new SqlConnectionStringBuilder(connectionString);

var serviceProvider = new ServiceCollection()
    .AddScoped(n => new AdoDotNetExample(sqlConnectionStringBuilder))
    .AddScoped(n => new DapperExample (sqlConnectionStringBuilder))
    .AddDbContext<AppDbContext>(option =>
    {
    option.UseSqlServer(connectionString);
    })
    .AddScoped<EFCoreExample>()
    .BuildServiceProvider();

//AppDbContext db = serviceProvider.GetRequiredService<AppDbContext>();

//AdoDotNetExample adoDotNetExample = serviceProvider.GetRequiredService<AdoDotNetExample>();
//adoDotNetExample.Read();

DapperExample dapperExample = serviceProvider.GetRequiredService<DapperExample>();
dapperExample.Read();
 
Console.ReadLine();

