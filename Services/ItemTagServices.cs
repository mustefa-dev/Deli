
using AutoMapper;
using Deli.Repository;
using Deli.Services;
using Deli.DATA.DTOs;
using Deli.Entities;
using Deli.Interface;

namespace Deli.Services;


public interface IItemTagServices
{
Task<(ItemTag? itemtag, string? error)> Create(ItemTagForm itemtagForm );
Task<(List<ItemTagDto> itemtags, int? totalCount, string? error)> GetAll(ItemTagFilter filter);
Task<(ItemTag? itemtag, string? error)> Update(Guid id , ItemTagUpdate itemtagUpdate);
Task<(ItemTag? itemtag, string? error)> Delete(Guid id);
}

public class ItemTagServices : IItemTagServices
{
private readonly IMapper _mapper;
private readonly IRepositoryWrapper _repositoryWrapper;

public ItemTagServices(
    IMapper mapper ,
    IRepositoryWrapper repositoryWrapper
    )
{
    _mapper = mapper;
    _repositoryWrapper = repositoryWrapper;
}
   
   
public async Task<(ItemTag? itemtag, string? error)> Create(ItemTagForm itemtagForm )
{
    throw new NotImplementedException();
      
}

public async Task<(List<ItemTagDto> itemtags, int? totalCount, string? error)> GetAll(ItemTagFilter filter)
    {
        throw new NotImplementedException();
    }

public async Task<(ItemTag? itemtag, string? error)> Update(Guid id ,ItemTagUpdate itemtagUpdate)
    {
        throw new NotImplementedException();
      
    }

public async Task<(ItemTag? itemtag, string? error)> Delete(Guid id)
    {
        throw new NotImplementedException();
   
    }

}
