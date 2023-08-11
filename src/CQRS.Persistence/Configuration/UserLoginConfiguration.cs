using CQRS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CQRS.Persistence.Configuration;

public class UserLoginConfiguration : EntityConfiguration<UserLogin>
{
    public override void Configure(EntityTypeBuilder<UserLogin> builder)
    {
        base.Configure(builder);

        builder.ToTable("UserLogin");
    }
}
