using CQRS.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CQRS.Persistence.Configuration;

/// <summary>
/// Base Entity configuration.
/// </summary>
/// <typeparam name="T"></typeparam>
public class EntityConfiguration<T> : IEntityTypeConfiguration<T> where T : class, IEntity, IAuditable, ISoftDeletable
{
    /// <summary>
    /// Initialize new Entity configuration.
    /// </summary>
    /// <param name="builder"></param>
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.HasQueryFilter(e => e.DeletedOnUtc == null);

        builder.Property(e => e.CreatedBy)
            .IsUnicode(false)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(e => e.CreatedOnUtc)
            .IsRequired();

        builder.Property(e => e.ModifiedBy)
            .IsUnicode(false)
            .HasMaxLength(255)
            .IsRequired(false);

        builder.Property(e => e.ModifiedOnUtc)
            .IsRequired(false);
    }
}
