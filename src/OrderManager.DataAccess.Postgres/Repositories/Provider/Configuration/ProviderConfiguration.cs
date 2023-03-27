using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderManager.Domain.Provider;


public sealed class ProviderConfiguration : IEntityTypeConfiguration<Provider>
{
    public void Configure(EntityTypeBuilder<Provider> builder)
    {
        builder.ToTable("providers");
        
        builder.HasKey(m => m.AggregateRootId);
        
        builder.Property(m => m.Name)
            .HasMaxLength(256)
            .IsRequired();
    }
}