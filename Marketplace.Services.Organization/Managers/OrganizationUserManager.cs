using Marketplace.Services.Organization.Entities;
using Marketplace.Services.Organization.Interfaces;
using Marketplace.Services.Organization.Models.CreateModels;
using Marketplace.Services.Organization.Models.UpdateModels;
using Marketplace.Services.Organization.OrganizationContext;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Services.Organization.Managers;

public class OrganizationUserManager : IOrganizationUserManager
{
    private readonly OrganizationDbContext _dbContext;

    public OrganizationUserManager(OrganizationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AddOrganizationUserModel> AddUserAsync(AddOrganizationUserModel orgUserModel)
    {
        var organization = await GetOrganizationAsync(orgUserModel.OrganizationId);

        if (organization == null)
        {
            throw new ArgumentException("Organization not found!");
        }

        var newOrgUser = new OrganizationUser
        {
             UserId = orgUserModel.UserId,
             Organization = organization,
             OrganizationId = orgUserModel.OrganizationId,
             OrganizationUserRole = orgUserModel.UserRole
        };

        await _dbContext.AddAsync(newOrgUser);

        organization.OrganizationUsers ??= new List<OrganizationUser>();

        organization.OrganizationUsers.Add(newOrgUser);
        await _dbContext.SaveChangesAsync();

        return MapToAddOrganizationUserModel(newOrgUser);
    }

    public async Task<IEnumerable<AddOrganizationUserModel>> GetOrganizationUsersAsync(Guid organizationId)
    {
        var organization = await GetOrganizationAsync(organizationId);

        if (organization is null) 
            throw new ArgumentException("Organization not found!");
        
        if (organization.OrganizationUsers is null)
            throw new NullReferenceException("OrganizationUsers is null!");
        
        return organization.OrganizationUsers.Select(MapToAddOrganizationUserModel);
    }

    public async Task<AddOrganizationUserModel> GetOrganizationUserAsync(Guid userId, Guid organizationId)
    {
        var organization = await GetOrganizationAsync(organizationId);

        if (organization is null)
            throw new ArgumentException("Organization not found!");

        if (organization.OrganizationUsers is null)
            throw new NullReferenceException("OrganizationUsers is null!");

        var user = organization.OrganizationUsers.FirstOrDefault(user => user.Id == userId);

        if (user is null)
            throw new Exception("User not found");

        return MapToAddOrganizationUserModel(user);
    }

    public async Task<AddOrganizationUserModel> UpdateOrganizationUserAsync(Guid organizationId, Guid userId,
        UpdateOrganizationUserModel model)
    {
        var organization = await GetOrganizationAsync(organizationId);

        if (organization == null)
            throw new ArgumentException("Organization not found!");
        

        if (organization.OrganizationUsers is null)
            throw new NullReferenceException("OrganizationUsers is null!");

        var user = organization.OrganizationUsers.FirstOrDefault(user => user.Id == userId);

        if (user is null)
            throw new Exception("User not found");

        user.UserId = model.UserId ?? user.UserId;
        user.OrganizationId = model.OrganizationId ?? user.OrganizationId;
        user.OrganizationUserRole = model.UserRole ?? user.OrganizationUserRole;
        user.UpdatedAt = DateTime.UtcNow;

        await _dbContext.SaveChangesAsync();

        return MapToAddOrganizationUserModel(user);
    }

    public async Task DeleteOrganizationUserAsync(Guid userId, Guid organizationId)
    {
        var organization = await GetOrganizationAsync(organizationId);

        if (organization is null)
            throw new ArgumentException("Organization not found!");

        if (organization.OrganizationUsers is null)
            throw new NullReferenceException("OrganizationUsers is null!");

        var user = organization.OrganizationUsers.FirstOrDefault(user => user.Id == userId);

        if (user is null)
        {
            throw new Exception("User not found");
        }

        _dbContext.OrganizationUsers.Remove(user);
        organization.OrganizationUsers.Remove(user);

        await _dbContext.SaveChangesAsync();
    }

    private async Task<Entities.Organization?> GetOrganizationAsync(Guid organizationId)
    {
       return await _dbContext.Organizations
            .Include(org => org.OrganizationUsers)
            .Include(org => org.OrganizationAddresses)
            .FirstOrDefaultAsync(org => org.Id == organizationId);
    }

    private AddOrganizationUserModel MapToAddOrganizationUserModel(OrganizationUser organizationUser)
    {
        return new AddOrganizationUserModel
        {
            UserId = organizationUser.UserId,
            OrganizationId = organizationUser.OrganizationId,
            UserRole = organizationUser.OrganizationUserRole,
        };
    }


}