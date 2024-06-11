using Microsoft.EntityFrameworkCore;
using SubUrl.Data;
using SubUrl.Models.DTO;
using SubUrl.Models.Entities;

namespace SubUrl.Repositories
{
    public class UrlRepository : DataRepositoryBase<UrlEntity, UrlDTO>
    {
        public UrlRepository(AppDbContext db) : base(db) {}

        public override UrlEntity CreateAsync(UrlDTO valueDTO) => throw new NotImplementedException();
        public UrlEntity CreateAsync(UrlCreateDTO urlDTO)
        {
            return new()
            {
                LongValue = urlDTO.LongValue,
                ShortValue = GenerateUniqueShortValue(urlDTO.LongValue).Result,
                DateCreated = DateTime.Now,
            };
        }

        public async Task IncFollowCountAsync(UrlEntity urlToUpdate)
        {
            urlToUpdate.FollowCount++;

            await _db.SaveChangesAsync();
        }

        public override async Task UpdateAsync(UrlEntity urlToUpdate, UrlDTO urlDTO)
        {
            urlToUpdate.LongValue = urlDTO.LongValue;
            urlToUpdate.ShortValue = urlDTO.ShortValue;
            urlToUpdate.DateCreated = DateTime.Now;

            await _db.SaveChangesAsync();
        }

        public async Task<string> GenerateUniqueShortValue(string longValue)
        {
            int shortValueLength = 6;

            string shortValue = Guid.NewGuid().ToString()[..shortValueLength];

            UrlEntity? url = await _db.Url.FirstOrDefaultAsync(u => u.ShortValue == shortValue);

            if (url is not null)
            {
                await GenerateUniqueShortValue(longValue);
            }

            return shortValue;
        }
    }
}