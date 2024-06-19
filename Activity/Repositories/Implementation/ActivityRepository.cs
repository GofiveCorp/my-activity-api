using Activity.Data;
using Activity.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace Activity.Repositories.Implementation {
    public class ActivityRepository : IActivityRepository {
        private readonly ApplicationDbContext _context;

        public ActivityRepository(ApplicationDbContext context) {
            _context = context;
        }

        public async Task<Models.Entities.Activity> CreateActivityAsync(Models.Entities.Activity activity) {
            await _context.Activities.AddAsync(activity);
            await _context.SaveChangesAsync();
            return activity;
        }

        public async Task<bool> DeleteActivityAsync(Guid id) {
            bool result = false;
            var activity = await _context.Activities.FirstOrDefaultAsync(n => n.Id == id);

            if (activity != null) {
                _context.Activities.Remove(activity);
                await _context.SaveChangesAsync();
                result = true;
            }

            return result;
        }

        public async Task<List<Models.Entities.Activity>> GetActivitiesAsync(string type) {
            if (string.IsNullOrEmpty(type)) {
                return await _context.Activities.ToListAsync();
            }

            return await _context.Activities.Where(n => n.Type.ToLower() == type.ToLower()).ToListAsync();
        }

        public async Task<Models.Entities.Activity> GetActivityByIdAsync(Guid id) {
            return await _context.Activities.FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task<bool> UpdateActivityAsync(Models.Entities.Activity activity, Guid id) {
            bool result = false;
            var existingActivity = await _context.Activities.FirstOrDefaultAsync(a => a.Id == id);
            if (existingActivity != null) {
                existingActivity.Title = activity.Title;
                existingActivity.Description = activity.Description;
                existingActivity.Type = activity.Type;
                existingActivity.StartTime = activity.StartTime;
                existingActivity.EndTime = activity.EndTime;
                existingActivity.Date = activity.Date;
                existingActivity.Barometer = activity.Barometer;

                await _context.SaveChangesAsync();
                result = true;
            }

            return result;
        }

        public async Task<bool> UploadActivityImageAsync(Guid id, IFormFile imageFile) {
            bool result = false;
            var activity = await _context.Activities.FirstOrDefaultAsync(a => a.Id == id);
            if (activity != null) {
                if (imageFile != null && imageFile.Length > 0) {
                    using (var memoryStream = new MemoryStream()) {
                        await imageFile.CopyToAsync(memoryStream);
                        activity.Image = memoryStream.ToArray();
                    }
                    await _context.SaveChangesAsync();
                    result = true;
                }
            }

            return result;
        }

        public async Task<List<Models.Entities.Activity>> GetActivitiesByApiKeyAsync(string apiKey, string type) {
            if (string.IsNullOrEmpty(type)) {
                return await _context.Activities.Where(a => a.ApiKey.Value == apiKey).ToListAsync();
            }

            return await _context.Activities
                .Where(a => a.ApiKey.Value == apiKey && a.Type.ToLower() == type.ToLower())
                .ToListAsync();
        }

        public async Task<Models.Entities.Activity> GetActivityByApiKeyAndActivityIdAsync(string apiKey, Guid activityId) {
            return await _context.Activities.FirstOrDefaultAsync(a => a.ApiKey.Value == apiKey && a.Id == activityId);
        }
    }
}
