using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Services.Organization.ActionFilters;

public class AuthorizeOwner : TypeFilterAttribute
{
    public AuthorizeOwner() : base(typeof(OrganizationOwnerFilterAttribute))
    { }
}