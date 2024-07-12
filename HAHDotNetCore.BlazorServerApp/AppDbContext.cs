using System;
using HAHDotNetCore.BlazorServerApp.Models;
using Microsoft.EntityFrameworkCore;

namespace HAHDotNetCore.BlazorServerApp;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    public DbSet<BlogModel> Blogs { get; set; }
}