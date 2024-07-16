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

public class BlogAjaxController : Controller
{
    private AppDbContext _db;


    public BlogAjaxController(AppDbContext db)
    {
        _db = db;
    }

    // GET: /<controller>/
    public async Task<IActionResult> Index()
    {
        var lst = await _db.Blogs
            .OrderByDescending(x => x.BlogId)
            .ToListAsync();
        return View(lst);
    }

    [ActionName("Create")]
    public IActionResult BlogCreate()
    {
        return View("CreateBlog");
    }

    [HttpPost]
    [ActionName("Save")]
    public async Task<IActionResult> BlogCreate(BlogModel blog)
    {
        await _db.Blogs.AddAsync(blog);
        var result = await _db.SaveChangesAsync();

        string message = result > 0 ? "Saving Data Successfully." : "Saving Data Fail.";
        BlogMessageResponseModel responseModel = new BlogMessageResponseModel()
        {
            IsSuccess = result > 0,
            Message = message
        };
        return Json(responseModel);
    }

    [HttpGet]
    [ActionName("Edit")]
    public async Task<IActionResult> BlogEdit(int id)
    {
        var item = await _db.Blogs.FirstOrDefaultAsync(x => x.BlogId == id);
        if (item is null)
        {
            return Redirect("/Blog");
        }
        return View("EditBlog", item);
    }

    [HttpPost]
    [ActionName("Update")]
    public IActionResult BlogUpdate(int id, BlogModel blog)
    {
        BlogMessageResponseModel responseModel = new BlogMessageResponseModel();
        var item = _db.Blogs.FirstOrDefault(x => x.BlogId == id);
        if (item is null)
        {
            responseModel = new BlogMessageResponseModel()
            {
                IsSuccess = false,
                Message = "No Data Found."
            };
            return Json(responseModel);
        }

        item.BlogTitle = blog.BlogTitle;
        item.BlogAuthor = blog.BlogAuthor;
        item.BlogContent = blog.BlogContent;

        int result = _db.SaveChanges();
        string message = result > 0 ? "Update Data Successfully." : "Update Data Fail.";
        responseModel = new BlogMessageResponseModel()
        {
            IsSuccess = result > 0,
            Message = message
        };
        return Json(responseModel);
    }

    [HttpPost]
    [ActionName("Delete")]
    public async Task<IActionResult> BlogDelete(BlogModel blog)
    {
        BlogMessageResponseModel responseModel = new BlogMessageResponseModel();
        var item = await _db.Blogs.FirstOrDefaultAsync(x => x.BlogId == blog.BlogId);
        if (item is null)
        {
            responseModel = new BlogMessageResponseModel()
            {
                IsSuccess = false,
                Message = "No Data Foud."
            };
            return Json(responseModel);
        }

        _db.Blogs.Remove(item);
        int result = await _db.SaveChangesAsync();
        string message = result > 0 ? "Delete Data Successfully." : "Delete Data Fail.";
        responseModel = new BlogMessageResponseModel()
        {
            IsSuccess = result > 0,
            Message = message
        };
        return Json(responseModel);
    }
}

