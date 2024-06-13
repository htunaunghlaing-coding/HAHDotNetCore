using System;
using Microsoft.EntityFrameworkCore;

namespace HAHDotNetCore.ConsoleApp
{

	internal class AppDbContext : DbContext
	{
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(SqlConnectionString.stringBuilder.ConnectionString);
        }

        public DbSet<BlogDto> Blogs { get; set; }
	}
}

