
using AutoMapper;
using Deli.Repository;
using Deli.Services;
using Deli.DATA.DTOs;
using Deli.Entities;
using Deli.Interface;

namespace Deli.Services;


public interface IAddressServices
{
Task<(Address? address, string? error)> Create(AddressForm addressForm );
Task<(List<AddressDto> addresss, int? totalCount, string? error)> GetAll(AddressFilter filter);
Task<(Address? address, string? error)> Update(Guid id , AddressUpdate addressUpdate);
Task<(Address? address, string? error)> Delete(Guid id);
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
   
   
public async Task<(Address? address, string? error)> Create(AddressForm addressForm )
{
    var address = _mapper.Map<Address>(addressForm);
    var result = await _repositoryWrapper.Address.Add(address);
    if (result == null) return (null, "Error in creating address");
    return (result, null);
}

public async Task<(List<AddressDto> addresss, int? totalCount, string? error)> GetAll(AddressFilter filter)
    {
        var (address,totalCount) = await _repositoryWrapper.Address.GetAll<AddressDto>(
            x => (string.IsNullOrEmpty(filter.Name) || x.Name.Contains(filter.Name)),
            filter.PageNumber,
            filter.PageSize
        );
        return (address, totalCount, null);
    }

public async Task<(Address? address, string? error)> Update(Guid id ,AddressUpdate addressUpdate)
    {
        var address = await _repositoryWrapper.Address.Get(x => x.Id == id);
        if (address == null) return (null, "Address not found");
        _mapper.Map(addressUpdate, address);
        var result = await _repositoryWrapper.Address.Update(address);
        if (result == null) return (null, "Error in updating address");
        return (result, null);
    }

public async Task<(Address? address, string? error)> Delete(Guid id)
    {
        var address = await _repositoryWrapper.Address.Get(x => x.Id == id);
        if (address == null) return (null, "Address not found");
        var result = await _repositoryWrapper.Address.SoftDelete(id);
        if (result == null) return (null, "Error in deleting address");
        return (result, null);
    }

}
