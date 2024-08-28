using System.Security.Claims;
using Microsoft.AspNetCore.SignalR;

public class CustomUserIdProvider : IUserIdProvider
{
        // Implement your logic to retrieve the user ID here
        // For example, using claim-based authentication:
    public string? GetUserId(HubConnectionContext connection)
    {
            var claimsPrincipal = connection.User;
            var userIdClaim = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier);
            return userIdClaim?.Value;
    }
 }
