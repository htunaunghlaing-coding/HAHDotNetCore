using System.ComponentModel.DataAnnotations;
using HAHDotNetCore.ConsoleAppHttpClientExamples;
using Newtonsoft.Json;

Console.WriteLine("Hello, World!");

HttpClientExamples httpClientExamples = new HttpClientExamples();
await httpClientExamples.RunAsync();
