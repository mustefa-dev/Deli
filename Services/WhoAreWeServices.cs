
using AutoMapper;
using Deli.Repository;
using Deli.Services;
using Deli.DATA.DTOs;
using Deli.Entities;
using Deli.Interface;

namespace Deli.Services;


public interface IWhoAreWeServices
{
Task<(WhoAreWe? whoarewe, string? error)> Create(WhoAreWeForm whoareweForm );
Task<(List<WhoAreWeDto> whoarewes, int? totalCount, string? error)> GetAll(WhoAreWeFilter filter);
Task<(WhoAreWe? whoarewe, string? error)> Update(Guid id , WhoAreWeUpdate whoareweUpdate);
Task<(WhoAreWe? whoarewe, string? error)> Delete(Guid id);
}

public class WhoAreWeServices : IWhoAreWeServices
{
private readonly IMapper _mapper;
private readonly IRepositoryWrapper _repositoryWrapper;

public WhoAreWeServices(
    IMapper mapper ,
    IRepositoryWrapper repositoryWrapper
    )
{
    _mapper = mapper;
    _repositoryWrapper = repositoryWrapper;
}
   
   
public async Task<(WhoAreWe? whoarewe, string? error)> Create(WhoAreWeForm whoareweForm )
{
    throw new NotImplementedException();
      
}

public async Task<(List<WhoAreWeDto> whoarewes, int? totalCount, string? error)> GetAll(WhoAreWeFilter filter)
    {
        throw new NotImplementedException();
    }

public async Task<(WhoAreWe? whoarewe, string? error)> Update(Guid id ,WhoAreWeUpdate whoareweUpdate)
    {
        throw new NotImplementedException();
      
    }

public async Task<(WhoAreWe? whoarewe, string? error)> Delete(Guid id)
    {
        throw new NotImplementedException();
   
    }

}
