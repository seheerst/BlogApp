using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApp.Entity;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Data.Concrete.EfCore
{
    public class BlogContext : DbContext
    {

        public BlogContext(DbContextOptions<BlogContext> options) : base(options)
        {

        }


        public DbSet<Post> Posts => Set<Post>();

        public DbSet<Tag> Tags => Set<Tag>();

        public DbSet<User> Users => Set<User>();

        public DbSet<Comment> Comments => Set<Comment>();
    }
}