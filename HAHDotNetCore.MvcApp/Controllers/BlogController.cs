using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HAHDotNetCore.MvcApp.Db;
using HAHDotNetCore.MvcApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HAHDotNetCore.MvcApp.Controllers;

public class BlogController : Controller
{
    private AppDbContext _db;


    public BlogController(AppDbContext db)
    {
        _db = db;
    }

    // GET: /<controller>/
    public async Task<IActionResult> Index()
    {
        var lst = await _db.Blogs.ToListAsync();
        return View(lst);
    }

    [ActionName("Create")]
    public IActionResult BlogCreate()
    {
        return View("CreateBlog");
    }

    [ActionName("Save")]
    public async Task<IActionResult> BlogCreateAsync(BlogModel blog)
    {
        await _db.Blogs.AddAsync(blog);
        var result = await _db.SaveChangesAsync();
        return Redirect("/Blog");
    }
}

