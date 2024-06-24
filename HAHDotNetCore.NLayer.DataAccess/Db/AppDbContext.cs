using System;
using HAHDotNetCore.NLayer.DataAccess.Models;
using HAHDotNetCore.NLayer.DataAccess.Settings;
using Microsoft.EntityFrameworkCore;

namespace HAHDotNetCore.NLayer.DataAccess.Db;

internal class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(SqlConnectionStrings.sqlBuilder.ConnectionString);
    }

    public DbSet<BlogModel> Blogs { get; set; }
}

