    using System.Runtime.Loader;
    using BookShelfter.Domain;
    using BookShelfter.Domain.Entities;
    using BookShelfter.Domain.Entities.Common;
    using BookShelfter.Domain.Entities.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    namespace BookShelfter.Persistence.Contexts;

    public class BookShelfterDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public BookShelfterDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Domain.Entities.Book> Books { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Domain.Entities.File> Files { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<Domain.Entities.Category> Categories { get; set; }

        public DbSet<CompletedOrder> CompletedOrders { get; set; }
        public DbSet<InvoiceFile> InvoiceFiles { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<ProductImageFile> ProductImageFiles { get; set; }
        public DbSet<Reviews> Reviews { get; set; }

        public DbSet<Language> Languages { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<AppUser>()
                .HasOne(u => u.Customer)
                .WithOne(c => c.AppUser)
                .HasForeignKey<Customer>(c => c.AppUserId);




            builder.Entity<Order>()
                .HasKey(b => b.Id);


            builder.Entity<Order>()
                .HasIndex(o => o.OrderCode);

            builder.Entity<Domain.Entities.Category>()
                .HasIndex(c => c.CategoryName)
                .IsUnique();


            builder.Entity<Domain.Entities.Book>()
                .HasOne(b => b.Category)
                .WithMany(c => c.Books)
                .HasForeignKey(b => b.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);


            builder.Entity< Domain.Entities.Book>()
                .HasOne(b => b.Language)
                .WithMany(l=>l.Books)
                .HasForeignKey(b=>b.LanguageId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.Entity<Basket>()
                .HasMany(b => b.BasketItems)
                .WithOne(bi => bi.Basket)
                .HasForeignKey(bi => bi.BasketId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Basket>()
                .HasOne(b => b.order)
                .WithOne(o => o.Basket)
                .HasForeignKey<Order>(b => b.BasketId);




            builder.Entity<BasketItem>()
                .HasOne(b=>b.Book)
                .WithMany(b=>b.BasketItems)
                .HasForeignKey(bi=>bi.BookId)
                .OnDelete(DeleteBehavior.Cascade);


            builder.Entity<Order>()
                .HasOne(o => o.CompletedOrder)
                .WithOne(c => c.Order)
                .HasForeignKey<CompletedOrder>(c => c.OrderId);

            builder.Entity<Reviews>()
                .HasOne<Domain.Entities.Book>(r => r.Book)
                .WithMany(b => b.Reviews)
                .HasForeignKey(r => r.BookID);
            
            

            builder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Order>()
                .HasKey(o => o.Id);

            builder.Entity<Order>()
                .HasMany(o => o.OrderDetails)
                .WithOne(od => od.Order)
                .HasForeignKey(od => od.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Order>()
                .HasOne(o => o.Basket)
                .WithOne(b => b.order)
                .HasForeignKey<Basket>(b => b.OrderId);

        builder.Entity<OrderDetails>()
                .HasKey(od => od.Id);


        builder.Entity<AppUser>()
                .HasIndex(u => u.Email)
                .IsUnique();


            builder.Entity<Reviews>()
                .HasIndex(r => new { r.BookID, r.UserID })
                .IsUnique();
            

            base.OnModelCreating(builder);







            base.OnModelCreating(builder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<BaseEntity>();


            foreach (var entry in entries)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.UtcNow;
                        entry.Entity.UpdatedDate = DateTime.UtcNow;
                        break;

                    case EntityState.Modified:
                        entry.Entity.UpdatedDate = DateTime.UtcNow;
                        break;
                }
            }

            //var datas = ChangeTracker.Entries<BaseEntity>();

            //foreach (var data in datas)
            //{
            //  _=data.State switch
            //    {
            //        EntityState.Added => data.Entity.CreatedDate = DateTime.UtcNow,
            //        EntityState.Modified => data.Entity.UpdatedDate = DateTime.UtcNow,
            //        _ => DateTime.UtcNow
            //    };
            //}


            return base.SaveChangesAsync(cancellationToken);
        }
    }