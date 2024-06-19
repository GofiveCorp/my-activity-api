using Activity.Models.Entities;

namespace Activity.Services.Interface {
    public interface IApiKeyService {
        Task<ApiKey> GetApiKeyByValue(string apiKey);
    }
}
