﻿using System;
using HAHDotNetCore.NLayer.DataAccess.Models;
using HAHDotNetCore.NLayer.DataAccess.Services.DA_Blog;

namespace HAHDotNetCore.NLayer.BusinessLogic.Services.BL_Blog;
public class BL_Blog
{
    private readonly DA_Blog _daBlog;

    public BL_Blog()
    {
        _daBlog = new DA_Blog();
    }

    public List<BlogModel> GetBlogs()
    {
        var lists = _daBlog.GetBlogs();
        return lists;
    }

    public BlogModel GetBlog(int id)
    {
        var item = _daBlog.GetBlog(id);
        return item;
    }

    public int CreateBlog(BlogModel requestModel)
    {
        var result = _daBlog.CreateBlog(requestModel);
        return result;
    }

    public int UpdateBlog(int id, BlogModel requestModel)
    {
        var result = _daBlog.UpdateBlog(id, requestModel);
        return result;
    }

    public int PatchBlog(int id, BlogModel requestModel)
    {
        var result = _daBlog.PatchBlog(id, requestModel);
        return result;
    }

    public int DeleteBlog(int id)
    {
        var result = _daBlog.DeleteBlog(id);
        return result;
    }
}

