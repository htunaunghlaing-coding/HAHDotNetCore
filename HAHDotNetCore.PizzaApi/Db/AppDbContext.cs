using System;
using HAHDotNetCore.PizzaApi.Models;
using HAHDotNetCore.PizzaApi.Settings;
using Microsoft.EntityFrameworkCore;

namespace HAHDotNetCore.PizzaApi.Db;

internal class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(SqlConnectionStrings.stringBuilder.ConnectionString);
    }

    public DbSet<PizzaModel> Pizzas { get; set; }

    public DbSet<PizzaExtraModel> pizzaExtras { get; set; }

    public DbSet<PizzaOrderModel> pizzaOrderModels { get; set; }

    public DbSet<PizzaOrderDetailModel> pizzaOrderDetails { get; set; }
}

