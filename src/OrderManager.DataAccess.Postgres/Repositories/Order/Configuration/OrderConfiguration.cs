using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderManager.Domain.Order;

namespace OrderManager.DataAccess.Postgres.Repositories.Configuration;

public sealed class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        var navigation = builder.Metadata.FindNavigation(nameof(Order.OrderItems));
        
        navigation?.SetPropertyAccessMode(PropertyAccessMode.Field);
        
        builder.ToTable("orders");
        
        builder.HasKey(m => m.AggregateRootId);
        
        builder.Property(m => m.Number)
            .HasMaxLength(256)
            .IsRequired();

        builder.HasIndex(o => new { o.ProviderId, o.Number }).IsUnique();

        builder.HasMany(o => o.OrderItems)
            .WithOne()
            .HasForeignKey("order_id")
            .HasPrincipalKey(i => i.AggregateRootId);
    }
}