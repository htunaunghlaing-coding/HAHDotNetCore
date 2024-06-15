using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HAHDotNetCore.RestApi.Controllers;

[Route("api/[controller]")]
public class BlogDapperController : Controller
{
    [HttpGet]
    public IActionResult GetBlogs()
    {
        string query = "select * from tbl_blog";

        using IDbConnection db = new SqlConnection(SqlConnectionString.StringBuilder.ConnectionString);
        List<BlogModel> lists = db.Query<BlogModel>(query).ToList();

        return Ok(lists);
    }

    [HttpGet("{id}")]
    public IActionResult GetBlogById(int id)
    {
        var item = FindById(id);
        if (item is null)
        {
            return NotFound("Data not found in the table.");
        }
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
        using IDbConnection db = new SqlConnection(SqlConnectionString.StringBuilder.ConnectionString);
        int result = db.Execute(query, blog);

        string message = result > 0 ? "Insert Data Successfully." : "Insert Data Fail.";
        return Ok(message);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateBlog(int id, BlogModel blog)
    {
        var item = FindById(id);
        if (item is null)
        {
            return NotFound("Data Not Found In the table.");
        }

        blog.BlogId = id;
        string query = @"UPDATE [dbo].[tbl_blog]
                            SET [BlogTitle] = @BlogTitle,
                            [BlogAuthor] = @BlogAuthor,
                            [BlogContent] = @BlogContent
                            Where BlogId = @BlogId";

        using IDbConnection db = new SqlConnection(SqlConnectionString.StringBuilder.ConnectionString);
        int result = db.Execute(query, blog);

        string message = result > 0 ? "Update Data Successfully." : "Update Data Fail.";
        return Ok(message);
    }

    [HttpPatch("{id}")]
    public IActionResult PatchBlog(int id, BlogModel blog)
    {
        var item = FindById(id);
        if (item is null)
        {
            return NotFound("Data Not Found In the table.");
        }

        string conditions = string.Empty;

        if (!string.IsNullOrEmpty(blog.BlogTitle))
        {
            conditions += "[BlogTitle] = @BlogTitle, ";
        }
        if (!string.IsNullOrEmpty(blog.BlogAuthor))
        {
            conditions += "[BlogAuthor] = @BlogAuthor, ";
        }
        if (!string.IsNullOrEmpty(blog.BlogContent))
        {
            conditions += "[BlogContent] = @BlogContent, ";
        }
        if (conditions.Length == 0)
        {
            return NotFound("No data to update.");
        }

        conditions = conditions.Substring(0, conditions.Length - 2);

        blog.BlogId = id;
        string query = $@"UPDATE [dbo].[tbl_blog]
                            SET {conditions}
                            Where BlogId = @BlogId";


        using IDbConnection db = new SqlConnection(SqlConnectionString.StringBuilder.ConnectionString);
        int result = db.Execute(query, blog);

        string message = result > 0 ? "Update Data Successfully." : "Update Data Fail.";
        return Ok(message);
    }

    [HttpDelete("{id}")] 
    public IActionResult DeleteBlog(int id)
    {
        var item = FindById(id);
        if (item is null)
        {
            return NotFound("Data Not Found In the table.");
        }

        string query = @"Delete from [dbo].[tbl_blog] where blogId = @BlogId";
        using IDbConnection db = new SqlConnection(SqlConnectionString.StringBuilder.ConnectionString);
        int result = db.Execute(query, new BlogModel { BlogId = id });

        string message = result > 0 ? "Delete Data Successfully." : "Delete Data Fail.";
        return Ok(message);
    }

    private BlogModel? FindById(int id)
    {
        string query = "select * from tbl_blog where blogId = @BlogId";
        using IDbConnection db = new SqlConnection(SqlConnectionString.StringBuilder.ConnectionString);
        var item = db.Query<BlogModel>(query, new BlogModel { BlogId = id }).FirstOrDefault();
        return item;
    }

}

