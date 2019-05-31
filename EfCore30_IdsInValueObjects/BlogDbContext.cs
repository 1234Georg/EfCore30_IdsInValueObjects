using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EfCore30_IdsInValueObjects
{
    public class BlogDbContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data source=SpongeBob.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //with the next line you get a exception "The entity type 'Blog' requires a primary key to be defined"
            //modelBuilder.Entity<Blog>().OwnsOne(p => p.Id);

            //with the next line still the exception: The entity type 'Blog' requires a primary key to be defined
            //modelBuilder.Entity<Blog>().OwnsOne(b => b.Id, sb => sb.HasKey(id => id.Value));

            //with the next two lines there is the exception: "The properties expression 'b => b.Id.Value' is not valid. The expression should represent a simple property access: 't => t.MyProperty'. ..."
            //modelBuilder.Entity<Blog>().OwnsOne(b => b.Id);
            //modelBuilder.Entity<Blog>().HasKey(b => b.Id.Value);


            //with conversion it works, but it is evaluated locally
            modelBuilder.Entity<Blog>().Property(b => b.Id).HasConversion(idObject => idObject.Value, idString => new BlogId(idString));

            //same as above, the next line does not work, because Post needs an id
            //modelBuilder.Entity<Post>().OwnsOne(p => p.Id);

            //the next line works, but the following code is evaluated locally: 
            //var postsOfBlog123 = DbContext.Posts.Where(p => p.BlogId == new BlogId("123")).ToList();
            //'Error generated for warning 'Microsoft.EntityFrameworkCore.Query.QueryClientEvaluationWarning: The LINQ expression 'where ([p.BlogId] == value(EfCore30_IdsInValueObjects.BlogId))' could not be translated and will be evaluated locally.'. 
            modelBuilder.Entity<Post>().Property(b => b.Id).HasConversion(idObject => idObject.Value, idString => new PostId(idString));

            modelBuilder.Entity<Post>().OwnsOne(p => p.BlogId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
