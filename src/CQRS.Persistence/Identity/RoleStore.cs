using System.Security.Claims;
using CQRS.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CQRS.Persistence.Identity;

public class RoleStore : RoleStore<Role, CQRSContext, Guid, UserRole, RoleClaim>
{
    public RoleStore(CQRSContext context, IdentityErrorDescriber? describer = null) : base(context, describer)
    {
        AutoSaveChanges = false;
    }

    protected override RoleClaim CreateRoleClaim(Role role, Claim claim)
    {
        return new RoleClaim
        {
            RoleId = role.Id,
            ClaimType = claim.Type,
            ClaimValue = claim.Value
        };
    }
}
