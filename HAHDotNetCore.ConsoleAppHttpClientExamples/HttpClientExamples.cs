using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HAHDotNetCore.ConsoleAppHttpClientExamples;

internal class HttpClientExamples
{
    //public HttpClientExamples()
    //{
    //    var handler = new HttpClientHandler
    //    {
    //        ClientCertificateOptions = ClientCertificateOption.Manual,
    //        ServerCertificateCustomValidationCallback =
    //            (httpRequestMessage, cert, cetChain, policyErrors) =>
    //            {
    //                return true;
    //            }
    //    };

    //    _client = new HttpClient(handler)
    //    {
    //        BaseAddress = new Uri("https://localhost:7034")
    //    };
    //}
    private readonly HttpClient _client = new HttpClient() { BaseAddress = new Uri("https://localhost:7034") };
    private readonly string _blogEndpoint = "api/blog";

    public async Task RunAsync()
    {
        await EditAsync(1);
        await EditAsync(100);
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

    private async Task EditAsync(int id)
    {
        var response = await _client.GetAsync($"{_blogEndpoint}/{id}");
        if (response.IsSuccessStatusCode)
        {
            string jsonStr = await response.Content.ReadAsStringAsync();
            Console.WriteLine(jsonStr);
            var item = JsonConvert.DeserializeObject<BlogModel>(jsonStr)!;
            Console.WriteLine($"BlogTitle => {item.BlogTitle}");
            Console.WriteLine($"BlogAuthor => {item.BlogAuthor}");
            Console.WriteLine($"BlogContent => {item.BlogContent}");
        }
        else
        {
            string message = await response.Content.ReadAsStringAsync();
            Console.WriteLine(message);
        }
    }
}

public class BlogModel
{
    public int BlogId { get; set; }
    public string? BlogTitle { get; set; }
    public string? BlogAuthor { get; set; }
    public string? BlogContent { get; set; }
}
