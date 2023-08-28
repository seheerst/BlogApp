using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete;
using BlogApp.Data.Concrete.EfCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<BlogContext>(options =>
{
    options.UseSqlite(builder.Configuration["ConnectionStrings:sql_connection"]);

    // var version = new MySqlServerVersion(new Version(8, 0, 34));
    // options.UseMySql(connectionString, version);
});

builder.Services.AddScoped<IPostRepository, EfPostRepository>();
builder.Services.AddScoped<ITagRepository, EfTagRepository>();

var app = builder.Build();
app.UseStaticFiles();

SeedData.InitialData(app);

app.MapControllerRoute(
    name: "default",
    pattern:"{controller=home}/{action=Index}/{id?}"
    );

app.MapControllerRoute(
    name: "post_detail",
    pattern:"posts/{url}",
    defaults: new {controller="Post", action = "Details",}
);

app.Run();
