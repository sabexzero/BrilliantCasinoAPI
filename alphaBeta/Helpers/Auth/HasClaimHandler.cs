using Microsoft.AspNetCore.Authorization;

namespace BrilliantCasinoAPI.Helpers.Auth;
public class HasClaimHandler : AuthorizationHandler<HasClaimRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasClaimRequirement requirement)
    {
        if (context.User.HasClaim(c => c.Type == requirement.ClaimType && c.Value == requirement.ClaimValue))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}