using Marketplace.Services.Identity.Entities;

namespace Marketplace.Services.Identity.Interfaces;

public interface IJwtManager
{
    string CreateJwtToken(User user);
}