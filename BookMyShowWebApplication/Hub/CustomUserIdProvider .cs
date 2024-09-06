using System.Security.Claims;
using Microsoft.AspNetCore.SignalR;

public class CustomUserIdProvider : IUserIdProvider
{
    public string? GetUserId(HubConnectionContext connection)
    {
            var claimsPrincipal = connection.User;
            var userIdClaim = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier);
            return userIdClaim?.Value;
    }
 }
