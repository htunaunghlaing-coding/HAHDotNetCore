using System.Data;
using System.Data.SqlClient;

Console.WriteLine("Hello, World!");

SqlConnectionStringBuilder stringBuilder = new SqlConnectionStringBuilder();
stringBuilder.DataSource = "localhost";
stringBuilder.InitialCatalog = "HAHDotNetCore";
stringBuilder.UserID = "SA";
stringBuilder.Password = "Password123";

SqlConnection connection = new SqlConnection(stringBuilder.ConnectionString);
connection.Open();
Console.WriteLine("Connection Open.");

string query = "select * from tbl_blog";
SqlCommand cmd = new SqlCommand(query, connection);
SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
DataTable dataTable = new DataTable();
sqlDataAdapter.Fill(dataTable);

connection.Close();
Console.WriteLine("Connection Close.");

foreach (DataRow dr in dataTable.Rows)
{
    Console.WriteLine("Blog Id => " + dr["BlogId"]);
    Console.WriteLine("Blog Title => " + dr["BlogTitle"]);
    Console.WriteLine("Blog Author => " + dr["BlogAuthor"]);
    Console.WriteLine("Blog Content => " + dr["BlogContent"]);
    Console.WriteLine("-----------------------------------");
}

Console.ReadKey();

