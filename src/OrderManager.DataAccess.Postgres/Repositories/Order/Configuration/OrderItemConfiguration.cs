using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderManager.Domain.Order;
using OrderManager.Domain.Provider;

namespace OrderManager.DataAccess.Postgres.Repositories.Configuration;

public sealed class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("order_items");

        builder.HasKey(m => m.OrderItemId);
        builder.Property(m => m.OrderItemId)
            .ValueGeneratedNever()
            .HasColumnName("order_item_id");
        
        builder.Property(m => m.Name)
            .HasMaxLength(256)
            .IsRequired();
    }
}