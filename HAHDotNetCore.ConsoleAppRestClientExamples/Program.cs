using HAHDotNetCore.ConsoleAppRestClientExamples;

Console.WriteLine("Hello, World!");


RestClientExamples restClient = new RestClientExamples();
await restClient.RunAsync();
