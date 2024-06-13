using System;
namespace HAHDotNetCore.ConsoleApp;

internal class EFCoreExample
{

    private readonly AppDbContext appDbContext = new AppDbContext();

    public void Run()
    {
        //Read();
        //RetrieveDataById(1);
        //RetrieveDataById(20);
        Create("test 1008", "test author", "test content");
    }

    private void Read()
    {
        var lists = appDbContext.Blogs.ToList();

        foreach (BlogDto item in lists)
        {
            Console.WriteLine($"BlogId => {item.BlogId}");
            Console.WriteLine($"BlogTitle => {item.BlogTitle}");
            Console.WriteLine($"BlogAuthor => {item.BlogAuthor}");
            Console.WriteLine($"BlogContent => {item.BlogContent}");
            Console.WriteLine("-------------------------------------");
        }
    }

    private void RetrieveDataById(int id)
    {
        var item = appDbContext.Blogs.FirstOrDefault(x => x.BlogId == id);

        if (item is null)
        {
            Console.WriteLine("Data Not Found in table.");
            return;
        }

        Console.WriteLine($"Blog Id => {item.BlogId}");
        Console.WriteLine($"Blog Title => {item.BlogTitle}");
        Console.WriteLine($"Blog Author => {item.BlogAuthor}");
        Console.WriteLine($"Blog Content => {item.BlogContent}");
        Console.WriteLine("---------------------------------------");
    }

    private void Create(string title, string author, string content)
    {
        var item = new BlogDto()
        {
            BlogTitle = title,
            BlogAuthor = author,
            BlogContent = content
        };

        appDbContext.Blogs.Add(item);
        int result = appDbContext.SaveChanges();

        string message = result > 0 ? "Insert Data Successfully." : "Insert Data Fail.";
        Console.WriteLine(message);
    }
}

