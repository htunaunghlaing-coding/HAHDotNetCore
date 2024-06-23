using System;
using Refit;

namespace HAHDotNetCore.ConsoleAppRefitExamples;

public class RefitExamples
{
    private readonly IBlogApi _service = RestService.For<IBlogApi>("https://localhost:7223");

    public async Task RunAsync()
    {
        //await ReadAsync();
        //await EditAsync(1);
        //await EditAsync(50);
        //await CreateAsync("testing", "testing", "testing");
        await UpdateAsync(2002, "test", "test", "test");
        //await DeleteAsync(2);
    }

    private async Task ReadAsync()
    {
        var lst = await _service.GetBlogs();
        foreach (var item in lst)
        {
            Console.WriteLine($"Id => {item.BlogId}");
            Console.WriteLine($"BlogTitle => {item.BlogTitle}");
            Console.WriteLine($"BlogAuthor => {item.BlogAuthor}");
            Console.WriteLine($"BlogContent => {item.BlogContent}");
            Console.WriteLine("--------------------------------------");
        }

    }
    private async Task EditAsync(int id)
    {
        try
        {
            var item = await _service.GetBlog(id);
            Console.WriteLine($"Id => {item.BlogId}");
            Console.WriteLine($"BlogTitle => {item.BlogTitle}");
            Console.WriteLine($"BlogAuthor => {item.BlogAuthor}");
            Console.WriteLine($"BlogContent => {item.BlogContent}");
            Console.WriteLine("--------------------------------------");
        }
        catch (ApiException ex)
        {
            Console.WriteLine(ex.StatusCode.ToString());
            Console.WriteLine(ex.Content);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private async Task CreateAsync(string title, string author, string content)
    {
        BlogModel blog = new BlogModel()
        {
            BlogTitle = title,
            BlogAuthor = author,
            BlogContent = content
        };

        var message = await _service.CreateBlog(blog);
        Console.WriteLine(message);
    }

    private async Task UpdateAsync(int id, [Body] string title, [Body] string author, [Body] string content)
    {
        try
        {
            BlogModel blog = new BlogModel()
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };

            var message = await _service.UpdateBlog(id, blog);
            Console.WriteLine(message);
        }
        catch (ApiException ex)
        {
            Console.WriteLine(ex.StatusCode.ToString());
            Console.WriteLine(ex.Message);
        }
        catch (Exception ex)
        {
            ; Console.WriteLine(ex.ToString());
        }

    }

    private async Task DeleteAsync(int id)
    {
        try
        {
            var message = await _service.DeleteBlog(id);
            Console.WriteLine(message);
        }
        catch (ApiException ex)
        {
            Console.WriteLine(ex.StatusCode.ToString());
            Console.WriteLine(ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

    }
}

