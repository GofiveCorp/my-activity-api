using Activity.Models.Dtos;
using Activity.Repositories.Interface;
using Activity.Services.Interface;

namespace Activity.Services.Implementation {
    public class ActivityService : IActivityService {
        private readonly IActivityRepository _activityRepository;

        public ActivityService(IActivityRepository activityRepository) {
            _activityRepository = activityRepository;
        }

        private DurationDto CalculateDuration(Models.Entities.Activity activity) {
            return new DurationDto {
                Hours = (activity.EndTime - activity.StartTime).Hours,
                Minutes = (activity.EndTime - activity.StartTime).Minutes
            };
        }

        public async Task<Models.Entities.Activity> CreateActivity(Models.Entities.Activity activity) {
            return await _activityRepository.CreateActivityAsync(activity);
        }

        public async Task<bool> DeleteActivity(Guid id) {
            return await _activityRepository.DeleteActivityAsync(id);
        }

        public async Task<List<ActivityDto>> GetActivities(string type) {
            var result = await _activityRepository.GetActivitiesAsync(type);

            var dto = new List<ActivityDto>();
            foreach (var item in result) {
                var activityDto = new ActivityDto {
                    Id = item.Id,
                    Title = item.Title,
                    Description = item.Description,
                    Type = item.Type,
                    StartTime = item.StartTime,
                    EndTime = item.EndTime,
                    Date = item.Date,
                    Duration = CalculateDuration(item),
                    Barometer = item.Barometer,
                    Image = item.Image
                };
                dto.Add(activityDto);
            }

            return dto;
        }

        public async Task<ActivityDto> GetActivityById(Guid id) {
            var result = await _activityRepository.GetActivityByIdAsync(id);
            if (result == null) {
                return null;
            }

            var activityDto = new ActivityDto {
                Id = result.Id,
                Title = result.Title,
                Description = result.Description,
                Type = result.Type,
                StartTime = result.StartTime,
                EndTime = result.EndTime,
                Date = result.Date,
                Duration = CalculateDuration(result),
                Barometer = result.Barometer,
                Image = result.Image
            };

            return activityDto;
        }

        public async Task<bool> UpdateActivity(Models.Entities.Activity activity, Guid id) {
            return await _activityRepository.UpdateActivityAsync(activity, id);
        }

        public async Task<bool> UploadActivityImage(Guid id, IFormFile imageFile) {
            return await _activityRepository.UploadActivityImageAsync(id, imageFile);
        }

        public async Task<List<ActivityDto>> GetActivitiesByApiKey(string apiKey, string type) {
            var result = await _activityRepository.GetActivitiesByApiKeyAsync(apiKey, type);
            if (result == null) {
                return null;
            }

            var dto = new List<ActivityDto>();
            foreach (var item in result) {
                var activityDto = new ActivityDto {
                    Id = item.Id,
                    Title = item.Title,
                    Description = item.Description,
                    Type = item.Type,
                    StartTime = item.StartTime,
                    EndTime = item.EndTime,
                    Date = item.Date,
                    Duration = CalculateDuration(item),
                    Barometer = item.Barometer,
                    Image = item.Image
                };
                dto.Add(activityDto);
            }

            return dto;
        }

        public async Task<ActivityDto> GetActivityByApiKeyAndActivityId(string apiKey, Guid activityId) {
            var result = await _activityRepository.GetActivityByApiKeyAndActivityIdAsync(apiKey, activityId);
            if (result == null) {
                return null;
            }

            var activityDto = new ActivityDto {
                Id = result.Id,
                Title = result.Title,
                Description = result.Description,
                Type = result.Type,
                StartTime = result.StartTime,
                EndTime = result.EndTime,
                Date = result.Date,
                Duration = CalculateDuration(result),
                Barometer = result.Barometer,
                Image = result.Image
            };

            return activityDto;
        }
    }
}
