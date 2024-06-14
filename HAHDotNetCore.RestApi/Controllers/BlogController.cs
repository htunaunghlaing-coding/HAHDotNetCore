using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HAHDotNetCore.RestApi.Controllers;

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
        if(item is null)
        {
            return NotFound("Data not found in the table.");
        }
        return Ok(item);
    }

    [HttpPost]
    public IActionResult Create()
    {
        return Ok("Create");
    }

    [HttpPut]
    public IActionResult Update()
    {
        return Ok("Update");
    }

    [HttpPatch]
    public IActionResult Patch()
    {
        return Ok("Patch");
    }

    [HttpDelete]
    public IActionResult Delete()
    {
        return Ok("Delete");
    }
}

