using MediatR;
using SubUrl.MediatR.Queries;
using SubUrl.Models.Entities;
using SubUrl.Repositories;

namespace SubUrl.MediatR.Handlers
{
    public class GetUrlByShortValueHandler : IRequestHandler<GetUrlByShortValueQuery, UrlEntity?>
    {
        private readonly UrlRepository _urlRepo;

        public GetUrlByShortValueHandler(UrlRepository urlRepo)
        {
            _urlRepo = urlRepo;
        }

        public async Task<UrlEntity?> Handle(GetUrlByShortValueQuery request, CancellationToken cancellationToken)
        {
            UrlEntity[] urlList = await _urlRepo.GetListAsync();

            return urlList.FirstOrDefault(u => u.ShortValue == request.ShortValue);
        }
    }
}