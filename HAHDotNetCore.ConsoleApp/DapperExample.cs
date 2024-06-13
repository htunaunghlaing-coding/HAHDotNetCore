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
            //Read();
            //RetrieveDataById(1);
            //RetrieveDataById(20);
            //Create("test 1007", "test author", "test content");
            //Update(1007, "1007 test", "1007 author", "1007 content");
            Delete(1007);
        }

        public void Read()
        {
            using IDbConnection db = new SqlConnection(SqlConnectionString.stringBuilder.ConnectionString);
            List<BlogDto> blogDtos = db.Query<BlogDto>("select * from tbl_blog").ToList();

            foreach (BlogDto item in blogDtos)
            {
                Console.WriteLine($"Blog Id => {item.BlogId}");
                Console.WriteLine($"Blog Title => {item.BlogTitle}");
                Console.WriteLine($"Blog Author => {item.BlogAuthor}");
                Console.WriteLine($"Blog Content => {item.BlogContent}");
                Console.WriteLine("---------------------------------------");
            }
        }

        public void RetrieveDataById(int id)
        {
            using IDbConnection db = new SqlConnection(SqlConnectionString.stringBuilder.ConnectionString);
            var blogDto = db.Query<BlogDto>("select * from tbl_blog where blogId = @BlogId", new BlogDto { BlogId = id }).FirstOrDefault();

            if (blogDto is null)
            {
                Console.WriteLine("Dat not found in the table.");
                return;
            }

            Console.WriteLine($"Blog Id => {blogDto.BlogId}");
            Console.WriteLine($"Blog Title => {blogDto.BlogTitle}");
            Console.WriteLine($"Blog Author => {blogDto.BlogAuthor}");
            Console.WriteLine($"Blog Content => {blogDto.BlogContent}");
            Console.WriteLine("---------------------------------------");
        }

        public void Create(string title, string author, string content)
        {
            var item = new BlogDto
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };

            string query = @"INSERT INTO [dbo].[tbl_Blog]
                            ([BlogTitle],
                            [BlogAuthor],
                            [BlogContent])
                            values
                            (@BlogTitle,
                            @BlogAuthor,
                            @BlogContent)";
            using IDbConnection db = new SqlConnection(SqlConnectionString.stringBuilder.ConnectionString);
            int result = db.Execute(query, item);

            String message = result > 0 ? "Insert Data Success." : "Insert Data Fail.";
            Console.WriteLine(message);
        }

        public void Update(int id, string title, string author, string content)
        {
            var item = new BlogDto
            {
                BlogId = id,
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };

            string query = @"UPDATE [dbo].[tbl_blog]
                            SET [BlogTitle] = @BlogTitle,
                            [BlogAuthor] = @BlogAuthor,
                            [BlogContent] = @BlogContent
                            Where BlogId = @BlogId";

            using IDbConnection db = new SqlConnection(SqlConnectionString.stringBuilder.ConnectionString);
            int result = db.Execute(query, item);

            string message = result > 0 ? "Update Data Successfully." : "Update Data Fail.";
            Console.WriteLine(message);
        }

        public void Delete(int id)
        {
            var item = new BlogDto()
            {
                BlogId = id
            };

            string query = @"Delete from [dbo].[tbl_blog] where BlogId = @BlogId";

            using IDbConnection db = new SqlConnection(SqlConnectionString.stringBuilder.ConnectionString);
            int result = db.Execute(query, item);

            string message = result > 0 ? "Delete Data Successfully." : "Delete Data Fail.";
            Console.WriteLine(message);
        }
    }
}

