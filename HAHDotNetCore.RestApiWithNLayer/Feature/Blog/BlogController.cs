using System;

namespace HAHDotNetCore.RestApiWithNLayer.Feature.Blog;

[Route("api/[controller]")]
public class BlogController : Controller
{
    private readonly BL_Blog _bL_Blog;

    public BlogController()
    {
        _bL_Blog = new BL_Blog();
    }

    [HttpGet]
    public IActionResult Read()
    {
        var lists = _bL_Blog.GetBlogs();
        return Ok(lists);
    }

    [HttpGet("{id}")]
    public IActionResult Edit(int id)
    {
        var item = _bL_Blog.GetBlog(id);
        if (item is null)
        {
            return NotFound("Data not found in the table.");
        }
        return Ok(item);
    }

    [HttpPost]
    public IActionResult Create(BlogModel blog)
    {
        var result = _bL_Blog.CreateBlog(blog);
        string message = result > 0 ? "Saving Data Successfully." : "Saving Data Fail";
        return Ok(message);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, BlogModel blog)
    {
        var item = _bL_Blog.GetBlog(id);
        if (item is null)
        {
            return NotFound("Data not found in the table.");
        }

        var result = _bL_Blog.UpdateBlog(id, blog);
        string message = result > 0 ? "Update Data Successfully" : "Update Data Fail.";
        return Ok(message);
    }

    [HttpPatch("{id}")]
    public IActionResult Patch(int id, BlogModel blog)
    {
        var item = _bL_Blog.GetBlog(id);
        if (item is null)
        {
            return NotFound("Data not found in the table.");
        }

        var result = _bL_Blog.PatchBlog(id, blog);
        string message = result > 0 ? "Update Data Successfully." : "Update Data Fail";
        return Ok(message);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var item = _bL_Blog.GetBlog(id);
        if (item is null)
        {
            return NotFound("Data not found in the table.");
        }

        var result = _bL_Blog.DeleteBlog(id);
        string message = result > 0 ? "Delete Data Successfully." : "Delete Data Fail.";
        return Ok(message);
    }

}

