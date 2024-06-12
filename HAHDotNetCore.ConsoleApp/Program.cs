using System.Data;
using System.Data.SqlClient;
using HAHDotNetCore.ConsoleApp;

Console.WriteLine("Hello, World!");

AdoDotNetExample adoDotNetExample = new AdoDotNetExample();
adoDotNetExample.create("test 11", "test author", "test content");

Console.ReadLine();

