using Marketplace.Common.Helper;
using Marketplace.Common.Interfaces;
using Marketplace.Services.Organization.Entities;
using Marketplace.Services.Organization.Interfaces;
using Marketplace.Services.Organization.Models.CreateModels;
using Marketplace.Services.Organization.Models.UpdateModels;
using Marketplace.Services.Organization.Models.ViewModels;
using Marketplace.Services.Organization.OrganizationContext;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Services.Organization.Managers;

public class OrganizationManager : IOrganizationManager
{
    private const string FolderName = "OrganizationLogos";
    private readonly OrganizationDbContext _dbContext;
    private readonly UserProvider _userProvider;
    private readonly IFileManager _fileManager;

    public OrganizationManager(OrganizationDbContext dbContext, UserProvider userProvider, IFileManager fileManager)
    {
        _dbContext = dbContext;
        _userProvider = userProvider;
        _fileManager = fileManager;
    }

    public async Task<OrganizationViewModel> CreateAsync(CreateOrganizationModel orgModel)
    {
        var organization = new Entities.Organization
        {
            OrganizationName = orgModel.OrganizationName,
            Description = orgModel.Description,
            Email = orgModel.Email,
            PhoneNumber = orgModel.PhoneNumber,
            IsDeleted = false,
        };

        if (orgModel.LogoFile is not null)
        {
            organization.LogoPath = await _fileManager.SaveFileToWwwrootAsync(orgModel.LogoFile, FolderName);
        }

        if (orgModel.Addresses is not null)
        {
            organization.OrganizationAddresses = new List<OrganizationAddress>();
            organization.OrganizationAddresses = orgModel.Addresses.Select(model => new OrganizationAddress
            {
                Id = Guid.NewGuid(),
                OrganizationId = organization.Id,
                Organization = organization,
                Address = model.Address
            }).ToList();

            await _dbContext.OrganizationAddress.AddRangeAsync(organization.OrganizationAddresses);
        }

        var organizationUser = new OrganizationUser
        {
            OrganizationId = organization.Id,
            Organization = organization,
            UserId = _userProvider.UserId,
            OrganizationUserRole = OrganizationUserRole.Owner
        };

        organization.OrganizationUsers = new List<OrganizationUser> { organizationUser };
        await _dbContext.OrganizationUsers.AddRangeAsync(organization.OrganizationUsers);
        await _dbContext.Organizations.AddAsync(organization);
        await _dbContext.SaveChangesAsync();

        return MapToOrganizationViewModel(organization);
    }

    public async Task<OrganizationViewModel> UpdateAsync(Guid organizationId, UpdateOrganizationModel model)
    {
        var organization = await _dbContext.Organizations
            .FirstOrDefaultAsync(org => org.Id == organizationId && !org.IsDeleted);

        if (organization is null)
        {
            throw new Exception("Organization not found!");
        }

        organization.OrganizationName = model.OrganizationName ?? organization.OrganizationName;
        organization.Description = model.Description ?? organization.Description;
        organization.Email = model.Email ?? organization.Email;
        organization.PhoneNumber = model.PhoneNumber ?? organization.PhoneNumber;
        organization.LogoPath = model.LogoFilePath ?? organization.LogoPath;
        organization.UpdatedAt = DateTime.UtcNow;

        if (model.Addresses is not null)
        {
            organization.OrganizationAddresses ??= new List<OrganizationAddress>();

            for (var i = 0; i < model.Addresses.Count; i++)
            {
                organization.OrganizationAddresses[i].Address = 
                    model.Addresses[i].Address ?? organization.OrganizationAddresses[i].Address;
            }
        }
        await _dbContext.SaveChangesAsync();

        return MapToOrganizationViewModel(organization);
    }

    public Task<IEnumerable<OrganizationViewModel>> GetAllOrganizations()
    {
        var organizations =  _dbContext.Organizations
            .Include(org => org.OrganizationUsers)
            .Include(org => org.OrganizationAddresses)
            .Where(org => !org.IsDeleted)
            .ToList();

       var organizationModels = organizations.Select(MapToOrganizationViewModel);

       return Task.FromResult(organizationModels);
    }

    public async Task<OrganizationViewModel> GetOrganizationAsync(Guid organizationId)
    {
        var organization = await _dbContext.Organizations
            .Include(org => org.OrganizationUsers)
            .Include(org => org.OrganizationAddresses)
            .FirstOrDefaultAsync(org => org.Id == organizationId && !org.IsDeleted);

        if (organization is null)
        {
            throw new Exception("Organization not found!");
        }

        return MapToOrganizationViewModel(organization);
    }


    public async Task<bool> DeleteAsync(Guid organizationId)
    {
        var organization = await _dbContext.Organizations
            .FirstOrDefaultAsync(org => org.Id == organizationId && !org.IsDeleted);

        if (organization is null)
        {
            return false;
        }

        organization.IsDeleted = true;
        await _dbContext.SaveChangesAsync();

        return true;
    }


    private OrganizationViewModel MapToOrganizationViewModel(Entities.Organization model)
    {
        var organizationModel = new OrganizationViewModel
        {
            OrganizationId = model.Id,
            OrganizationName = model.OrganizationName,
            Description = model.Description,
            LogoPath = model.LogoPath,
            Email = model.Email,
            PhoneNumber = model.PhoneNumber,
        };

        if (model.OrganizationUsers is not null)
        {
            organizationModel.Users = model.OrganizationUsers.Select(user => new OrganizationViewUserModel
            {
                UserId = user.UserId, 
                UserRole = user.OrganizationUserRole
            }).ToList();
        }

        if (model.OrganizationAddresses is not null)
        {
            organizationModel.Addresses = model.OrganizationAddresses.Select(address => new OrganizationViewAddressModel
            {
                AddressId = address.Id,
                Address = address.Address,
            }).ToList();
        }

        return organizationModel;
    }
}