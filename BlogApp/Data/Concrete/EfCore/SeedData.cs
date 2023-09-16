using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApp.Entity;
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
                        new Entity.Tag { Text = "Doğal yaşam", Url = "dogal-yasam", Color = TagColors.primary.ToString()},
                        new Entity.Tag { Text = "Sanat", Url = "Sanat", Color = TagColors.secondary.ToString()},
                        new Entity.Tag { Text = "Çalışma" , Url = "Çalışma", Color = TagColors.danger.ToString()},
                        new Entity.Tag { Text = "Eğlence" , Url = "Eğlence", Color = TagColors.success.ToString()},
                        new Entity.Tag { Text = "Seyahat", Url = "Seyahat", Color = TagColors.warning.ToString()}
                    );
                    context.SaveChanges();
                }

                if (!context.Users.Any())
                {
                    context.Users.AddRange(
                        new Entity.User { Name = "seherselin", Image = "p2.jpg"},
                        new Entity.User { UserName = "sadikturan" , Name="Sadık Turan",Email = "info@sadikturan.com",Password = "12345",Image = "p1.jpg"},
                        new Entity.User { UserName = "aliyilmaz", Name="Ali Yılmaz",Email = "info@aliyilmaz.com",Password = "12345", Image = "p2.jpg"}

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
                                Comments = new List<Comment>
                                {
                                    new Comment
                                    {
                                        Text = "comment",
                                        PublishedOn = new DateTime(),
                                        UserId = 1,
                                    },
                                    new Comment
                                    {
                                        Text = "comment",
                                        PublishedOn = new DateTime(),
                                        UserId = 2,
                                    }
                                }
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