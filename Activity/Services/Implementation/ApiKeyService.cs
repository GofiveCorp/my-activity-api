using Activity.Models.Entities;
using Activity.Repositories.Interface;
using Activity.Services.Interface;

namespace Activity.Services.Implementation {
    public class ApiKeyService : IApiKeyService {
        private readonly IApiKeyRepository _apiKeyRepository;

        public ApiKeyService(IApiKeyRepository apiKeyRepository) {
            _apiKeyRepository = apiKeyRepository;
        }

        public async Task<ApiKey> GetApiKeyByValue(string apiKey) {
            return await _apiKeyRepository.GetApiKeyByValueAsync(apiKey);
        }
    }
}
