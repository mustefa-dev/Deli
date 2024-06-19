
using AutoMapper;
using Deli.Repository;
using Deli.Services;
using Deli.DATA.DTOs;
using Deli.Entities;
using Deli.Interface;

namespace Deli.Services;


public interface IAddressServices
{
Task<(Address? address, string? error)> Create(AddressForm addressForm, string language);
Task<(List<AddressDto> addresss, int? totalCount, string? error)> GetAll(AddressFilter filter, string language);
Task<(Address? address, string? error)> Update(Guid id , AddressUpdate addressUpdate, string language);
Task<(Address? address, string? error)> Delete(Guid id, string language);
}

public class AddressServices : IAddressServices
{
private readonly IMapper _mapper;
private readonly IRepositoryWrapper _repositoryWrapper;

public AddressServices(
    IMapper mapper ,
    IRepositoryWrapper repositoryWrapper
    )
{
    _mapper = mapper;
    _repositoryWrapper = repositoryWrapper;
}
   
   
public async Task<(Address? address, string? error)> Create(AddressForm addressForm, string language )
{
    var address = _mapper.Map<Address>(addressForm);
    var result = await _repositoryWrapper.Address.Add(address);
    if (result == null) return (null, ErrorResponseException.GenerateLocalizedResponse("Error in creating Address", "خطأ في إنشاء العنوان", language));
    return (result, null);
}

public async Task<(List<AddressDto> addresss, int? totalCount, string? error)> GetAll(AddressFilter filter, string language)
    {
        var (address,totalCount) = await _repositoryWrapper.Address.GetAll<AddressDto>(
            x => (string.IsNullOrEmpty(filter.Name) || x.Name.Contains(filter.Name)),
            filter.PageNumber,
            filter.PageSize
        );
        if (address == null) return (null, null, ErrorResponseException.GenerateLocalizedResponse("Error in getting Addresses", "خطأ في الحصول على العناوين", language));
        
        return (address, totalCount, null);
    }

public async Task<(Address? address, string? error)> Update(Guid id ,AddressUpdate addressUpdate, string language)
    {
        var address = await _repositoryWrapper.Address.Get(x => x.Id == id);
        if (address == null) return (null, ErrorResponseException.GenerateLocalizedResponse("Address not found", "لم يتم العثور على العنوان", language));
        _mapper.Map(addressUpdate, address);
        var result = await _repositoryWrapper.Address.Update(address);
        if (result == null) return (null, ErrorResponseException.GenerateLocalizedResponse("Error in updating Address", "خطأ في تحديث العنوان", language));
        return (result, null);
    }

public async Task<(Address? address, string? error)> Delete(Guid id, string language)
    {
        var address = await _repositoryWrapper.Address.Get(x => x.Id == id);
        if (address == null) return (null, ErrorResponseException.GenerateLocalizedResponse("Address not found", "لم يتم العثور على العنوان", language));
        var result = await _repositoryWrapper.Address.SoftDelete(id);
        if (result == null) return (null, ErrorResponseException.GenerateLocalizedResponse("Error in deleting Address", "خطأ في حذف العنوان", language));
        return (result, null);
    }

}
