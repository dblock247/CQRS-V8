using System.Security.Claims;
using CQRS.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CQRS.Persistence.Identity;

public class UserStore : UserStore<User, Role, CQRSContext, Guid, UserClaim, UserRole, UserLogin, UserToken, RoleClaim>
{
    public UserStore(CQRSContext context, IdentityErrorDescriber? describer = null) : base(context, describer)
    {
        AutoSaveChanges = true;
    }

    protected override UserRole CreateUserRole(User user, Role role)
    {
        return new UserRole
        {
            UserId = user.Id,
            RoleId = role.Id
        };
    }

    protected override UserClaim CreateUserClaim(User user, Claim claim)
    {
        return new UserClaim
        {
            UserId = user.Id,
            ClaimType = claim.Type,
            ClaimValue = claim.Value
        };
    }

    protected override UserLogin CreateUserLogin(User user, UserLoginInfo login)
    {
        return new UserLogin
        {
            UserId = user.Id,
            ProviderKey = login.ProviderKey,
            LoginProvider = login.LoginProvider,
            ProviderDisplayName = login.ProviderDisplayName
        };
    }

    protected override UserToken CreateUserToken(User user, string loginProvider, string name, string? value)
    {
        return new UserToken
        {
            UserId = user.Id,
            LoginProvider = loginProvider,
            Name = name,
            Value = value
        };
    }
}
