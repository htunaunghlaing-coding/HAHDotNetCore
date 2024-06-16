using System;

internal class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(SqlConnectionStrings.sqlConnection.ConnectionString);
    }
    public DbSet<BlogModel> Blogs { get; set; }
}

