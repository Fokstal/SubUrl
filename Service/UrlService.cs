using Microsoft.EntityFrameworkCore;
using SubUrl.Data;
using SubUrl.Models;

namespace SubUrl.Service
{
    public static class UrlService
    {
        public async static Task<string> GenerateUniqueShortValueByLongValue(string longValue)
        {
            string shortValue = HashWorker.GenerateSHA512HashInLengthWithSalt(longValue, 6);

            using (AppDbContext db = new())
            {
                Url? url = await db.Url.FirstOrDefaultAsync(urlDb => urlDb.ShortValue == shortValue);

                while (url is not null)
                {
                    shortValue = HashWorker.GenerateSHA512HashInLengthWithSalt(shortValue, 6);

                    url = await db.Url.FirstOrDefaultAsync(urlDb => urlDb.ShortValue == shortValue);
                }
            }

            return shortValue;
        }
    }
}