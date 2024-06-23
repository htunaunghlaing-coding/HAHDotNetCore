using System;
using Refit;

namespace HAHDotNetCore.ConsoleAppRefitExamples;

public interface IBlogApi
{
    [Get("/api/blog")]
    Task<List<BlogModel>> GetBlogs();

    [Get("/api/blog/{id}")]
    Task<BlogModel> GetBlog(int id);

    [Post("/api/blog")]
    Task<string> CreateBlog(BlogModel blog);

    [Put("/api/blog/{id}")]
    Task<string> UpdateBlog(int id, [Body] BlogModel blog);

    [Delete("/api/blog/{id}")]
    Task<string> DeleteBlog(int id);
}

