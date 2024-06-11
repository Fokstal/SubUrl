using Microsoft.EntityFrameworkCore;
using SubUrl.Data;
using SubUrl.Models.Entities;

namespace SubUrl.Service
{
    public class UrlService
    {
        private readonly AppDbContext _db;
        private readonly int shortValueLength = 6;

        public UrlService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<string> GenerateUniqueShortValueByLongValue(string longValue)
        {
            string shortValue =  Guid.NewGuid().ToString()[..shortValueLength];

            UrlEntity? url = await _db.Url.FirstOrDefaultAsync(urlDb => urlDb.ShortValue == shortValue);

            if (url is not null)
            {
                await GenerateUniqueShortValueByLongValue(longValue);
            }

            return shortValue;
        }
    }
}