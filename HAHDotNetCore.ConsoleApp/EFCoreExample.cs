using System;
namespace HAHDotNetCore.ConsoleApp
{
    internal class EFCoreExample
    {
        public void Run()
        {
            Read();
        }

        private void Read()
        {
            AppDbContext appDbContext = new AppDbContext();
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
    }
}

