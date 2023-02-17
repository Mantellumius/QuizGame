using Microsoft.AspNetCore.Components.Authorization;

namespace QuizGame.Client.Providers;

public static class AuthenticationStateProviderExtensions
{
    public static async Task<Guid> GetUserGuid(this AuthenticationStateProvider authenticationStateProvider)
    {
        var state = await authenticationStateProvider.GetAuthenticationStateAsync();
        var claims = state.User.Claims;
        var nameIdentifierClaim = claims.SingleOrDefault(claim => claim.Type.Contains("nameidentifier"));
        if (nameIdentifierClaim is null) throw new InvalidOperationException("Can't find claim with type: nameidentifier");
        var id = Guid.Parse(nameIdentifierClaim.Value);
        return id;
    }
}