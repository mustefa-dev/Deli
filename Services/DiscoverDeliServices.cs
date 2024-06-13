
using AutoMapper;
using Deli.Repository;
using Deli.Services;
using Deli.DATA.DTOs;
using Deli.Entities;
using Deli.Interface;

namespace Deli.Services;


public interface IDiscoverDeliServices
{
Task<(DiscoverDeli? discoverdeli, string? error)> Create(DiscoverDeliForm discoverdeliForm );
Task<(List<DiscoverDeliDto> discoverdelis, int? totalCount, string? error)> GetAll(DiscoverDeliFilter filter);
Task<(DiscoverDeli? discoverdeli, string? error)> Update(Guid id , DiscoverDeliUpdate discoverdeliUpdate);
Task<(DiscoverDeli? discoverdeli, string? error)> Delete(Guid id);
}

public class DiscoverDeliServices : IDiscoverDeliServices
{
private readonly IMapper _mapper;
private readonly IRepositoryWrapper _repositoryWrapper;

public DiscoverDeliServices(
    IMapper mapper ,
    IRepositoryWrapper repositoryWrapper
    )
{
    _mapper = mapper;
    _repositoryWrapper = repositoryWrapper;
}
   
   
public async Task<(DiscoverDeli? discoverdeli, string? error)> Create(DiscoverDeliForm discoverdeliForm )
{
    throw new NotImplementedException();
      
}

public async Task<(List<DiscoverDeliDto> discoverdelis, int? totalCount, string? error)> GetAll(DiscoverDeliFilter filter)
    {
        throw new NotImplementedException();
    }

public async Task<(DiscoverDeli? discoverdeli, string? error)> Update(Guid id ,DiscoverDeliUpdate discoverdeliUpdate)
    {
        throw new NotImplementedException();
      
    }

public async Task<(DiscoverDeli? discoverdeli, string? error)> Delete(Guid id)
    {
        throw new NotImplementedException();
   
    }

}
