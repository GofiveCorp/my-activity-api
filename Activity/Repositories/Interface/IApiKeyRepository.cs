using Activity.Models.Entities;

namespace Activity.Repositories.Interface {
    public interface IApiKeyRepository {
        Task<ApiKey> GetApiKeyByValueAsync(string apiKey);
    }
}
