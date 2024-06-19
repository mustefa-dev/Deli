
using AutoMapper;
using Deli.Repository;
using Deli.Services;
using Deli.DATA.DTOs;
using Deli.Entities;
using Deli.Interface;

namespace Deli.Services;


public interface IPackageServices
{
Task<(Package? package, string? error)> Create(PackageForm packageForm );
Task<(List<PackageDto> packages, int? totalCount, string? error)> GetAll(PackageFilter filter);
//getbyid
Task<(Package? package, string? error)> GetById(Guid id);
Task<(Package? package, string? error)> Update(Guid id , PackageUpdate packageUpdate);
Task<(Package? package, string? error)> Delete(Guid id);
}

public class PackageServices : IPackageServices
{
    private readonly IMapper _mapper;
    private readonly IRepositoryWrapper _repositoryWrapper;

    public PackageServices(IMapper mapper, IRepositoryWrapper repositoryWrapper)
    {
        _mapper = mapper;
        _repositoryWrapper = repositoryWrapper;
    }

    public async Task<(Package? package, string? error)> Create(PackageForm packageForm)
    {
        var package = _mapper.Map<Package>(packageForm);

        foreach (var itemId in packageForm.ItemIds)
        {
            var item = await _repositoryWrapper.Item.GetById(itemId);
            if (item != null)
            {
                package.Items!.Add(item);
            }
        }

        var createdPackage = await _repositoryWrapper.Package.Add(package);
        if (createdPackage == null) return (null, "Error in Creating a Package");

        return (createdPackage, null);
    }

    public async Task<(List<PackageDto> packages, int? totalCount, string? error)> GetAll(PackageFilter filter)
    {
        var packages = await _repositoryWrapper.Package.GetAll<PackageDto>(filter.PageNumber, filter.PageSize);
        return (packages.data, packages.totalCount, null);
    }

    public async Task<(Package? package, string? error)> GetById(Guid id)
    {
        var   package = await _repositoryWrapper.Package.GetById(id);
        if (package == null) return (null, "Package Not Found");
        return (package, null);
    }

    public async Task<(Package? package, string? error)> Update(Guid id, PackageUpdate packageUpdate)
    {
        var package = await _repositoryWrapper.Package.GetById(id);
        if (package == null) return (null, "Package Not Found");
        _mapper.Map(packageUpdate, package);
        var updatedPackage = await _repositoryWrapper.Package.Update(package);
        if (updatedPackage == null) return (null, "Error in Updating the Package");
        return (updatedPackage, null);
    }

    public async Task<(Package? package, string? error)> Delete(Guid id)
    {
        var package = await _repositoryWrapper.Package.GetById(id);
        if (package == null) return (null, "Package Not Found");
        
        var deletedPackage = await _repositoryWrapper.Package.SoftDelete(id);
        if (deletedPackage == null) return (null, "Error in Deleting the Package");
        
        return (deletedPackage, null);
    }

}
