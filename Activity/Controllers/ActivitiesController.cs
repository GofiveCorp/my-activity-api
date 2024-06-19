using Activity.Configuration;
using Activity.Models.Dtos;
using Activity.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Activity.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    [ApiKeyFilter]
    public class ActivitiesController : ControllerBase {
        private readonly IActivityService _activityService;
        private readonly IApiKeyService _apiKeyService;

        public ActivitiesController(IActivityService activityService, IApiKeyService apiKeyService) {
            _activityService = activityService;
            _apiKeyService = apiKeyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetActivities([FromQuery] string type = "") {
            var activities = await _activityService.GetActivities(type);
            return Ok(activities);
        }

        [HttpGet]
        [Route("api-key")]
        public async Task<IActionResult> GetApiKeyFromHeader([FromQuery] string type = "") {
            var apiKey = HttpContext.Request.Headers["X-API-KEY"];
            var activities = await _activityService.GetActivitiesByApiKey(apiKey, type);
            return Ok(activities);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetActivityById(Guid id) {
            var apiKey = HttpContext.Request.Headers["X-API-KEY"];
            var activity = await _activityService.GetActivityByApiKeyAndActivityId(apiKey, id);
            if (activity == null) {
                return NotFound();
            }

            return Ok(activity);
        }

        [HttpPost]
        public async Task<IActionResult> CreateActivity([FromBody] ActivityDto activity) {
            var apiKey = HttpContext.Request.Headers["X-API-KEY"];
            var apiKeyEntity = await _apiKeyService.GetApiKeyByValue(apiKey);
            if (apiKeyEntity == null) {
                return Unauthorized();
            }

            var activityEntity = new Models.Entities.Activity {
                Title = activity.Title,
                Description = activity.Description,
                Type = activity.Type,
                StartTime = activity.StartTime,
                EndTime = activity.EndTime,
                Date = activity.Date,
                Barometer = activity.Barometer,
                Image = activity.Image,
                ApiKey = apiKeyEntity
            };
            var result = await _activityService.CreateActivity(activityEntity);

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
