using System;
using HAHDotNetCore.ConsoleApp.Settings;
using Microsoft.EntityFrameworkCore;

namespace HAHDotNetCore.ConsoleApp;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    optionsBuilder.UseSqlServer(SqlConnectionString.stringBuilder.ConnectionString);
    //}

    public DbSet<BlogDto> Blogs { get; set; }
}

