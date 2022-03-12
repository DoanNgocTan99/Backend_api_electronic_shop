using Microsoft.EntityFrameworkCore;

namespace WebsiteApi.Model.Entity
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {

        }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<User> Users { get; set; }
        #region Required
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brand>()
                .HasMany(e => e.Products)
                .WithOne(e => e.Brand)
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<Category>()
                .HasMany(e => e.Images)
                .WithOne(e => e.Category)
                .HasForeignKey(e => e.CateId);

            modelBuilder.Entity<Image>()
                .Property(e => e.Path)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.Total);
            //.HasPrecision(2, 0);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.OrderDetails)
                .WithOne(e => e.Order)
                .HasForeignKey(e => e.OrderIdDetail)
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<Payment>()
                .HasMany(e => e.OrderDetails)
                .WithOne(e => e.Payment)
                .HasForeignKey(e => e.PaymentIdDetail)
               .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<Product>()
                .Property(e => e.Product_Price);
            //.HasPrecision(2, 0);

            modelBuilder.Entity<Product>()
                .Property(e => e.Del_Price);
            //.HasPrecision(2, 0);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Images)
                .WithOne(e => e.Product)
                .HasForeignKey(e => e.ProId)
               .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.OrderDetails)
                .WithOne(e => e.Product)
                .HasForeignKey(e => e.ProdIdDetail)
              .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Transactionns)
                .WithOne(e => e.Product)
                .HasForeignKey(e => e.ProdIdTransaction);

            modelBuilder.Entity<User>()
                .HasMany(e => e.OrderDetails)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserIdDetail)
                 .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Transactions)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserIdTransaction);
        }
        #endregion
    }
}
