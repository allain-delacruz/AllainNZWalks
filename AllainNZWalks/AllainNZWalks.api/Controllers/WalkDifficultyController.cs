using AllainNZWalks.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using AllainNZWalks.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace AllainNZWalks.Controllers
{
    [ApiController]
    [Route("NZWalkDifficulty")]

    public class WalkDifficultyController : Controller
    {
        private readonly IWalkDifficultyRepository walkDifficultyRepository;
        private readonly IMapper mapper;

        public WalkDifficultyController(IWalkDifficultyRepository walkDifficultyRepository, IMapper mapper)
        {
            this.walkDifficultyRepository = walkDifficultyRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "reader,writer")] //remove to make public
        public async Task<IActionResult> GetAllWalkDifficultyAsync()
        {
            // Fetch data from Database - Domain walks
            var walkD = await walkDifficultyRepository.GetAllWalkDifficultyAsync();

            // Convert Domain walks to DTO Walks
            var walkDDTO = mapper.Map<List<Models.DTO.WalkDifficulty>>(walkD);

            // Return Ok Response
            return Ok(walkDDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkDifficultyAsync")]
        [Authorize(Roles = "reader,writer")] //remove to make public
        public async Task<IActionResult> GetWalkDifficultyAsync(Guid id)
        {
            var walkD = await walkDifficultyRepository.GetWalkDifficultyAsync(id);

            if (walkD == null)
            {
                return NotFound();
            }

            var walkDDTO = mapper.Map<Models.DTO.WalkDifficulty>(walkD);

            return Ok(walkDDTO);
        }

        [HttpPost]
        [Authorize(Roles = "writer")] //remove to make public
        public async Task<IActionResult> AddWalkDifficultyAsync([FromBody] Models.DTO.AddUpdateWalkDifficultyRequest addUpdateDeleteWalkDifficultyRequest)
        {
            //Validate
            //if (!ValidateAddWalkDifficultyAsync(addUpdateDeleteWalkDifficultyRequest))
            //{
            //    return BadRequest(ModelState);
            //}

            var walkD = new Models.Domain.WalkDifficulty()
            {
                Code = addUpdateDeleteWalkDifficultyRequest.Code
            };

            walkD = await walkDifficultyRepository.AddWalkDifficultyAsync(walkD);

            var walkDDTO = new Models.DTO.WalkDifficulty()
            {
                Id = walkD.Id,
                Code = walkD.Code
            };

            return CreatedAtAction(nameof(GetWalkDifficultyAsync), new { id = walkDDTO.Id }, walkDDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        [Authorize(Roles = "writer")] //remove to make public
        public async Task<IActionResult> UpdateWalkDifficultyAsync([FromRoute] Guid id, [FromBody] Models.DTO.AddUpdateWalkDifficultyRequest addUpdateDeleteWalkDifficultyRequest)
        {
            //Validate
            //if (!ValidateUpdateWalkDifficultyAsync(addUpdateDeleteWalkDifficultyRequest))
            //{
            //    return BadRequest(ModelState);
            //}

            var walkD = new Models.Domain.WalkDifficulty()
            {
                Code = addUpdateDeleteWalkDifficultyRequest.Code,
            };

            walkD = await walkDifficultyRepository.UpdateWalkDifficultyAsync(id, walkD);

            if (walkD == null)
            {
                return NotFound();
            }

            var walkDDTO = mapper.Map<Models.DTO.WalkDifficulty>(walkD);

            return Ok(walkDDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        [Authorize(Roles = "writer")] //remove to make public
        public async Task<IActionResult> DeleteWalkDifficultyAsync(Guid id)
        {
            var walkD = await walkDifficultyRepository.DeleteWalkDifficultyAsync(id);

            if (walkD == null)
            {
                return NotFound();
            }

            var walkDDTO = new Models.DTO.WalkDifficulty()
            {
                Id = walkD.Id,
                Code = walkD.Code
            };

            return Ok(walkDDTO);
        }

        #region Private Methods

        private bool ValidateAddWalkDifficultyAsync(AddUpdateWalkDifficultyRequest addUpdateWalkDifficultyRequest)
        {
            if (addUpdateWalkDifficultyRequest == null)
            {
                ModelState.AddModelError(nameof(addUpdateWalkDifficultyRequest), $"Data is required.");

                return false;
            }

            if (string.IsNullOrWhiteSpace(addUpdateWalkDifficultyRequest.Code))
            {
                ModelState.AddModelError(nameof(addUpdateWalkDifficultyRequest.Code), $"{nameof(addUpdateWalkDifficultyRequest.Code)} cannot be null or empty or white space.");
            }

            if (ModelState.ErrorCount > 0) 
            {
                return false;
            }

            return true;
        }

        private bool ValidateUpdateWalkDifficultyAsync(AddUpdateWalkDifficultyRequest addUpdateWalkDifficultyRequest)
        {
            if (addUpdateWalkDifficultyRequest == null)
            {
                ModelState.AddModelError(nameof(addUpdateWalkDifficultyRequest), $"Data is required.");

                return false;
            }

            if (string.IsNullOrWhiteSpace(addUpdateWalkDifficultyRequest.Code))
            {
                ModelState.AddModelError(nameof(addUpdateWalkDifficultyRequest.Code), $"{nameof(addUpdateWalkDifficultyRequest.Code)} cannot be null or empty or white space.");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }

        #endregion
    }
}
