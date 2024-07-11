using System;
using HAHDotNetCore.MinimalApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HAHDotNetCore.MinimalApi.Db;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<BlogModel> Blogs { get; set; }
}
