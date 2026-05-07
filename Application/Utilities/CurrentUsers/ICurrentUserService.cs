namespace Application.Utilities.CurrentUsers;

public interface ICurrentUserService
{
    string GetCurrentUserName();
    Guid? GetCurrentUserId();
    string GetCurrentUserRole();
    bool IsAuthenticated();
}