using MediatR;
using SubUrl.MediatR.Queries;
using SubUrl.Models.Entities;
using SubUrl.Repositories;

namespace SubUrl.MediatR.Handlers
{
    public class GetUrlListHandler : IRequestHandler<GetUrlListQuery, UrlEntity[]>
    {
        private readonly UrlRepository _urlRepo;

        public GetUrlListHandler(UrlRepository urlRepo)
        {
            _urlRepo = urlRepo;
        }

        public async Task<UrlEntity[]> Handle(GetUrlListQuery request, CancellationToken cancellationToken)
            => await _urlRepo.GetListAsync();
    }
}