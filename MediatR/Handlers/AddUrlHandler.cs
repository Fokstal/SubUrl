using MediatR;
using SubUrl.MediatR.Commands;
using SubUrl.Repositories;

namespace SubUrl.MediatR.Handlers
{
    public class AddUrlHandler : IRequestHandler<AddUrlCommand>
    {
        private readonly UrlRepository _urlRepo;

        public AddUrlHandler(UrlRepository urlRepo)
        {
            _urlRepo = urlRepo;
        }

        public async Task Handle(AddUrlCommand request, CancellationToken cancellationToken)
            => await _urlRepo.AddAsync(request.UrlToAdd);
    }
}