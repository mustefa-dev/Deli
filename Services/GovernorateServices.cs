using AutoMapper;
using Deli.DATA.DTOs;
using Deli.Entities;
using Deli.Interface;

namespace Deli.Services;

public interface IGovernorateServices
{
    Task<(Governorate? governorate, string? error)> Create(GovernorateForm governorateForm, string language);
    Task<(List<GovernorateDto> governorates, int? totalCount, string? error)> GetAll(GovernorateFilter filter, string language);
    Task <(GovernorateDto? governorate, string? error)> GetById(Guid id, string language);
    Task<(GovernorateDto? governorate, string? error)> Update(Guid id, GovernorateUpdate governorateUpdate, string language);
    Task<(GovernorateDto? governorate, string? error)> Delete(Guid id, string language);
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

    

    public async Task<(Governorate? governorate, string? error)> Create(GovernorateForm governorateForm, string language)
    {
        var governorate =_mapper.Map<Governorate>(governorateForm);
        var response = await _repositoryWrapper.Governorate.Add(governorate);
        return response == null ? (null,ErrorResponseException.GenerateErrorResponse("Error in Creating a Governorate","خطأ في انشاء المنطقة",language)): (response,null);
    }

    public async Task<(List<GovernorateDto> governorates, int? totalCount, string? error)> GetAll(
        GovernorateFilter filter, string language)
    {
        var (governorate, totalCount) = await _repositoryWrapper.Governorate.GetAll<GovernorateDto>(
            x =>
                (filter.Name == null || x.Name.Contains(filter.Name))&&
                (filter.ArName == null || x.ArName.Contains(filter.ArName)),
            filter.PageNumber, filter.PageSize
        );
        var responseDto = _mapper.Map<List<GovernorateDto>>(governorate);
        return (responseDto, totalCount, null);
    }

    public async Task<(GovernorateDto? governorate, string? error)> GetById(Guid id, string language)
    {
        var governorate = await _repositoryWrapper.Governorate.Get(x => x.Id == id);
        if (governorate == null) return (null, ErrorResponseException.GenerateErrorResponse("Governorate not found", "لم يتم العثور على المنطقة", language));
        var responseDto = _mapper.Map<GovernorateDto>(governorate);
        return (responseDto, null);
       
    }

    public async Task<(GovernorateDto? governorate, string? error)> Update(Guid id, GovernorateUpdate governorateUpdate, string language)
    {
        var governorate = await _repositoryWrapper.Governorate.Get(x => x.Id == id);
        if (governorate == null) return (null, ErrorResponseException.GenerateErrorResponse("Governorate not found", "لم يتم العثور على المنطقة", language));
        governorate = _mapper.Map(governorateUpdate, governorate);
        var response = await _repositoryWrapper.Governorate.Update(governorate);
        var responseDto = _mapper.Map<GovernorateDto>(response);
        return response == null ? (null, ErrorResponseException.GenerateErrorResponse("Error in updating governorate","خطأ في تحديث المنطقة",language)) : (responseDto, null);
    }

    public async Task<(GovernorateDto? governorate, string? error)> Delete(Guid id, string language)
    {
        var governorate = _repositoryWrapper.Governorate.Get(x => x.Id == id);
        if (governorate == null) return (null, ErrorResponseException.GenerateErrorResponse("Governorate not found", "لم يتم العثور على المنطقة", language));
        var response = await _repositoryWrapper.Governorate.SoftDelete(id);
        var responseDto = _mapper.Map<GovernorateDto>(governorate);
        return response == null ? (null, ErrorResponseException.GenerateErrorResponse("Error in deleting governorate","خطأ في حذف المنطقة",language)) : (responseDto, null);

    }
}