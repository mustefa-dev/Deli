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
        private readonly IAboutUsService _aboutUsService;

        public AboutUsController(IAboutUsService aboutUsService)
        {
            _aboutUsService = aboutUsService;
        }

        [HttpGet("GetDeliDifference")]
        public async Task<ActionResult<DeliDifferenceDto>> GetDeliDifference() => Ok(await _aboutUsService.GetDeliDifference());

        [HttpPut("UpdateDeliDifference/{id}")]
        public async Task<ActionResult> UpdateDeliDifference(Guid id, DeliDifferenceUpdate deliDifferenceUpdate) => Ok(await _aboutUsService.Update(id, deliDifferenceUpdate));

        [HttpGet("GetOurMission")]
        public async Task<ActionResult<OurMissionDto>> GetOurMission() => Ok(await _aboutUsService.GetOurMission());

        [HttpPut("UpdateOurMission/{id}")]
        public async Task<ActionResult> UpdateOurMission(Guid id, OurMissionUpdate ourMissionUpdate) => Ok(await _aboutUsService.Update(id, ourMissionUpdate));

        [HttpGet("GetMileStone/{id}")]
        public async Task<ActionResult<MileStone>> GetMileStone(Guid id) => Ok(await _aboutUsService.GetById(id));

        [HttpPost("CreateMileStone")]
        public async Task<ActionResult<MileStone>> CreateMileStone(MileStoneForm milestoneForm) => Ok(await _aboutUsService.Create(milestoneForm));

        [HttpGet("GetAllMileStones")]
        public async Task<ActionResult<List<MileStoneDto>>> GetAllMileStones([FromQuery] MileStoneFilter filter) => Ok(await _aboutUsService.GetAll(filter));

        [HttpPut("UpdateMileStone/{id}")]
        public async Task<ActionResult<MileStone>> UpdateMileStone(Guid id, MileStoneUpdate milestoneUpdate) => Ok(await _aboutUsService.Update(id, milestoneUpdate));

        [HttpDelete("DeleteMileStone/{id}")]
        public async Task<ActionResult<MileStone>> DeleteMileStone(Guid id) => Ok(await _aboutUsService.Delete(id));
        [HttpGet("GetQualityTools")]
        public async Task<ActionResult<QualityToolsDto>> GetQualityTools() => Ok(await _aboutUsService.GetQualityTools());

        [HttpPut("UpdateQualityTools/{id}")]
        public async Task<ActionResult> UpdateQualityTools(Guid id, QualityToolsUpdate qualityToolsUpdate) => Ok(await _aboutUsService.Update(id, qualityToolsUpdate));
    }
}