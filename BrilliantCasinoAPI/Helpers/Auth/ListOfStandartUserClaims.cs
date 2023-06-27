using System.Security.Claims;

namespace BrilliantCasinoAPI.Helpers.Auth;
public static class ListOfStandartUserClaims
{
    public static IEnumerable<Claim> Claims()
    {
        return new List<Claim>
        {
            new Claim(ClaimTypes.Role,"User"),
            new Claim(ClaimTypes.Role,"SlotsPlayer")
        };
    }
}