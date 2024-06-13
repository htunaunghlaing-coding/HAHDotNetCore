using System;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace HAHDotNetCore.ConsoleApp
{
    internal class DapperExample
    {
        public void Run()
        {
            Read();
        }

        public void Read()
        {
            using IDbConnection db = new SqlConnection(SqlConnectionString.stringBuilder.ConnectionString);
            List<BlogDto> blogDtos = db.Query<BlogDto>("select * from tbl_blog").ToList();

            foreach (BlogDto item in blogDtos)
            {
                Console.WriteLine(item.BlogId);
                Console.WriteLine(item.BlogTitle);
                Console.WriteLine(item.BlogAuthor);
                Console.WriteLine(item.BlogContent);
                Console.WriteLine("-----------------------");
            }
        }
    }
}

