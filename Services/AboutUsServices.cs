using AutoMapper;
using Deli.DATA.DTOs;
using Deli.Entities;
using Deli.Interface;

namespace Deli.Services;

public interface IAboutUsServices
{
    Task<(DeliDifferenceDto? deliDifference, string? error)> Update(Guid id, DeliDifferenceUpdate deliDifferenceUpdate, string language);
    Task<DeliDifferenceDto> GetDeliDifference(string language);
    
    Task<(OurMissionDto? ourMission, string? error)> Update(Guid id, OurMissionUpdate ourMissionUpdate, string language);
    Task<OurMissionDto> GetOurMission(string language);
    
    Task<(MileStone? milestone, string? error)> Create(MileStoneForm milestoneForm , string language);
    Task<(List<MileStoneDto> milestones, int? totalCount, string? error)> GetAll(MileStoneFilter filter, string language);
    Task<(MileStone? milestone, string? error)> Update(Guid id ,MileStoneUpdate milestoneUpdate, string language);
    Task<(MileStone? milestone, string? error)> Delete(Guid id, string language);
    Task<(MileStone? milestone, string? error)> GetById(Guid id, string language);
    Task<(QualityToolsDto? qualityTools, string? error)> Update(Guid id, QualityToolsUpdate qualityToolsUpdate, string language);
    Task<QualityToolsDto> GetQualityTools(string language);

}
public class AboutUsServiceses : IAboutUsServices
{
    private readonly IMapper _mapper;
    private readonly IRepositoryWrapper _repositoryWrapper;

    public AboutUsServiceses(
        IMapper mapper ,
        IRepositoryWrapper repositoryWrapper
    )
    {
        _mapper = mapper;
        _repositoryWrapper = repositoryWrapper;
    }
   public async Task<(MileStone? milestone, string? error)> GetById(Guid id, string language)
    {
        var milestone = await _repositoryWrapper.MileStone.GetById(id);
        if (milestone == null)
        {
            return (null,ErrorResponseException.GenerateLocalizedResponse("MileStone not found", "لم يتم العثور على الحدث", language));
        }
        var milestoneDto = _mapper.Map<MileStoneDto>(milestone);
        milestoneDto.Description = ErrorResponseException.GenerateLocalizedResponse(milestone.Description,milestone.ArDescription,language);
        return (milestone, null);
    }
   
    public async Task<(MileStone? milestone, string? error)> Create(MileStoneForm milestoneForm, string language )
    {
        var milestone = _mapper.Map<MileStone>(milestoneForm);
        var result = await _repositoryWrapper.MileStone.Add(milestone);
        if (result == null)
        {
            return (null, ErrorResponseException.GenerateLocalizedResponse("Error in creating MileStone", "خطأ في إنشاء الحدث", language));
        }
        return (result, null);
        
    }

    public async Task<(List<MileStoneDto> milestones, int? totalCount, string? error)> GetAll(MileStoneFilter filter, string language)
    {
        var (milestones,Count) = await _repositoryWrapper.MileStone.GetAll<MileStoneDto>( x=>filter.Year==null || x.Year==filter.Year
            ,filter.PageNumber,filter.PageSize);
        if (milestones == null)
        {
            return (null, null,ErrorResponseException.GenerateLocalizedResponse("Error in getting MileStones", "خطأ في الحصول على الأحداث", language));
        }

        foreach (var milestone in milestones)
        {
            var originalMilestone = await _repositoryWrapper.MileStone.GetById(milestone.Id);
            milestone.Description = ErrorResponseException.GenerateLocalizedResponse(originalMilestone.Description,originalMilestone.ArDescription,language);
        }
        return (milestones, Count, null);

    }

    public async Task<(MileStone? milestone, string? error)> Update(Guid id ,MileStoneUpdate milestoneUpdate, string language)
    {
        var milestone = await _repositoryWrapper.MileStone.GetById(id);
        if (milestone == null)
        {
            return (null, ErrorResponseException.GenerateLocalizedResponse("MileStone not found", "لم يتم العثور على الحدث", language));
        }
        _mapper.Map(milestoneUpdate, milestone);
        var result = await _repositoryWrapper.MileStone.Update(milestone);
        if (result == null)
        {
            return (null, ErrorResponseException.GenerateLocalizedResponse("Error in updating MileStone", "خطأ في تحديث الحدث", language));
        }
        return (result, null);
      
    }

    public async Task<(MileStone? milestone, string? error)> Delete(Guid id, string language)
    {
        var milestone = await _repositoryWrapper.MileStone.Get(x => x.Id == id);
        if (milestone == null)
        {
            return (null, ErrorResponseException.GenerateLocalizedResponse("MileStone not found", "لم يتم العثور على الحدث", language));
        }

        var result = await _repositoryWrapper.MileStone.SoftDelete(id);
        if (result == null)
        {
            return (null, ErrorResponseException.GenerateLocalizedResponse("Error in deleting MileStone", "خطأ في حذف الحدث", language));
        }
        return (milestone, null);
   
    }
    public async Task<OurMissionDto> GetOurMission(string language)
    {
        // Try to retrieve the OurMission from the database
        var ourMission1 = await _repositoryWrapper.OurMission.GetAll();
        var ourMission = ourMission1.data.FirstOrDefault();

        // If it doesn't exist, create a default one
        if (ourMission == null)
        {
            var defaultOurMission = new OurMission
            {
                // Set default values here
            };

            ourMission = await _repositoryWrapper.OurMission.Add(defaultOurMission);
        }
            var ourMissionDto = _mapper.Map<OurMissionDto>(ourMission);
            ourMissionDto.Title=ErrorResponseException.GenerateLocalizedResponse(ourMission.Title,ourMission.ArTitle,language);
            ourMissionDto.Description=ErrorResponseException.GenerateLocalizedResponse(ourMission.Description,ourMission.ArDescription,language);
            ourMissionDto.MiniTitle1=ErrorResponseException.GenerateLocalizedResponse(ourMission.MiniTitle1,ourMission.ArMiniTitle1,language);
            ourMissionDto.MiniDescription1=ErrorResponseException.GenerateLocalizedResponse(ourMission.MiniDescription1,ourMission.ArMiniDescription1,language);
            ourMissionDto.MiniTitle2=ErrorResponseException.GenerateLocalizedResponse(ourMission.MiniTitle2,ourMission.ArMiniTitle2,language);
            ourMissionDto.MiniDescription2=ErrorResponseException.GenerateLocalizedResponse(ourMission.MiniDescription2,ourMission.ArMiniDescription2,language);
            ourMissionDto.MiniTitle3=ErrorResponseException.GenerateLocalizedResponse(ourMission.MiniTitle3,ourMission.ArMiniTitle3,language);
            ourMissionDto.MiniDescription3=ErrorResponseException.GenerateLocalizedResponse(ourMission.MiniDescription3,ourMission.ArMiniDescription3,language);
            
        // Map it to OurMissionDto and return
        return ourMissionDto;
    }

    public async Task<(OurMissionDto? ourMission, string? error)> Update(Guid id, OurMissionUpdate ourMissionUpdate, string language)
    {
        var ourMission = await _repositoryWrapper.OurMission.GetById(id);
        if (ourMission == null) return (null,ErrorResponseException.GenerateLocalizedResponse("OurMission not found", "لم يتم العثور على المعلومات", language));
        _mapper.Map(ourMissionUpdate, ourMission);
        ourMission = await _repositoryWrapper.OurMission.Update(ourMission, id);
        return (_mapper.Map<OurMissionDto>(ourMission), null);
    }
    public async Task<DeliDifferenceDto> GetDeliDifference(string language)
    {
        // Try to retrieve the DeliDifference from the database
        var deliDifference1 = await _repositoryWrapper.DeliDifference.GetAll();
        var deliDifference = deliDifference1.data.FirstOrDefault();
        

        // If it doesn't exist, create a default one
        if (deliDifference == null)
        {
            var defaultDeliDifference = new DeliDifference
            {
                // Set default values here
            };

            deliDifference = await _repositoryWrapper.DeliDifference.Add(defaultDeliDifference);
        }
        var deliDifferenceDto = _mapper.Map<DeliDifferenceDto>(deliDifference);
        deliDifferenceDto.Title=ErrorResponseException.GenerateLocalizedResponse(deliDifference.Title,deliDifference.ArTitle,language);
        deliDifferenceDto.Description=ErrorResponseException.GenerateLocalizedResponse(deliDifference.Description,deliDifference.ArDescription,"language");

        // Map it to DeliDifferenceDto and return
        return (deliDifferenceDto);
    }

    public async Task<(DeliDifferenceDto? deliDifference, string? error)> Update(Guid id, DeliDifferenceUpdate deliDifferenceUpdate, string language)
    {
        var deliDifference = await _repositoryWrapper.DeliDifference.GetById(id);
        if (deliDifference == null) return (null, ErrorResponseException.GenerateLocalizedResponse("DeliDifference not found", "لم يتم العثور على المعلومات", language));
        _mapper.Map(deliDifferenceUpdate, deliDifference);
        deliDifference = await _repositoryWrapper.DeliDifference.Update(deliDifference, id);
        return (_mapper.Map<DeliDifferenceDto>(deliDifference), null);
    }
    public async Task<QualityToolsDto> GetQualityTools(string language)
    {
        // Try to retrieve the QualityTools from the database
        var qualityTools1 = await _repositoryWrapper.QualityTools.GetAll();
        var qualityTools = qualityTools1.data.FirstOrDefault();

        // If it doesn't exist, create a default one
        if (qualityTools == null)
        {
            var defaultQualityTools = new QualityTools
            {
                // Set default values here
            };

            qualityTools = await _repositoryWrapper.QualityTools.Add(defaultQualityTools);
        }
        var qualityToolsDto = _mapper.Map<QualityToolsDto>(qualityTools);
        qualityToolsDto.Title=ErrorResponseException.GenerateLocalizedResponse(qualityTools.Title,qualityTools.ArTitle,language);
        qualityToolsDto.Description=ErrorResponseException.GenerateLocalizedResponse(qualityTools.Description,qualityTools.ArDescription,language);

        // Map it to QualityToolsDto and return
        return qualityToolsDto;
    }

    public async Task<(QualityToolsDto? qualityTools, string? error)> Update(Guid id, QualityToolsUpdate qualityToolsUpdate , string language)
    {
        var qualityTools = await _repositoryWrapper.QualityTools.GetById(id);
        if (qualityTools == null) return (null, ErrorResponseException.GenerateLocalizedResponse("QualityTools not found", "لم يتم العثور على المعلومات", language));
        _mapper.Map(qualityToolsUpdate, qualityTools);
        qualityTools = await _repositoryWrapper.QualityTools.Update(qualityTools, id);
        return (_mapper.Map<QualityToolsDto>(qualityTools), null);
    }

}


  