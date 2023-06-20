using System.Linq;
using System.Threading.Tasks;
using alphaBeta.Helpers.Auth;
using Microsoft.AspNetCore.Authorization;

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