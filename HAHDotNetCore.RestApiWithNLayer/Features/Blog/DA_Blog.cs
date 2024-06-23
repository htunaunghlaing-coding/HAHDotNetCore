using System;

namespace HAHDotNetCore.RestApiWithNLayer.Feature.Blog;

public class DA_Blog
{
    private readonly AppDbContext _db;

    public DA_Blog()
    {
        _db = new AppDbContext();
    }

    public List<BlogModel> GetBlogs()
    {
        var lists = _db.Blogs.ToList();
        return lists;
    }

    public BlogModel GetBlog(int id)
    {
        var item = _db.Blogs.FirstOrDefault(x => x.BlogId == id);
        return item;
    }

    public int CreateBlog(BlogModel requestModel)
    {
        _db.Blogs.Add(requestModel);
        var result = _db.SaveChanges();
        return result;
    }

    public int UpdateBlog(int id, BlogModel requestModel)
    {
        var item = _db.Blogs.FirstOrDefault(x => x.BlogId == id);
        if (item is null) return 0;

        item.BlogTitle = requestModel.BlogTitle;
        item.BlogAuthor = requestModel.BlogAuthor;
        item.BlogContent = requestModel.BlogContent;
        var result = _db.SaveChanges();
        return result;
    }

    public int PatchBlog(int id, BlogModel requestModel)
    {
        var item = _db.Blogs.FirstOrDefault(x => x.BlogId == id);
        if (item is null) return 0;

        if (!string.IsNullOrEmpty(requestModel.BlogTitle))
        {
            item.BlogTitle = requestModel.BlogTitle;
        }
        if (!string.IsNullOrEmpty(requestModel.BlogAuthor))
        {
            item.BlogAuthor = requestModel.BlogAuthor;
        }
        if (!string.IsNullOrEmpty(requestModel.BlogContent))
        {
            item.BlogContent = requestModel.BlogContent;
        }

        var result = _db.SaveChanges();
        return result;
    }

    public int DeleteBlog(int id)
    {
        var item = _db.Blogs.FirstOrDefault(x => x.BlogId == id);
        if (item is null) return 0;

        _db.Blogs.Remove(item);
        var result = _db.SaveChanges();
        return result;
    }
}
