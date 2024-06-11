using MediatR;
using SubUrl.MediatR.Commands;
using SubUrl.Repositories;

namespace SubUrl.MediatR.Handlers
{
    public class UpdateUrlHandler : IRequestHandler<UpdateUrlCommand>
    {
        private readonly UrlRepository _urlRepo;

        public UpdateUrlHandler(UrlRepository urlRepo)
        {
            _urlRepo = urlRepo;
        }

        public async Task Handle(UpdateUrlCommand request, CancellationToken cancellationToken)
            => await _urlRepo.UpdateAsync(request.UrlToUpdate, request.UrlDTO);
    }
}