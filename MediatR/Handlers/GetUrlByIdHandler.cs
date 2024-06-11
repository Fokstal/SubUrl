using MediatR;
using SubUrl.MediatR.Queries;
using SubUrl.Models.Entities;
using SubUrl.Repositories;

namespace SubUrl.MediatR.Handlers
{
    public class GetUrlByIdHandler : IRequestHandler<GetUrlByIdQuery, UrlEntity?>
    {
        private readonly UrlRepository _urlRepo;

        public GetUrlByIdHandler(UrlRepository urlRepo)
        {
            _urlRepo = urlRepo;
        }
        public async Task<UrlEntity?> Handle(GetUrlByIdQuery request, CancellationToken cancellationToken)
            => await _urlRepo.GetByIdAsync(request.Id);
    }
}