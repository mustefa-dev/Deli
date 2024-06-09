using AutoMapper;
using Deli.DATA.DTOs;
using Deli.Entities;
using Deli.Interface;

namespace Deli.Services;

public interface IAboutUsService
{
    Task<(DeliDifferenceDto? deliDifference, string? error)> Update(Guid id, DeliDifferenceUpdate deliDifferenceUpdate);
    Task<DeliDifferenceDto> GetDeliDifference();
    
    Task<(OurMissionDto? ourMission, string? error)> Update(Guid id, OurMissionUpdate ourMissionUpdate); Task<OurMissionDto> GetOurMission();
    
    Task<(MileStone? milestone, string? error)> Create(MileStoneForm milestoneForm );
    Task<(List<MileStoneDto> milestones, int? totalCount, string? error)> GetAll(MileStoneFilter filter);
    Task<(MileStone? milestone, string? error)> Update(Guid id ,MileStoneUpdate milestoneUpdate);
    Task<(MileStone? milestone, string? error)> Delete(Guid id);
    Task<(MileStone? milestone, string? error)> GetById(Guid id);
    Task<(QualityToolsDto? qualityTools, string? error)> Update(Guid id, QualityToolsUpdate qualityToolsUpdate);
    Task<QualityToolsDto> GetQualityTools();

}
public class AboutUsService : IAboutUsService
{
    private readonly IMapper _mapper;
    private readonly IRepositoryWrapper _repositoryWrapper;

    public AboutUsService(
        IMapper mapper ,
        IRepositoryWrapper repositoryWrapper
    )
    {
        _mapper = mapper;
        _repositoryWrapper = repositoryWrapper;
    }
   public async Task<(MileStone? milestone, string? error)> GetById(Guid id)
    {
        var milestone = await _repositoryWrapper.MileStone.GetById(id);
        if (milestone == null)
        {
            return (null, "MileStone not found");
        }
        return (milestone, null);
    }
   
    public async Task<(MileStone? milestone, string? error)> Create(MileStoneForm milestoneForm )
    {
        var milestone = _mapper.Map<MileStone>(milestoneForm);
        var result = await _repositoryWrapper.MileStone.Add(milestone);
        if (result == null)
        {
            return (null, "Error creating MileStone");
        }
        return (result, null);
        
    }

    public async Task<(List<MileStoneDto> milestones, int? totalCount, string? error)> GetAll(MileStoneFilter filter)
    {
        var (milestones,Count) = await _repositoryWrapper.MileStone.GetAll<MileStoneDto>( x=>filter.Year==null || x.Year==filter.Year
            ,filter.PageNumber,filter.PageSize);
        if (milestones == null)
        {
            return (null, null, "Error fetching MileStones");
        }
        return (milestones, Count, null);

    }

    public async Task<(MileStone? milestone, string? error)> Update(Guid id ,MileStoneUpdate milestoneUpdate)
    {
        var milestone = await _repositoryWrapper.MileStone.GetById(id);
        if (milestone == null)
        {
            return (null, "MileStone not found");
        }
        _mapper.Map(milestoneUpdate, milestone);
        var result = await _repositoryWrapper.MileStone.Update(milestone);
        if (result == null)
        {
            return (null, "Error updating MileStone");
        }
        return (result, null);
      
    }

    public async Task<(MileStone? milestone, string? error)> Delete(Guid id)
    {
        var milestone = await _repositoryWrapper.MileStone.Get(x => x.Id == id);
        if (milestone == null)
        {
            return (null, "MileStone not found");
        }

        var result = await _repositoryWrapper.MileStone.SoftDelete(id);
        if (result == null)
        {
            return (null, "Error deleting MileStone");
        }
        return (milestone, null);
   
    }
    public async Task<OurMissionDto> GetOurMission()
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

        // Map it to OurMissionDto and return
        return _mapper.Map<OurMissionDto>(ourMission);
    }

    public async Task<(OurMissionDto? ourMission, string? error)> Update(Guid id, OurMissionUpdate ourMissionUpdate)
    {
        var ourMission = await _repositoryWrapper.OurMission.GetById(id);
        if (ourMission == null) return (null, "OurMission not found");
        _mapper.Map(ourMissionUpdate, ourMission);
        ourMission = await _repositoryWrapper.OurMission.Update(ourMission, id);
        return (_mapper.Map<OurMissionDto>(ourMission), null);
    }
    public async Task<DeliDifferenceDto> GetDeliDifference()
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

        // Map it to DeliDifferenceDto and return
        return _mapper.Map<DeliDifferenceDto>(deliDifference);
    }

    public async Task<(DeliDifferenceDto? deliDifference, string? error)> Update(Guid id, DeliDifferenceUpdate deliDifferenceUpdate)
    {
        var deliDifference = await _repositoryWrapper.DeliDifference.GetById(id);
        if (deliDifference == null) return (null, "DeliDifference not found");
        _mapper.Map(deliDifferenceUpdate, deliDifference);
        deliDifference = await _repositoryWrapper.DeliDifference.Update(deliDifference, id);
        return (_mapper.Map<DeliDifferenceDto>(deliDifference), null);
    }
    public async Task<QualityToolsDto> GetQualityTools()
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

        // Map it to QualityToolsDto and return
        return _mapper.Map<QualityToolsDto>(qualityTools);
    }

    public async Task<(QualityToolsDto? qualityTools, string? error)> Update(Guid id, QualityToolsUpdate qualityToolsUpdate)
    {
        var qualityTools = await _repositoryWrapper.QualityTools.GetById(id);
        if (qualityTools == null) return (null, "QualityTools not found");
        _mapper.Map(qualityToolsUpdate, qualityTools);
        qualityTools = await _repositoryWrapper.QualityTools.Update(qualityTools, id);
        return (_mapper.Map<QualityToolsDto>(qualityTools), null);
    }

}


  