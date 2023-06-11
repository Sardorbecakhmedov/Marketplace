using Marketplace.Common.Helper;
using Marketplace.Services.Organization.Entities;
using Marketplace.Services.Organization.OrganizationContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Services.Organization.ActionFilters;

public class OrganizationOwnerFilterAttribute : ActionFilterAttribute
{
    private readonly OrganizationDbContext _dbContext;
    private readonly UserProvider _userProvider;

    public OrganizationOwnerFilterAttribute(OrganizationDbContext dbContext, UserProvider userProvider)
    {
        _dbContext = dbContext;
        _userProvider = userProvider;
    }

    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var organizationIdObject = context.ActionArguments["organizationId"]?.ToString();

        if (organizationIdObject != null)
        {
            Guid organizationId = Guid.Parse(organizationIdObject);

            var organization = await _dbContext.Organizations
                .Include(org => org.OrganizationUsers)
                .FirstOrDefaultAsync(org => org.Id == organizationId && !org.IsDeleted);

            if (organization is not null && organization.OrganizationUsers is not null)
            {
                var isUserRoleOwner = organization.OrganizationUsers.Any(user =>
                    user.UserId == _userProvider.UserId && user.OrganizationUserRole == OrganizationUserRole.Owner);

                if (!isUserRoleOwner)
                {
                    context.Result = new ForbidResult("User role, not owner!");
                }
            }
        }
        else
        {
            context.Result = new NotFoundObjectResult("ActionArguments not found!");
        }
    }
}