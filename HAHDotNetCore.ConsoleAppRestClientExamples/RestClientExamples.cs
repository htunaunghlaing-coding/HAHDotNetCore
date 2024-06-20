using System;
using Newtonsoft.Json;
using RestSharp;

namespace HAHDotNetCore.ConsoleAppRestClientExamples;

internal class RestClientExamples
{
    private readonly RestClient _restClient = new RestClient(new Uri("https://localhost:7034"));
    private readonly string _blogEndpoint = "api/blog";

    public async Task RunAsync()
    {
        await ReadBlogAsync();
    }

    private async Task ReadBlogAsync()
    {
        RestRequest restRequest = new RestRequest(_blogEndpoint);
        var response = await _restClient.GetAsync(restRequest);
        if (response.IsSuccessStatusCode)
        {
            string JsonStr = response.Content!;
            Console.WriteLine(JsonStr);
            List<BlogModel> lst = JsonConvert.DeserializeObject<List<BlogModel>>(JsonStr)!;
            foreach (var item in lst)
            {
                Console.WriteLine(JsonConvert.SerializeObject(item));
                Console.WriteLine($"BlogTitle => {item.BlogTitle}");
                Console.WriteLine($"BlogAuthor => {item.BlogAuthor}");
                Console.WriteLine($"BlogTitle => {item.BlogContent}");
            }
        }
    }

    private async Task EditAsync(int id)
    {
        RestRequest restRequest = new RestRequest($"{_blogEndpoint}/{id}", Method.Get);
        var response = await _restClient.ExecuteAsync(restRequest);
        if (response.IsSuccessStatusCode)
        {
            var JsonStr = response.Content!;
            var item = JsonConvert.DeserializeObject<BlogModel>(JsonStr)!;
            Console.WriteLine(JsonConvert.SerializeObject(item));
            Console.WriteLine($"BlogTitle => {item.BlogTitle}");
            Console.WriteLine($"BlogAuthor => {item.BlogAuthor}");
            Console.WriteLine($"BlogContent +> {item.BlogContent}");
        }
        else
        {
            string message = response.Content!;
            Console.WriteLine(message);
        }
    }

    private async Task CreateAsync(string title, string author, string content)
    {
        BlogModel blogModel = new BlogModel
        {
            BlogTitle = title,
            BlogAuthor = author,
            BlogContent = content
        };
        RestRequest restRequest = new RestRequest(_blogEndpoint);
        restRequest.AddJsonBody(blogModel);
        var response = await _restClient.PostAsync(restRequest);
        if (response.IsSuccessStatusCode)
        {
            string message = response.Content!;
            Console.WriteLine(message);
        }
    }

    private async Task UpdateAsync(int id, string title, string author, string content)
    {
        BlogModel blogModel = new BlogModel
        {
            BlogTitle = title,
            BlogAuthor = author,
            BlogContent = content
        };

        RestRequest restRequest = new RestRequest($"{_blogEndpoint}/{id}", Method.Put);
        restRequest.AddJsonBody(blogModel);
        var response = await _restClient.ExecuteAsync(restRequest)!;
        if (response.IsSuccessStatusCode)
        {
            string message = response.Content!;
            Console.WriteLine(message);
        }
    }

    private async Task DeleteAsynceAsync(int id)
    {
        RestRequest restRequest = new RestRequest($"{_blogEndpoint}/{id}", Method.Delete);
        var resposne = await _restClient.ExecuteAsync(restRequest);
        if (resposne.IsSuccessStatusCode)
        {
            string message = resposne.Content!;
            Console.WriteLine(message);
        }
        else
        {
            string mesage = resposne.Content!;
            Console.WriteLine(mesage);
        }
    }
}

