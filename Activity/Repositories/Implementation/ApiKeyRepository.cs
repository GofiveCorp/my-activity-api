using Activity.Data;
using Activity.Models.Entities;
using Activity.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace Activity.Repositories.Implementation {
    public class ApiKeyRepository : IApiKeyRepository {
        private readonly ApplicationDbContext _context;

        public ApiKeyRepository(ApplicationDbContext context) {
            _context = context;
        }

        public async Task<ApiKey> GetApiKeyByValueAsync(string apiKey) {
            return await _context.ApiKeys.FirstOrDefaultAsync(n => n.Value == apiKey);
        }
    }
}
