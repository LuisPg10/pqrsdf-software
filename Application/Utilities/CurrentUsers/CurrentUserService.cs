using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Application.Utilities.CurrentUsers;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string GetCurrentUserName()
    {
        var user = _httpContextAccessor.HttpContext?.User;
        if (user == null || !user.Identity?.IsAuthenticated == true)
            return "system";

        var userName = user.FindFirst(ClaimTypes.Name)?.Value;
        if (string.IsNullOrEmpty(userName))
            userName = user.Identity?.Name;
        if (string.IsNullOrEmpty(userName))
            userName = user.FindFirst("userName")?.Value;
        if (string.IsNullOrEmpty(userName))
            userName = "system";

        return userName;
    }

    public Guid? GetCurrentUserId()
    {
        var user = _httpContextAccessor.HttpContext?.User;
        if (user == null || !user.Identity?.IsAuthenticated == true)
            return null;

        var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdClaim))
            userIdClaim = user.FindFirst("userId")?.Value;

        if (Guid.TryParse(userIdClaim, out var userId))
            return userId;

        return null;
    }

    public string GetCurrentUserRole()
    {
        var user = _httpContextAccessor.HttpContext?.User;
        if (user == null || !user.Identity?.IsAuthenticated == true)
            return string.Empty;

        var role = user.FindFirst(ClaimTypes.Role)?.Value;
        if (string.IsNullOrEmpty(role))
            role = user.FindFirst("role")?.Value;

        return role ?? string.Empty;
    }

    public bool IsAuthenticated()
    {
        return _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
    }
}