using MediatR;
using SubUrl.MediatR.Queries;
using SubUrl.Models.Entities;
using SubUrl.Repositories;

namespace SubUrl.MediatR.Handlers
{
    public class GetUrlByLongValueHandler : IRequestHandler<GetUrlByLongValueQuery, UrlEntity?>
    {
        private readonly UrlRepository _urlRepo;

        public GetUrlByLongValueHandler(UrlRepository _urlRepo)
        {
            this._urlRepo = _urlRepo;
        }

        public async Task<UrlEntity?> Handle(GetUrlByLongValueQuery request, CancellationToken cancellationToken)
        {
            UrlEntity[] urlList = await _urlRepo.GetListAsync();

            return urlList.FirstOrDefault(u => u.LongValue == request.LongValue);
        }
    }
}