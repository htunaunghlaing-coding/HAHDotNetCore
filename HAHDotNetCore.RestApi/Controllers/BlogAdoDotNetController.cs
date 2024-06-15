using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HAHDotNetCore.RestApi.Controllers;

[Route("api/[controller]")]
public class BlogAdoDotNetController : Controller
{
    [HttpGet]
    public IActionResult GetBlogs()
    {
        string query = "select * from tbl_blog";

        SqlConnection connection = new SqlConnection(SqlConnectionString.StringBuilder.ConnectionString);
        connection.Open();

        SqlCommand cmd = new SqlCommand(query, connection);
        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        DataTable data = new DataTable();
        adapter.Fill(data);

        connection.Close();

        //List<BlogModel> blogs = new List<BlogModel>();
        //foreach (DataRow dr in data.Rows)
        //{
        //    BlogModel blog = new BlogModel();
        //    blog.BlogId = Convert.ToInt32(dr["BlogId"]);
        //    blog.BlogTitle = Convert.ToString(dr["BlogTitle"]);
        //    blog.BlogAuthor = Convert.ToString(dr["BlogAuthor"]);
        //    blog.BlogContent = Convert.ToString(dr["BlogContent"]);
        //    blogs.Add(blog);
        //}

        List<BlogModel> blogs = data.AsEnumerable().Select(dr => new BlogModel
        {
            BlogId = Convert.ToInt32(dr["BlogId"]),
            BlogTitle = Convert.ToString(dr["BlogTitle"]),
            BlogAuthor = Convert.ToString(dr["BlogAuthor"]),
            BlogContent = Convert.ToString(dr["BlogContent"])
        }).ToList();

        return Ok(blogs);
    }

    [HttpGet("{id}")]
    public IActionResult GetBlogById(int id)
    {
        string query = "select * from tbl_blog where BlogId = @BlogId";

        SqlConnection connection = new SqlConnection(SqlConnectionString.StringBuilder.ConnectionString);
        connection.Open();

        SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@BlogId", id);
        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        DataTable data = new DataTable();
        adapter.Fill(data);

        connection.Close();

        if (data.Rows.Count == 0)
        {
            return NotFound("Data not found in the table.");
        }

        DataRow dr = data.Rows[0];
        var item = new BlogModel
        {
            BlogId = Convert.ToInt32(dr["BlogId"]),
            BlogTitle = Convert.ToString(dr["BlogTitle"]),
            BlogAuthor = Convert.ToString(dr["BlogAuthor"]),
            BlogContent = Convert.ToString(dr["BlogContent"])
        };

        return Ok(item);
    }

    [HttpPost]
    public IActionResult CreateBlog(BlogModel blog)
    {
        string query = @"INSERT INTO [dbo].[tbl_Blog]
                            ([BlogTitle],
                            [BlogAuthor],
                            [BlogContent])
                            values
                            (@BlogTitle,
                            @BlogAuthor,
                            @BlogContent)";

        SqlConnection connection = new SqlConnection(SqlConnectionString.StringBuilder.ConnectionString);
        connection.Open();

        SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@BlogTitle", blog.BlogTitle);
        cmd.Parameters.AddWithValue("@BlogAuthor", blog.BlogAuthor);
        cmd.Parameters.AddWithValue("@BlogContent", blog.BlogContent);
        int result = cmd.ExecuteNonQuery();

        connection.Close();

        string message = result > 0 ? "Insert data successfully." : "Insert data fail.";
        return Ok(message);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateBlog(int id, BlogModel blog)
    {
        string query = @"UPDATE [dbo].[tbl_blog]
                            SET [BlogTitle] = @BlogTitle,
                            [BlogAuthor] = @BlogAuthor,
                            [BlogContent] = @BlogContent
                            Where BlogId = @BlogId";

        SqlConnection connection = new SqlConnection(SqlConnectionString.StringBuilder.ConnectionString);
        connection.Open();

        SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@BlogId", id);
        cmd.Parameters.AddWithValue("@BlogTitle", blog.BlogTitle);
        cmd.Parameters.AddWithValue("@BlogAuthor", blog.BlogAuthor);
        cmd.Parameters.AddWithValue("@BlogContent", blog.BlogContent);
        int result = cmd.ExecuteNonQuery();

        connection.Close();

        string message = result > 0 ? "Update Data Successfully." : "Update Data Fail.";
        return Ok(message);
    }

}

