
using Microsoft.AspNetCore.Authorization;

namespace alphaBeta.Helpers.Auth
{
    public class HasClaimRequirement : IAuthorizationRequirement
    {
        public string ClaimType { get; }
        public string ClaimValue { get; }

        public HasClaimRequirement(string claimType, string claimValue)
        {
            ClaimType = claimType;
            ClaimValue = claimValue;
        }
    }
}
