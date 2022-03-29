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
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<Rating> Ratings { get; set; }
        public virtual DbSet<TrackingOrder> TrackingOrders { get; set; }
        #region Required
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brand>()
                .HasMany(e => e.Products)
                .WithOne(e => e.Brand)
                .OnDelete(DeleteBehavior.ClientCascade);




            modelBuilder.Entity<Order>()
                .Property(e => e.Total);
            //.HasPrecision(2, 0);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.OrderDetails)
                .WithOne(e => e.Order)
                .HasForeignKey(e => e.OrderId)
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<Payment>()
                .HasMany(e => e.Orders)
                .WithOne(e => e.Payment)
                .HasForeignKey(e => e.PaymentId)
               .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<Product>()
                .Property(e => e.Product_Price);
            //.HasPrecision(2, 0);

            modelBuilder.Entity<Product>()
                .Property(e => e.Del_Price);
            //.HasPrecision(2, 0);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.OrderDetails)
                .WithOne(e => e.Product)
                .HasForeignKey(e => e.ProductId)
              .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Transactionns)
                .WithOne(e => e.Product)
                .HasForeignKey(e => e.ProdIdTransaction);

            modelBuilder.Entity<Product>()
               .HasMany(e => e.Carts)
               .WithOne(e => e.Product)
               .HasForeignKey(e => e.ProductId);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Orders)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                 .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Transactions)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserIdTransaction);
            modelBuilder.Entity<User>()
                .HasMany(e => e.Carts)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId);
        }
        #endregion
    }
}
