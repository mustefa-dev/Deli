using AutoMapper;
using Deli.DATA.DTOs;
using Deli.Entities;
using Deli.Interface;

namespace Deli.Services;

public interface IGovernorateServices
{
    Task<(Governorate? governorate, string? error)> Create(GovernorateForm governorateForm);
    Task<(List<GovernorateDto> governorates, int? totalCount, string? error)> GetAll(GovernorateFilter filter);
    Task <(GovernorateDto? governorate, string? error)> GetById(Guid id);
    Task<(GovernorateDto? governorate, string? error)> Update(Guid id, GovernorateUpdate governorateUpdate);
    Task<(GovernorateDto? governorate, string? error)> Delete(Guid id);
}

public class GovernorateServices : IGovernorateServices
{
    private readonly IMapper _mapper;
    private readonly IRepositoryWrapper _repositoryWrapper;

    public GovernorateServices(
        IMapper mapper,
        IRepositoryWrapper repositoryWrapper
    )
    {
        _mapper = mapper;
        _repositoryWrapper = repositoryWrapper;
    }


    public async Task<(Governorate? governorate, string? error)> Create(GovernorateForm governorateForm)
    {
        var governorate =_mapper.Map<Governorate>(governorateForm);
        var response = await _repositoryWrapper.Governorate.Add(governorate);
        return response == null ? (null, "Governorate"): (response,null);
    }

    public async Task<(List<GovernorateDto> governorates, int? totalCount, string? error)> GetAll(
        GovernorateFilter filter)
    {
        var (governorate, totalCount) = await _repositoryWrapper.Governorate.GetAll<GovernorateDto>(
            x =>
                (filter.Name == null || x.Name.Contains(filter.Name)),
            filter.PageNumber, filter.PageSize
        );
        var responseDto = _mapper.Map<List<GovernorateDto>>(governorate);
        return (responseDto, totalCount, null);
    }

    public Task<(GovernorateDto? governorate, string? error)> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<(GovernorateDto? governorate, string? error)> Update(Guid id, GovernorateUpdate governorateUpdate)
    {
        var governorate = await _repositoryWrapper.Governorate.Get(x => x.Id == id);
        if (governorate == null) return (null, "governorate not found");
        governorate = _mapper.Map(governorateUpdate, governorate);
        var response = await _repositoryWrapper.Governorate.Update(governorate);
        var responseDto = _mapper.Map<GovernorateDto>(response);
        return response == null ? (null, "Ticket couldn't be updated") : (responseDto, null);
    }

    public async Task<(GovernorateDto? governorate, string? error)> Delete(Guid id)
    {
        var governorate = _repositoryWrapper.Governorate.Get(x => x.Id == id);
        if (governorate == null) return (null, "governorate not found");
        var response = await _repositoryWrapper.Governorate.SoftDelete(id);
        var responseDto = _mapper.Map<GovernorateDto>(governorate);
        return response == null ? (null, "governorate couldn't be deleted") : (responseDto, null);

    }
}