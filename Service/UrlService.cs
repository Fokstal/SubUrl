using Microsoft.EntityFrameworkCore;
using SubUrl.Data;
using SubUrl.Models;

namespace SubUrl.Service
{
    public class UrlService
    {
        private readonly AppDbContext _db;

        public UrlService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<string> GenerateUniqueShortValueByLongValue(string longValue)
        {
            string shortValue = HashWorker.GenerateSHA512HashInLengthWithSalt(longValue, 6);

            Url? url = await _db.Url.FirstOrDefaultAsync(urlDb => urlDb.ShortValue == shortValue);

            while (url is not null)
            {
                shortValue = HashWorker.GenerateSHA512HashInLengthWithSalt(shortValue, 6);

                url = await _db.Url.FirstOrDefaultAsync(urlDb => urlDb.ShortValue == shortValue);
            }

            return shortValue;
        }
    }
}