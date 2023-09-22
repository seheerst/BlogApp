using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete;
using BlogApp.Data.Concrete.EfCore;
using Microsoft.AspNetCore.Authentication.Cookies;
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
builder.Services.AddScoped<ICommentRepository, EfCommentRepository>();
builder.Services.AddScoped<IUserRepository, EfUserRepository>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

var app = builder.Build();
app.UseStaticFiles();

SeedData.InitialData(app);

app.MapControllerRoute(
    name: "default",
    pattern:"{controller=Post}/{action=Index}/{id?}"
    );

app.UseAuthentication();
app.UseAuthorization();
app.UseRouting();

app.MapControllerRoute(
    name: "post_detail",
    pattern:"posts/detail/{url}",
    defaults: new {controller="Post", action = "Details",}
);

app.MapControllerRoute(
    name: "posts_by_tag",
    pattern:"posts/tag/{tag}",
    defaults: new {controller="Post", action = "Index",}
);

app.Run();
