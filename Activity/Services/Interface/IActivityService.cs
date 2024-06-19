using Activity.Models.Dtos;

namespace Activity.Services.Interface {
    public interface IActivityService {
        Task<ActivityDto> GetActivityById(Guid id);
        Task<List<ActivityDto>> GetActivities(string type);
        Task<Models.Entities.Activity> CreateActivity(Models.Entities.Activity activity);
        Task<bool> UpdateActivity(Models.Entities.Activity activity, Guid id);
        Task<bool> DeleteActivity(Guid id);
        Task<bool> UploadActivityImage(Guid id, IFormFile imageFile);
        Task<List<ActivityDto>> GetActivitiesByApiKey(string apiKey, string type);
        Task<ActivityDto> GetActivityByApiKeyAndActivityId(string apiKey, Guid activityId);
    }
}
