namespace Activity.Repositories.Interface {
    public interface IActivityRepository {
        Task<List<Models.Entities.Activity>> GetActivitiesAsync(string type);
        Task<Models.Entities.Activity> GetActivityByIdAsync(Guid id);
        Task<Models.Entities.Activity> CreateActivityAsync(Models.Entities.Activity activity);
        Task<bool> UpdateActivityAsync(Models.Entities.Activity activity, Guid id);
        Task<bool> DeleteActivityAsync(Guid id);
        Task<bool> UploadActivityImageAsync(Guid id, IFormFile imageFile);
    }
}
