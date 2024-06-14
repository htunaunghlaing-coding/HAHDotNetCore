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
    public IActionResult getBlogs()
    {
        string query = "select * from tbl_blog";

        using IDbConnection db = new SqlConnection(SqlConnectionString.StringBuilder.ConnectionString);
        List<BlogModel> lists = db.Query<BlogModel>(query).ToList();

        return Ok(lists);
    }

    [HttpGet("{id}")]
    public IActionResult getBlogById(int id)
    {
        string query = "select * from tbl_blog where blogId = @BlogId";
        using IDbConnection db = new SqlConnection(SqlConnectionString.StringBuilder.ConnectionString);
        var item = db.Query<BlogModel>(query, new BlogModel { BlogId = id }).FirstOrDefault();
        if (item is null)
        {
            return NotFound("Data not found in the table.");
        }
        return Ok(item);
    }
}

