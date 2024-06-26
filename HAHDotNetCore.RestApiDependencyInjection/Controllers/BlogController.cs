using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HAHDotNetCore.RestApiDependencyInjection.Controllers;

[Route("api/[controller]")]
public class BlogController : Controller
{
    private readonly AppDbContext _db;

    public BlogController()
    {
        _db = new AppDbContext();
    }

    [HttpGet]
    public IActionResult Read()
    {
        var list = _db.Blogs.ToList();
        return Ok(list);
    }

    [HttpGet("{id}")]
    public IActionResult Edit(int id)
    {
        var item = _db.Blogs.FirstOrDefault(x => x.BlogId == id);
        if (item is null)
        {
            return NotFound("Data not found in the table.");
        }
        return Ok(item);
    }

    [HttpPost]
    public IActionResult Create(BlogModel blog)
    {
        _db.Blogs.Add(blog);
        var result = _db.SaveChanges();

        string message = result > 0 ? "Saving Data Success." : "Saving Data Fail.";
        return Ok(message);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, BlogModel blog)
    {
        var item = _db.Blogs.FirstOrDefault(x => x.BlogId == id);
        if (item is null)
        {
            return NotFound("Data not found in the table.");
        }

        item.BlogTitle = blog.BlogTitle;
        item.BlogAuthor = blog.BlogAuthor;
        item.BlogContent = blog.BlogAuthor;
        var result = _db.SaveChanges();

        string message = result > 0 ? "Update data successfully." : "Update data fail.";
        return Ok(message);
    }

    [HttpPatch("{id}")]
    public IActionResult Patch(int id, BlogModel blog)
    {
        var item = _db.Blogs.FirstOrDefault(x => x.BlogId == id);
        if (item is null)
        {
            return NotFound("Data not found in the table.");
        }

        if (!string.IsNullOrEmpty(blog.BlogTitle))
        {
            item.BlogTitle = blog.BlogTitle;
        }
        if (!string.IsNullOrEmpty(blog.BlogAuthor))
        {
            item.BlogAuthor = blog.BlogAuthor;
        }
        if (!string.IsNullOrEmpty(blog.BlogContent))
        {
            item.BlogContent = blog.BlogContent;
        }
        int result = _db.SaveChanges();

        string mesage = result > 0 ? "Updaing Data Successfully." : "Updating Data Fail.";
        return Ok(mesage);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var item = _db.Blogs.FirstOrDefault(x => x.BlogId == id);
        if (item is null)
        {
            return NotFound("Data Not found in the table.");
        }
        _db.Blogs.Remove(item);
        int result = _db.SaveChanges();

        string message = result > 0 ? "Delete Data Successfully." : "Delete Data Fail.";
        return Ok(message);
    }
}

