using System;
namespace HAHDotNetCore.HttpClientTesting;

public class HttpClientTester
{
    private readonly HttpClient _httpClient;

    public HttpClientTester()
    {
        _httpClient = new HttpClient();
    }

    public async Task<string> SendRequestHeaderAsync(string url)
    {
        _httpClient.DefaultRequestHeaders.Clear();
        _httpClient.DefaultRequestHeaders.Add("Custom_Header", "Header Value");
        var response = await _httpClient.GetAsync(url);
        return await response.Content.ReadAsStringAsync();
    }

    public async Task<string> SendRequestWithoutHeaderAsync(string url)
    {
        _httpClient.DefaultRequestHeaders.Clear();
        var response = await _httpClient.GetAsync(url);
        return await response.Content.ReadAsStringAsync();
    }

    public async Task<string> SendRequestWithoutClearRequestHeader(string url)
    {
        var response = await _httpClient.GetAsync(url);
        return await response.Content.ReadAsStringAsync();
    }


}

