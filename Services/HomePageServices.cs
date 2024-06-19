using AutoMapper;
using Deli.DATA.DTOs;
using Deli.Entities;
using Deli.Interface;

namespace Deli.Services;

public interface IHomepageServices
{
    Task<DiscoverDeliDto> GetDiscoverDeli(string language);
    Task<(DiscoverDeliDto? discoverDeli, string? error)> Update(Guid id, DiscoverDeliUpdate discoverDeliUpdate, string language);
    Task<WhoAreWeDto> GetMyWhoAreWe(string language);
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
    
    public async Task<DiscoverDeliDto> GetDiscoverDeli(string language)
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
        var discoverdelidto = _mapper.Map<DiscoverDeliDto>(discoverDeli);
        discoverdelidto.Title=ErrorResponseException.GenerateLocalizedResponse(discoverDeli.Title,discoverDeli.ArTitle,language);
        discoverdelidto.Description=ErrorResponseException.GenerateLocalizedResponse(discoverDeli.Description,discoverDeli.ArDescription,language);
        discoverdelidto.MiniTitle1=ErrorResponseException.GenerateLocalizedResponse(discoverDeli.MiniTitle1,discoverDeli.ArMiniTitle1,language);
        discoverdelidto.MiniDescription1=ErrorResponseException.GenerateLocalizedResponse(discoverDeli.MiniDescription1,discoverDeli.ArMiniDescription1,language);
        discoverdelidto.MiniTitle2=ErrorResponseException.GenerateLocalizedResponse(discoverDeli.MiniTitle2,discoverDeli.ArMiniTitle2,language);
        discoverdelidto.MiniDescription2=ErrorResponseException.GenerateLocalizedResponse(discoverDeli.MiniDescription2,discoverDeli.ArMiniDescription2,language);
        discoverdelidto.MiniTitle3=ErrorResponseException.GenerateLocalizedResponse(discoverDeli.MiniTitle3,discoverDeli.ArMiniTitle3,language);
        discoverdelidto.MiniDescription3=ErrorResponseException.GenerateLocalizedResponse(discoverDeli.MiniDescription3,discoverDeli.ArMiniDescription3,language);
        
        // Map it to DiscoverDeliDto and return
        return _mapper.Map<DiscoverDeliDto>(discoverDeli);
    }

    public async Task<(DiscoverDeliDto? discoverDeli, string? error)> Update(Guid id, DiscoverDeliUpdate discoverDeliUpdate, string language)
    {
        var discoverDeli = await _repositoryWrapper.DiscoverDeli.GetById(id);
        if (discoverDeli == null) return (null, ErrorResponseException.GenerateLocalizedResponse("DiscoverDeli not found", "لم يتم العثور على المعلومات", language));
        _mapper.Map(discoverDeliUpdate, discoverDeli);
        discoverDeli = await _repositoryWrapper.DiscoverDeli.Update(discoverDeli, id);
        return (_mapper.Map<DiscoverDeliDto>(discoverDeli), null);
    }
    
    public async Task<WhoAreWeDto> GetMyWhoAreWe(string language)
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

        var whoarewedto = _mapper.Map<WhoAreWeDto>(whoAreWe);
        whoarewedto.Title=ErrorResponseException.GenerateLocalizedResponse(whoAreWe.Title,whoAreWe.ArTitle,language);
        whoarewedto.Description=ErrorResponseException.GenerateLocalizedResponse(whoAreWe.Description,whoAreWe.ArDescription,language);
        return whoarewedto;
    }

    public async Task<(WhoAreWeDto? whoAreWe, string? error)> Update(Guid id, WhoAreWeUpdate whoAreWeUpdate, string language)
    {
        var whoAreWe = await _repositoryWrapper.WhoAreWe.GetById(id);
        if (whoAreWe == null) return (null, ErrorResponseException.GenerateLocalizedResponse("WhoAreWe not found", "لم يتم العثور على المعلومات", language));
        _mapper.Map(whoAreWeUpdate, whoAreWe);
        whoAreWe = await _repositoryWrapper.WhoAreWe.Update(whoAreWe, id);
        return (_mapper.Map<WhoAreWeDto>(whoAreWe), null);
    }
}

