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
                        new Entity.Tag { Text = "Doğal yaşam" },
                        new Entity.Tag { Text = "Sanat", Url = "Sanat"},
                        new Entity.Tag { Text = "Kültür gezileri" , Url = "Kültür gezileri"},
                        new Entity.Tag { Text = "Çalışma" , Url = "Çalışma"},
                        new Entity.Tag { Text = "Eğlence" , Url = "Eğlence"},
                        new Entity.Tag { Text = "Seyahat", Url = "Seyahat"}
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
                            Title = "Sokaklar",
                            Content = "seyahatler eğlencelidir",
                            Url = "sokaklar",
                            IsActive = true,
                            PublishedOn = DateTime.Now.AddDays(-10),
                            Image = "1.jpg",
                            Tags = context.Tags.Take(3).ToList(),
                            UserId = 1,
                        },
                            new Entity.Post
                            {
                                Title = "Çini işçiliği",
                                Content = "sanatla uğraşmak",
                                Url = "cini-isciliği",
                                IsActive = true,
                                PublishedOn = DateTime.Now.AddDays(-20),
                                Image = "2.jpg",
                                Tags = context.Tags.Take(2).ToList(),
                                UserId = 2,
                            },
                            new Entity.Post
                            {
                                Title = "Papağanlar",
                                Content = "Hayvanlae alemi",
                                Url = "papağanlar",
                                IsActive = true,
                                PublishedOn = DateTime.Now.AddDays(-15),
                                Image = "3.jpg",
                                Tags = context.Tags.Take(1).ToList(),
                                UserId = 3,
                            },
                    new Entity.Post
                        {
                            Title = "Work",
                            Content = "pazartesi sendromu",
                            Url = "work",
                            IsActive = true,
                            PublishedOn = DateTime.Now.AddDays(-5),
                            Image = "3.jpg",
                            Tags = context.Tags.Take(1).ToList(),
                            UserId = 3,
                        },
                    new Entity.Post
                        {
                            Title = "eğitim",
                            Content = "aktivite",
                            Url = "eğitim",
                            IsActive = true,
                            PublishedOn = DateTime.Now.AddDays(-1),
                            Image = "2.jpg",
                            Tags = context.Tags.Take(1).ToList(),
                            UserId = 1,
                        },
                        
                    new Entity.Post
                        {
                            Title = "aşçılık",
                            Content = "yemek tarifleri",
                            Url = "aşçılık",
                            IsActive = true,
                            PublishedOn = DateTime.Now.AddDays(-28),
                            Image = "2.jpg",
                            Tags = context.Tags.Take(1).ToList(),
                            UserId = 2,
                        }
                    );
                    context.SaveChanges();


                }

            }
        }
    }
}