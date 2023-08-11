using CQRS.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace CQRS.Persistence.Identity;

public class RoleManager : RoleManager<Role>
{
    public RoleManager(IRoleStore<Role> store, IEnumerable<IRoleValidator<Role>> roleValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, ILogger<RoleManager> logger) : base(store, roleValidators, keyNormalizer, errors, logger)
    {
    }
}
