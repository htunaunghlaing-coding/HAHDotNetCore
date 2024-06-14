using System;
using Microsoft.EntityFrameworkCore;

namespace HAHDotNetCore.RestApi;

internal class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(SqlConnectionString.StringBuilder.ConnectionString);
    }

    public DbSet<BlogModel> Blogs { get; set; }
}

