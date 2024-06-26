using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using HAHDotNetCore.Shared;
using Microsoft.AspNetCore.Mvc;
using static HAHDotNetCore.Shared.AdoDotNetService;

namespace HAHDotNetCore.RestApiDependencyInjection.Controllers;

[Route("api/[controller]")]
public class BlogAdoDotNetController2 : Controller
{
    private readonly AdoDotNetService _adoDotNetService = new AdoDotNetService(SqlConnectionString.StringBuilder.ConnectionString);

    [HttpGet]
    public IActionResult GetBlogs()
    {
        string query = "select * from tbl_blog";
        List<BlogModel> blogs = _adoDotNetService.Query<BlogModel>(query);

        return Ok(blogs);
    }

    [HttpGet("{id}")]
    public IActionResult GetBlogById(int id)
    {
        string query = "select * from tbl_blog where BlogId = @BlogId";
        var item = _adoDotNetService.QueryFirstOrDefault<BlogModel>(query, new AdoDotNetParameter("@BlogId", id));

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

        int result = _adoDotNetService.Execute(query,
            new AdoDotNetParameter("@BlogTitle", blog.BlogTitle),
            new AdoDotNetParameter("@BlogAuthor", blog.BlogAuthor),
            new AdoDotNetParameter("@BlogAuthor", blog.BlogContent));

        string message = result > 0 ? "Insert data successfully." : "Insert data fail.";
        return Ok(message);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateBlog(int id, BlogModel blog)
    {
        string checkQuery = "SELECT * FROM tbl_blog WHERE BlogId = @BlogId";
        var checkResult = _adoDotNetService.Execute(checkQuery, new AdoDotNetParameter("@BlogId", id));

        if (checkResult == 0)
        {
            return NotFound("Data not found in the table.");
        }

        string query = @"UPDATE [dbo].[tbl_blog]
                            SET [BlogTitle] = @BlogTitle,
                            [BlogAuthor] = @BlogAuthor,
                            [BlogContent] = @BlogContent
                            Where BlogId = @BlogId";


        int result = _adoDotNetService.Execute(query,
            new AdoDotNetParameter("@BlogId", id),
            new AdoDotNetParameter("@BlogTitle", blog.BlogTitle),
            new AdoDotNetParameter("@BlogAuthor", blog.BlogAuthor),
            new AdoDotNetParameter("@BlogContent", blog.BlogContent));

        string message = result > 0 ? "Update Data Successfully." : "Update Data Fail.";
        return Ok(message);
    }

    [HttpPatch("{id}")]
    public IActionResult PatchBlog(int id, BlogModel blog)
    {
        string checkQuery = "SELECT * FROM tbl_blog WHERE BlogId = @BlogId";
        var checkResult = _adoDotNetService.Execute(checkQuery, new AdoDotNetParameter("@BlogId", id));

        if (checkResult == 0)
        {
            return NotFound("Data not found in the table.");
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

        if (string.IsNullOrEmpty(conditions))
        {
            return BadRequest("Not need data to update.");
        }

        conditions = conditions.Substring(0, conditions.Length - 2);

        string updateQuery = $@"UPDATE [dbo].[tbl_blog]
                            SET {conditions}
                            WHERE BlogId = @BlogId";

        try
        {
            var parameters = new List<AdoDotNetParameter>
                {
                    new AdoDotNetParameter("@BlogId", id)
                };

            if (!string.IsNullOrEmpty(blog.BlogTitle))
            {
                parameters.Add(new AdoDotNetParameter("@BlogTitle", blog.BlogTitle));
            }
            if (!string.IsNullOrEmpty(blog.BlogAuthor))
            {
                parameters.Add(new AdoDotNetParameter("@BlogAuthor", blog.BlogAuthor));
            }
            if (!string.IsNullOrEmpty(blog.BlogContent))
            {
                parameters.Add(new AdoDotNetParameter("@BlogContent", blog.BlogContent));
            }

            int result = _adoDotNetService.Execute(updateQuery, parameters.ToArray());

            string message = result > 0 ? "Update data successfully." : "Update data failed.";
            return Ok(message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpDelete]
    public IActionResult DeleteBlog(int id)
    {
        string checkQuery = "SELECT * FROM tbl_blog WHERE BlogId = @BlogId";
        var checkResult = _adoDotNetService.Execute(checkQuery, new AdoDotNetParameter("@BlogId", id));

        if (checkResult == 0)
        {
            return NotFound("Data not found in the table.");
        }

        string deleteQuery = @"Delete from [dbo].[tbl_blog] where BlogId = @BlogId";
        int result = _adoDotNetService.Execute(deleteQuery,
            new AdoDotNetParameter("@BlogId", id));
        if (result == 0)
        {
            return NotFound("Data not found in the data.");
        }

        string message = result > 0 ? "Delete Data Successfully." : "Delete Data Fail.";
        return Ok(message);
    }
}

