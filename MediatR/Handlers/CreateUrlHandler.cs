using MediatR;
using SubUrl.MediatR.Commands;
using SubUrl.Models.Entities;
using SubUrl.Repositories;

namespace SubUrl.MediatR.Handlers
{
    public class CreateUrlHandler : IRequestHandler<CreateUrlCommand, UrlEntity>
    {
        private readonly UrlRepository _urlRepo;

        public CreateUrlHandler(UrlRepository urlRepo)
        {
            _urlRepo = urlRepo;
        }

        public async Task<UrlEntity> Handle(CreateUrlCommand request, CancellationToken cancellationToken)
            => await Task.FromResult(_urlRepo.CreateAsync(request.UrlDTO));
    }
}