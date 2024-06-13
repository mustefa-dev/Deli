using Deli.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Deli.DATA.DTOs;
using Deli.Entities;

namespace Deli.Controllers
{
    public class AboutUsController : BaseController
    {
        private readonly IAboutUsServices _aboutUsServices;

        public AboutUsController(IAboutUsServices aboutUsServices)
        {
            _aboutUsServices = aboutUsServices;
        }

        [HttpGet("GetDeliDifference")]
        public async Task<ActionResult<DeliDifferenceDto>> GetDeliDifference() => Ok(await _aboutUsServices.GetDeliDifference());

        [HttpPut("UpdateDeliDifference/{id}")]
        public async Task<ActionResult> UpdateDeliDifference(Guid id, DeliDifferenceUpdate deliDifferenceUpdate) => Ok(await _aboutUsServices.Update(id, deliDifferenceUpdate,Language));

        [HttpGet("GetOurMission")]
        public async Task<ActionResult<OurMissionDto>> GetOurMission() => Ok(await _aboutUsServices.GetOurMission());

        [HttpPut("UpdateOurMission/{id}")]
        public async Task<ActionResult> UpdateOurMission(Guid id, OurMissionUpdate ourMissionUpdate) => Ok(await _aboutUsServices.Update(id, ourMissionUpdate,Language));

        [HttpGet("GetMileStone/{id}")]
        public async Task<ActionResult<MileStone>> GetMileStone(Guid id) => Ok(await _aboutUsServices.GetById(id,Language));

        [HttpPost("CreateMileStone")]
        public async Task<ActionResult<MileStone>> CreateMileStone(MileStoneForm milestoneForm) => Ok(await _aboutUsServices.Create(milestoneForm,Language));

        [HttpGet("GetAllMileStones")]
        public async Task<ActionResult<List<MileStoneDto>>> GetAllMileStones([FromQuery] MileStoneFilter filter) => Ok(await _aboutUsServices.GetAll(filter,Language));

        [HttpPut("UpdateMileStone/{id}")]
        public async Task<ActionResult<MileStone>> UpdateMileStone(Guid id, MileStoneUpdate milestoneUpdate) => Ok(await _aboutUsServices.Update(id, milestoneUpdate,Language));

        [HttpDelete("DeleteMileStone/{id}")]
        public async Task<ActionResult<MileStone>> DeleteMileStone(Guid id) => Ok(await _aboutUsServices.Delete(id,Language));
        [HttpGet("GetQualityTools")]
        public async Task<ActionResult<QualityToolsDto>> GetQualityTools() => Ok(await _aboutUsServices.GetQualityTools());

        [HttpPut("UpdateQualityTools/{id}")]
        public async Task<ActionResult> UpdateQualityTools(Guid id, QualityToolsUpdate qualityToolsUpdate) => Ok(await _aboutUsServices.Update(id, qualityToolsUpdate,Language));
    }
}