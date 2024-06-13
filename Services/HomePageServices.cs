using AutoMapper;
using Deli.DATA.DTOs;
using Deli.Entities;
using Deli.Interface;

namespace Deli.Services;

public interface IHomepageServices
{
    Task<DiscoverDeliDto> GetDiscoverDeli();
    Task<(DiscoverDeliDto? discoverDeli, string? error)> Update(Guid id, DiscoverDeliUpdate discoverDeliUpdate, string language);
    Task<WhoAreWeDto> GetMyWhoAreWe();
    Task<(WhoAreWeDto? whoAreWe, string? error)> Update(Guid id, WhoAreWeUpdate whoAreWeUpdate, string language);
}
public class HomePageServices : IHomepageServices
{
    private readonly IRepositoryWrapper _repositoryWrapper;
    private readonly IMapper _mapper;

    public HomePageServices(IRepositoryWrapper repositoryWrapper, IMapper mapper)
    {
        _repositoryWrapper = repositoryWrapper;
        _mapper = mapper;
    }
    
    public async Task<DiscoverDeliDto> GetDiscoverDeli()
    {
        // Try to retrieve the DiscoverDeli from the database
        var discoverDeli1 = await _repositoryWrapper.DiscoverDeli.GetAll();
        var discoverDeli = discoverDeli1.data.FirstOrDefault();

        // If it doesn't exist, create a default one
        if (discoverDeli == null)
        {
            var defaultDiscoverDeli = new DiscoverDeli
            {
                // Set default values here
            };

            discoverDeli = await _repositoryWrapper.DiscoverDeli.Add(defaultDiscoverDeli);
        }

        // Map it to DiscoverDeliDto and return
        return _mapper.Map<DiscoverDeliDto>(discoverDeli);
    }

    public async Task<(DiscoverDeliDto? discoverDeli, string? error)> Update(Guid id, DiscoverDeliUpdate discoverDeliUpdate, string language)
    {
        var discoverDeli = await _repositoryWrapper.DiscoverDeli.GetById(id);
        if (discoverDeli == null) return (null, ErrorResponseException.GenerateErrorResponse("DiscoverDeli not found", "لم يتم العثور على المعلومات", language));
        _mapper.Map(discoverDeliUpdate, discoverDeli);
        discoverDeli = await _repositoryWrapper.DiscoverDeli.Update(discoverDeli, id);
        return (_mapper.Map<DiscoverDeliDto>(discoverDeli), null);
    }
    
    public async Task<WhoAreWeDto> GetMyWhoAreWe()
    {
        // Try to retrieve the WhoAreWe from the database
        var whoAreWe1 = await _repositoryWrapper.WhoAreWe.GetAll();
        var whoAreWe = whoAreWe1.data.FirstOrDefault();

        // If it doesn't exist, create a default one
        if (whoAreWe == null)
        {
            var defaultWhoAreWe = new WhoAreWe
            {
                // Set default values here
            };

            whoAreWe = await _repositoryWrapper.WhoAreWe.Add(defaultWhoAreWe);
        }

        // Map it to WhoAreWeDto and return
        return _mapper.Map<WhoAreWeDto>(whoAreWe);
    }

    public async Task<(WhoAreWeDto? whoAreWe, string? error)> Update(Guid id, WhoAreWeUpdate whoAreWeUpdate, string language)
    {
        var whoAreWe = await _repositoryWrapper.WhoAreWe.GetById(id);
        if (whoAreWe == null) return (null, ErrorResponseException.GenerateErrorResponse("WhoAreWe not found", "لم يتم العثور على المعلومات", language));
        _mapper.Map(whoAreWeUpdate, whoAreWe);
        whoAreWe = await _repositoryWrapper.WhoAreWe.Update(whoAreWe, id);
        return (_mapper.Map<WhoAreWeDto>(whoAreWe), null);
    }
}

