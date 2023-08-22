using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Data.Concrete.EfCore
{
    public class SeedData
    {
        public static void InitialData(IApplicationBuilder app)
        {
            var context = app.ApplicationServices.CreateScope().ServiceProvider.GetService<BlogContext>();

            if (context != null)
            {

                if (context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                }
                if (!context.Tags.Any())
                {
                    context.Tags.AddRange(
                        new Entity.Tag { Text = "web programlama" },
                        new Entity.Tag { Text = "backend" },
                        new Entity.Tag { Text = "mobil uygulamalar" },
                        new Entity.Tag { Text = "frontend" },
                        new Entity.Tag { Text = "fullstack" },
                        new Entity.Tag { Text = "php" }
                    );
                    context.SaveChanges();
                }

                if (!context.Users.Any())
                {
                    context.Users.AddRange(
                        new Entity.User { Name = "seherselin" },
                        new Entity.User { Name = "sadikturan" },
                        new Entity.User { Name = "aliyilmaz" }

                    );
                    context.SaveChanges();
                }

                if (!context.Posts.Any())
                {
                    context.Posts.AddRange(
                        new Entity.Post
                        {
                            Title = "Asp.net core",
                            Content = "Asp.net core dersleri",
                            IsActive = true,
                            PublishedOn = DateTime.Now.AddDays(-10),
                            Tags = context.Tags.Take(3).ToList(),
                            UserId = 1,
                        },
                            new Entity.Post
                            {
                                Title = "Php",
                                Content = "Php dersleri",
                                IsActive = true,
                                PublishedOn = DateTime.Now.AddDays(-20),
                                Tags = context.Tags.Take(2).ToList(),
                                UserId = 2,
                            },
                            new Entity.Post
                            {
                                Title = "Django",
                                Content = "Django dersleri",
                                IsActive = true,
                                PublishedOn = DateTime.Now.AddDays(-15),
                                Tags = context.Tags.Take(1).ToList(),
                                UserId = 3,
                            }
                    );
                    context.SaveChanges();


                }

            }
        }
    }
}