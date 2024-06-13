using System.Data;
using System.Data.SqlClient;
using HAHDotNetCore.ConsoleApp;

Console.WriteLine("Hello, World!");

//AdoDotNetExample adoDotNetExample = new AdoDotNetExample();
//adoDotNetExample.Create("test 11", "test author", "test content");
//adoDotNetExample.Update(1005, "title", "author", "content");
//adoDotNetExample.Delete(1005);
//adoDotNetExample.RetrieveDataById(1004);

//DapperExample dapper = new DapperExample();
//dapper.Run();

EFCoreExample eFCore = new EFCoreExample();
eFCore.Run();

Console.ReadLine();

