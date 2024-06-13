using System;
using System.Data;
using System.Data.SqlClient;

namespace HAHDotNetCore.ConsoleApp
{
    internal class AdoDotNetExample
    {
        private readonly SqlConnectionStringBuilder _stringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = "localhost",
            InitialCatalog = "HAHDotNetCore",
            UserID = "SA",
            Password = "Password123"
        };

        public void Read()
        {
            SqlConnection connection = new SqlConnection(_stringBuilder.ConnectionString);
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
        }

        public void create(string title, string author, string content)
        {
            SqlConnection connection = new SqlConnection(_stringBuilder.ConnectionString);
            connection.Open();

            string query = @"INSERT INTO [dbo].[tbl_Blog]
                            ([BlogTitle],
                            [BlogAuthor],
                            [BlogContent])
                            values
                            (@BlogTitle,
                            @BlogAuthor,
                            @BlogContent)";
            SqlCommand sqlCommand = new SqlCommand(query, connection);
            sqlCommand.Parameters.AddWithValue("@BlogTitle", title);
            sqlCommand.Parameters.AddWithValue("@BlogAuthor", author);
            sqlCommand.Parameters.AddWithValue("@BlogContent", content);
            int result = sqlCommand.ExecuteNonQuery();

            connection.Close();

            string message = result > 0 ? "Insert Success." : "Insert Fail.";
            Console.WriteLine(message);
        }

        public void update(int id, string title, string author, string content)
        {
            SqlConnection sqlConnection = new SqlConnection(_stringBuilder.ConnectionString);
            sqlConnection.Open();

            string query = @"UPDATE [dbo].[tbl_blog]
                            SET [BlogTitle] = @BlogTitle,
                            [BlogAuthor] = @BlogAuthor,
                            [BlogContent] = @BlogContent
                            Where BlogId = @BlogId";

            SqlCommand sqlcommand = new SqlCommand(query, sqlConnection);
            sqlcommand.Parameters.AddWithValue("@BlogId", id);
            sqlcommand.Parameters.AddWithValue("@BlogTitle", title);
            sqlcommand.Parameters.AddWithValue("@BlogAuthor", author);
            sqlcommand.Parameters.AddWithValue("@BlogContent", content);
            int result = sqlcommand.ExecuteNonQuery();

            sqlConnection.Close();

            string message = result > 0 ? "Updte Success." : "Update Fail.";
        }
    }
}

