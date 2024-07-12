using System;
using Microsoft.EntityFrameworkCore;

namespace HAHDotNetCore.BlazorServerApp.Db;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    public DbSet<BlogModel> Blogs { get; set; }
}

