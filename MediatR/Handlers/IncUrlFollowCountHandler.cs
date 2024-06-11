using MediatR;
using SubUrl.MediatR.Commands;
using SubUrl.Models.DTO;
using SubUrl.Repositories;

namespace SubUrl.MediatR.Handlers
{
    public class IncUrlFollowCountHandler : IRequestHandler<IncUrlFollowCountCommand>
    {
        private readonly UrlRepository _urlRepo;

        public IncUrlFollowCountHandler(UrlRepository urlRepo)
        {
            _urlRepo = urlRepo;
        }

        public async Task Handle(IncUrlFollowCountCommand request, CancellationToken cancellationToken)
            => await _urlRepo.IncFollowCountAsync(request.UrlToUpdate);
    }
}