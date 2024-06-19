using Activity.Configuration;
using Activity.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Activity.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    [ApiKeyFilter]
    public class ActivitiesController : ControllerBase {
        private readonly IActivityService _activityService;

        public ActivitiesController(IActivityService activityService) {
            _activityService = activityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetActivities([FromQuery] string type = "") {
            var activities = await _activityService.GetActivities(type);
            return Ok(activities);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetActivityById(Guid id) {
            var activity = await _activityService.GetActivityById(id);
            if (activity == null) {
                return NotFound();
            }

            return Ok(activity);
        }

        [HttpPost]
        public async Task<IActionResult> CreateActivity([FromBody] Models.Entities.Activity activity) {
            var result = await _activityService.CreateActivity(activity);

            if (result == null) {
                return BadRequest();
            }

            return Created();
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateActivity([FromBody] Models.Entities.Activity activity, Guid id) {
            var result = await _activityService.UpdateActivity(activity, id);

            if (result) {
                return NoContent();
            } else {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteActivity(Guid id) {
            var result = await _activityService.DeleteActivity(id);
            if (result) {
                return NoContent();
            } else {
                return BadRequest();
            }
        }

        [HttpPost("{id}/upload-image")]
        public async Task<IActionResult> UploadActivityImage(Guid id, IFormFile imageFile) {
            var result = await _activityService.UploadActivityImage(id, imageFile);
            if (result) {
                return Ok();
            }
            return NotFound();
        }

    }
}
