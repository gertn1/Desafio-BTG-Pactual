using Microsoft.EntityFrameworkCore;
using Project_BTG_Pactual_Api.Entities;

namespace OrderMS.Data;
public class OrderDbContext : DbContext
{
    public OrderDbContext(DbContextOptions<OrderDbContext> options)
    : base(options) { }

    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

    protected override void OnModelCreating(ModelBuilder model)
    {
        model.Entity<Order>(e => {
            e.HasKey(o => o.Id);
            e.Property(o => o.CodigoPedido).IsRequired();
            e.HasMany(o => o.Items)
             .WithOne(i => i.Order)
             .HasForeignKey(i => i.OrderId);
        });
        model.Entity<OrderItem>(e => {
            e.HasKey(i => i.Id);
            e.Property(i => i.Product).HasMaxLength(200).IsRequired();
        });
    }
}