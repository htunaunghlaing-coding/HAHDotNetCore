// See https://aka.ms/new-console-template for more information
using HAHDotNetCore.HttpClientTesting;

Console.WriteLine("Hello, World!");

HttpClientTester httpClient = new HttpClientTester();
string urlWithHeader = "https://httpbin.org/headers"; 
string urlWithoutHeader = "https://jsonplaceholder.typicode.com/posts";

Console.WriteLine("Sending request to the first URL with custom header...");
string responseWithHeader = await httpClient.SendRequestHeaderAsync(urlWithHeader);
Console.WriteLine("Response from first URL:");
Console.WriteLine(responseWithHeader);

Console.WriteLine("\nSending request to the second URL without clearing headers...");
string responseWithoutClearingHeader = await httpClient.SendRequestWithoutHeaderAsync(urlWithoutHeader);
Console.WriteLine("Response from second URL without clearing headers:");
Console.WriteLine(responseWithoutClearingHeader);

Console.WriteLine("\nSending request to the second URL after clearing headers...");
string responseAfterClearingHeader = await httpClient.SendRequestWithoutClearRequestHeader(urlWithoutHeader);
Console.WriteLine("Response from second URL after clearing headers:");
Console.WriteLine(responseAfterClearingHeader);



