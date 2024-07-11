using HAHDotNetCore.MinimalApi.Db;
using HAHDotNetCore.MinimalApi.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"));
},
ServiceLifetime.Transient,
ServiceLifetime.Transient);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/", () => "Hello World");

app.MapGet("api/Blog", async (AppDbContext db) =>
{
    var lst = await db.Blogs.AsNoTracking().ToListAsync();
    return Results.Ok(lst);
});

app.MapPost("api/Blog", async (AppDbContext db, BlogModel blog) =>
{
    await db.Blogs.AddAsync(blog);
    var result = await db.SaveChangesAsync();

    string message = result > 0 ? "Saving Data Successfully." : "Saving Data Fail.";
    return Results.Ok(message);
});

app.MapPut("api/Blog/{id}", async (AppDbContext db, int id, BlogModel blog) =>
{
    var item = await db.Blogs.FirstOrDefaultAsync(x => x.BlogId == id);
    if (item is null)
    {
        return Results.NotFound("Not found the data.");
    }

    item.BlogId = id;
    item.BlogTitle = blog.BlogTitle;
    item.BlogAuthor = blog.BlogAuthor;
    item.BlogContent = blog.BlogContent;
    var result = await db.SaveChangesAsync();

    var message = result > 0 ? "Update Data Successfully." : "Update Data Fail.";
    return Results.Ok(message);
});

app.MapDelete("api/Blog/{id}", async (AppDbContext db, int id) =>
{
    var item = await db.Blogs.FirstOrDefaultAsync(x => x.BlogId == id);
    if (item is null)
    {
        return Results.NotFound("Not found the data.");
    }

    db.Remove(item);
    var result = db.SaveChanges();

    var message = result > 0 ? "Delete Data Successfully." : "Delete Data Fail.";
    return Results.Ok(message);
});

app.Run();

//var summaries = new[]
//{
//    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
//};

//app.MapGet("/weatherforecast", () =>
//{
//    var forecast =  Enumerable.Range(1, 5).Select(index =>
//        new WeatherForecast
//        (
//            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//            Random.Shared.Next(-20, 55),
//            summaries[Random.Shared.Next(summaries.Length)]
//        ))
//        .ToArray();
//    return forecast;
//})
//.WithName("GetWeatherForecast")
//.WithOpenApi();




//record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
//{
//    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
//}

