using System;
using Microsoft.EntityFrameworkCore;

namespace DDSWebstore.Models
{
  public class MyDBContext : DbContext, IdentityDbContext
  {
//     protected override void OnModelCreating( ModelBuilder builder ) {
//     base.OnModelCreating( builder );
//     // Customize the ASP.NET Identity model and override the defaults if needed.
//     // For example, you can rename the ASP.NET Identity table names and more.
//     // Add your customizations after calling base.OnModelCreating(builder);

//     builder.Entity<Item>() //Use your application user class here
//            .ToTable( "Item" ); //Set the table name here
// }
    public MyDBContext(DbContextOptions<MyDBContext> options)
       : base(options)
   { }

    public DbSet<Item> Item {get; set;}
    public DbSet<Order> Order {get; set;}
    public DbSet<Image> Image {get; set;}
    public DbSet<Category> Category {get; set;}
    public DbSet<BoughtItem> BoughtItem{get; set;}
  }
}
