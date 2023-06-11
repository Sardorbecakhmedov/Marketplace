using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Marketplace.Common.Helper;

public class UserProvider
{
    private readonly IHttpContextAccessor _contextAccessor;

    public UserProvider(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    private HttpContext? Context => _contextAccessor.HttpContext;
    public string Username => Context?.User.FindFirstValue(ClaimTypes.Name)!;
    public Guid UserId => Guid.Parse(Context?.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
}