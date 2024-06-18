using System;
using Newtonsoft.Json;

namespace HAHDotNetCore.ConsoleAppHttpClientExamples;

internal class HttpClientExamples
{
    private readonly HttpClient _client = new HttpClient() { BaseAddress = new Uri("https://localhost:7034") };
    private readonly string _blogEndpoint = "api/blog";

    public async Task RunAsync()
    {
        await ReadAsync();
    }

    private async Task ReadAsync()
    {
        var response = await _client.GetAsync(_blogEndpoint);
        if (response.IsSuccessStatusCode)
        {
            string jsonStr = await response.Content.ReadAsStringAsync();
            Console.WriteLine(jsonStr);
            List<BlogModel> lst = JsonConvert.DeserializeObject<List<BlogModel>>(jsonStr)!;
            foreach (var blog in lst)
            {
                Console.WriteLine(JsonConvert.SerializeObject(blog));
                Console.WriteLine($"BlogTitle => {blog.BlogTitle}");
                Console.WriteLine($"BlogAuthor => {blog.BlogAuthor}");
                Console.WriteLine($"BlogContent -> {blog.BlogContent}");
            }
        }
    }
}

