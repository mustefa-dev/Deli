using Deli.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Deli.DATA.DTOs;
using Deli.Entities;

namespace Deli.Controllers
{
    public class ContentController : BaseController
    {
        private readonly IContentService _contentService;

        public ContentController(IContentService contentService)
        {
            _contentService = contentService;
        }

        [HttpGet("GetDeliDifference")]
        public async Task<ActionResult<DeliDifferenceDto>> GetDeliDifference() => Ok(await _contentService.GetDeliDifference());

        [HttpPut("UpdateDeliDifference/{id}")]
        public async Task<ActionResult> UpdateDeliDifference(Guid id, DeliDifferenceUpdate deliDifferenceUpdate) => Ok(await _contentService.Update(id, deliDifferenceUpdate));

        [HttpGet("GetOurMission")]
        public async Task<ActionResult<OurMissionDto>> GetOurMission() => Ok(await _contentService.GetOurMission());

        [HttpPut("UpdateOurMission/{id}")]
        public async Task<ActionResult> UpdateOurMission(Guid id, OurMissionUpdate ourMissionUpdate) => Ok(await _contentService.Update(id, ourMissionUpdate));

        [HttpGet("GetMileStone/{id}")]
        public async Task<ActionResult<MileStone>> GetMileStone(Guid id) => Ok(await _contentService.GetById(id));

        [HttpPost("CreateMileStone")]
        public async Task<ActionResult<MileStone>> CreateMileStone(MileStoneForm milestoneForm) => Ok(await _contentService.Create(milestoneForm));

        [HttpGet("GetAllMileStones")]
        public async Task<ActionResult<List<MileStoneDto>>> GetAllMileStones([FromQuery] MileStoneFilter filter) => Ok(await _contentService.GetAll(filter));

        [HttpPut("UpdateMileStone/{id}")]
        public async Task<ActionResult<MileStone>> UpdateMileStone(Guid id, MileStoneUpdate milestoneUpdate) => Ok(await _contentService.Update(id, milestoneUpdate));

        [HttpDelete("DeleteMileStone/{id}")]
        public async Task<ActionResult<MileStone>> DeleteMileStone(Guid id) => Ok(await _contentService.Delete(id));
    }
}