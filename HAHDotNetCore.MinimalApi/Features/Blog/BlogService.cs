using System;
using HAHDotNetCore.MinimalApi.Db;
using HAHDotNetCore.MinimalApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HAHDotNetCore.MinimalApi.Features.Blog;

public static class BlogService
{
    public static IEndpointRouteBuilder MapBlog(this IEndpointRouteBuilder app)
    {
        app.MapGet("api/Blog", async (AppDbContext db) =>
        {
            var lst = await db.Blogs.AsNoTracking().ToListAsync();
            return Results.Ok(lst);
        });

        app.MapPost("api/Blog", async (AppDbContext db, BlogModel blog) =>
        {
            await db.Blogs.AddAsync(blog);
            var result = await db.SaveChangesAsync();

            string message = result > 0 ? "Saving Data Successfully." : "Saving Data Fail.";
            return Results.Ok(message);
        });

        app.MapPut("api/Blog/{id}", async (AppDbContext db, int id, BlogModel blog) =>
        {
            var item = await db.Blogs.FirstOrDefaultAsync(x => x.BlogId == id);
            if (item is null)
            {
                return Results.NotFound("Not found the data.");
            }

            item.BlogId = id;
            item.BlogTitle = blog.BlogTitle;
            item.BlogAuthor = blog.BlogAuthor;
            item.BlogContent = blog.BlogContent;
            var result = await db.SaveChangesAsync();

            var message = result > 0 ? "Update Data Successfully." : "Update Data Fail.";
            return Results.Ok(message);
        });

        app.MapDelete("api/Blog/{id}", async (AppDbContext db, int id) =>
        {
            var item = await db.Blogs.FirstOrDefaultAsync(x => x.BlogId == id);
            if (item is null)
            {
                return Results.NotFound("Not found the data.");
            }

            db.Remove(item);
            var result = db.SaveChanges();

            var message = result > 0 ? "Delete Data Successfully." : "Delete Data Fail.";
            return Results.Ok(message);
        });

        return app;
    }
}

