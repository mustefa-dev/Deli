
using AutoMapper;
using Deli.Repository;
using Deli.Services;
using Deli.DATA.DTOs;
using Deli.Entities;
using Deli.Interface;
using Microsoft.EntityFrameworkCore;

namespace Deli.Services;


public interface IPackageServices
{
Task<(Package? package, string? error)> Create(PackageForm packageForm , string language );
Task<(List<PackageDto> packages, int? totalCount, string? error)> GetAll(PackageFilter filter, string language );
//getbyid
Task<(PackageDto? package, string? error)> GetById(Guid id, string language) ;
Task<(Package? package, string? error)> Update(Guid id , PackageUpdate packageUpdate, string language );
Task<(Package? package, string? error)> Delete(Guid id, string language );
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

    public async Task<(Package? package, string? error)> Create(PackageForm packageForm , string language )
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
        if (createdPackage == null) return (null,ErrorResponseException.GenerateLocalizedResponse("Error in Creating the Package", "خطأ في اضافة الحزمة", language));

        return (createdPackage, null);
    }

    public async Task<(List<PackageDto> packages, int? totalCount, string? error)> GetAll(PackageFilter filter, string language )
    {
        var packages = await _repositoryWrapper.Package.GetAll<PackageDto>(filter.PageNumber, filter.PageSize);
        foreach (var  package in packages.data)
        {    var originalpackage=await _repositoryWrapper.Package.GetById(package.Id);
            package.Name=ErrorResponseException.GenerateLocalizedResponse(originalpackage.Name, originalpackage.ArName, language);
            package.Description=ErrorResponseException.GenerateLocalizedResponse(originalpackage.Description, originalpackage.ArDescription, language);
            foreach (var itemdto in package.Items)
            {
                var originalitem=await _repositoryWrapper.Item.GetById(itemdto.Id);
                itemdto.Name=ErrorResponseException.GenerateLocalizedResponse(originalitem.Name, originalitem.ArName, language);
                itemdto.Description=ErrorResponseException.GenerateLocalizedResponse(originalitem.Description, originalitem.ArDescription, language);
                itemdto.MainDetails=ErrorResponseException.GenerateLocalizedResponse(originalitem.MainDetails, originalitem.ArMainDetails, language);
                
            }
            
                
            
        }

        
       
        return (packages.data, packages.totalCount, null);
    }

    public async Task<(PackageDto? package, string? error)> GetById(Guid id, string language )
    {
        var   package = await _repositoryWrapper.Package.Get(i=>i.Id==id, i=>i.Include(x=>x.Items));
        if (package == null) return (null, ErrorResponseException.GenerateLocalizedResponse("Package Not Found", "الحزمة غير موجودة", language));
        var packageDto = _mapper.Map<PackageDto>(package);
        foreach (var itemdto in packageDto.Items)
        {
            var originalitem=await _repositoryWrapper.Item.GetById(itemdto.Id);
            itemdto.Name=ErrorResponseException.GenerateLocalizedResponse(originalitem.Name, originalitem.ArName, language);
            itemdto.Description=ErrorResponseException.GenerateLocalizedResponse(originalitem.Description, originalitem.ArDescription, language);
            itemdto.MainDetails=ErrorResponseException.GenerateLocalizedResponse(originalitem.MainDetails, originalitem.ArMainDetails, language);
                
        }
        packageDto.Name=ErrorResponseException.GenerateLocalizedResponse(package.Name, package.ArName, language);
        packageDto.Description=ErrorResponseException.GenerateLocalizedResponse(package.Description, package.ArDescription, language);
        return (packageDto, null);
    }

    public async Task<(Package? package, string? error)> Update(Guid id, PackageUpdate packageUpdate, string language )
    {
        var package = await _repositoryWrapper.Package.GetById(id);
        if (package == null) return (null, ErrorResponseException.GenerateLocalizedResponse("Package Not Found", "الحزمة غير موجودة", language));
        _mapper.Map(packageUpdate, package);
        var updatedPackage = await _repositoryWrapper.Package.Update(package);
        if (updatedPackage == null) return (null, ErrorResponseException.GenerateLocalizedResponse("Error in Updating the Package", "خطأ في تحديث الحزمة", language));
        return (updatedPackage, null);
    }

    public async Task<(Package? package, string? error)> Delete(Guid id, string language )
    {
        var package = await _repositoryWrapper.Package.GetById(id);
        if (package == null) return (null, ErrorResponseException.GenerateLocalizedResponse("Package Not Found", "الحزمة غير موجودة", language));
        
        var deletedPackage = await _repositoryWrapper.Package.SoftDelete(id);
        if (deletedPackage == null) return (null, ErrorResponseException.GenerateLocalizedResponse("Error in Deleting the Package", "خطأ في حذف الحزمة", language));
        
        return (deletedPackage, null);
    }

}
