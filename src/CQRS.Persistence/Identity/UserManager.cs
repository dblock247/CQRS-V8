using System.Security.Claims;
using CQRS.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CQRS.Persistence.Identity;

public class UserManager : UserManager<User>
{
    public UserManager(IUserStore<User> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<User> passwordHasher, IEnumerable<IUserValidator<User>> userValidators, IEnumerable<IPasswordValidator<User>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
    {
    }

    public async Task<User?> FindByIdAsync(Guid userId)
    {
        return await base.FindByIdAsync($"{userId}");
    }

    public override Task<User?> GetUserAsync(ClaimsPrincipal principal)
    {
        var id = GetUserId(principal);
        if (id is null) return Task.FromResult((User?)null);

        return Guid.TryParse(id, out _)
            ? FindByIdAsync(id)
            : FindByNameAsync(id);
    }
}
