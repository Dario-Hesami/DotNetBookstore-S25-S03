using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DotNetBookstore.Models;

namespace DotNetBookstore.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        // DbSets for for all entities - these represent the tables in the database

        public DbSet<Category> Categories
        {
            get; set;
        }

        public DbSet<Book> Books
        {
            get; set;
        }

        public DbSet<CartItem> CartItems
        {
            get; set;
        }

        public DbSet<Order> Orders
        {
            get; set;
        }

        public DbSet<OrderDetail> OrderDetails
        {
            get; set;
        }



        /*

        // Optional: Fluent API configuration (if needed later)

        // Note:

        // In this current project, it is not strictly necessary to configure relationships in OnModelCreating()

        // because they are already fully defined using data annotations and clear navigation + foreign key properties.

        // This Fluent API setup would be redundant unless you:

        // - Encounter special cases (e.g., composite keys, cascade delete rules, table splitting),

        // - Or prefer to centralize configuration logic outside of model classes.

        // You can safely skip OnModelCreating unless one of those needs arises.





        protected override void OnModelCreating(ModelBuilder modelBuilder)

        {

          base.OnModelCreating(modelBuilder);



          // If needed: explicitly define relationships, define composite primary keys, enforce unique constraints or performance tuning, seed the database with initial data, set default values for properties, control cascade delete and referential actions, etc.



          // For example:



          // Seeding initial data for Category

          modelBuilder.Entity<Category>().HasData(

            new Category { CategoryId = 1, Name = "Fiction" },

            new Category { CategoryId = 2, Name = "Non-Fiction" },

            new Category { CategoryId = 3, Name = "Science" },

            new Category { CategoryId = 4, Name = "Technology" },

            new Category { CategoryId = 5, Name = "Children" }

          );



          // Book must have a Category (required FK relationship)

          modelBuilder.Entity<Book>()

            .HasOne(b => b.Category)

            .WithMany(c => c.Books)

            .HasForeignKey(b => b.CategoryId)

            .IsRequired();

        }

        */
    }
}
