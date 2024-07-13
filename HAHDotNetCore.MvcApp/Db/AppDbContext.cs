using System;
using HAHDotNetCore.MvcApp.Models;
using Microsoft.EntityFrameworkCore;

namespace HAHDotNetCore.MvcApp.Db;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<BlogModel> Blogs { get; set; }
}

