using MediatR;
using SubUrl.MediatR.Commands;
using SubUrl.Repositories;

namespace SubUrl.MediatR.Handlers
{
    public class RemoveUrlHandler : IRequestHandler<RemoveUrlCommand>
    {
        private readonly UrlRepository _urlRepo;

        public RemoveUrlHandler(UrlRepository urlRepo)
        {
            _urlRepo = urlRepo;
        }

        public async Task Handle(RemoveUrlCommand request, CancellationToken cancellationToken)
            => await _urlRepo.RemoveAsync(request.UrlToRemove);
    }
}