using System;
using Microsoft.EntityFrameworkCore;

namespace HAHDotNetCore.RestApiDependencyInjection;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    optionsBuilder.UseSqlServer(SqlConnectionString.StringBuilder.ConnectionString);
    //}

    public DbSet<BlogModel> Blogs { get; set; }
}

