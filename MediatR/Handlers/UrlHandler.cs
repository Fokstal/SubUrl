using MediatR;
using SubUrl.MediatR.Commands;
using SubUrl.MediatR.Queries;
using SubUrl.Models.Entities;
using SubUrl.Repositories;

namespace SubUrl.MediatR.Handlers
{
    public class UrlHandler :
        IRequestHandler<AddUrlCommand>,
        IRequestHandler<CreateUrlCommand, UrlEntity>,
        IRequestHandler<GetUrlByIdQuery, UrlEntity?>,
        IRequestHandler<GetUrlByLongValueQuery, UrlEntity?>,
        IRequestHandler<GetUrlByShortValueQuery, UrlEntity?>,
        IRequestHandler<GetUrlListQuery, UrlEntity[]>,
        IRequestHandler<IncUrlFollowCountCommand>,
        IRequestHandler<RemoveUrlCommand>,
        IRequestHandler<RemoveUrlListCommand>,
        IRequestHandler<UpdateUrlCommand>
    {
        private readonly UrlRepository _urlRepo;

        public UrlHandler(UrlRepository urlRepo)
        {
            _urlRepo = urlRepo;
        }

        public async Task Handle(AddUrlCommand request, CancellationToken cancellationToken)
            => await _urlRepo.AddAsync(request.UrlToAdd);

        public async Task<UrlEntity> Handle(CreateUrlCommand request, CancellationToken cancellationToken)
            => await Task.FromResult(_urlRepo.CreateAsync(request.UrlDTO));

        public async Task<UrlEntity?> Handle(GetUrlByIdQuery request, CancellationToken cancellationToken)
            => await _urlRepo.GetByIdAsync(request.Id);

        public async Task<UrlEntity?> Handle(GetUrlByLongValueQuery request, CancellationToken cancellationToken)
        {
            UrlEntity[] urlList = await _urlRepo.GetListAsync();

            return urlList.FirstOrDefault(u => u.LongValue == request.LongValue);
        }

        public async Task<UrlEntity?> Handle(GetUrlByShortValueQuery request, CancellationToken cancellationToken)
        {
            UrlEntity[] urlList = await _urlRepo.GetListAsync();

            return urlList.FirstOrDefault(u => u.ShortValue == request.ShortValue);
        }

        public async Task<UrlEntity[]> Handle(GetUrlListQuery request, CancellationToken cancellationToken)
            => await _urlRepo.GetListAsync();

        public async Task Handle(IncUrlFollowCountCommand request, CancellationToken cancellationToken)
            => await _urlRepo.IncFollowCountAsync(request.UrlToUpdate);

        public async Task Handle(RemoveUrlCommand request, CancellationToken cancellationToken)
            => await _urlRepo.RemoveAsync(request.UrlToRemove);

        public async Task Handle(RemoveUrlListCommand request, CancellationToken cancellationToken)
        {
            UrlEntity[] urlList = await _urlRepo.GetListAsync();

            foreach (UrlEntity url in urlList)
            {
                await _urlRepo.RemoveAsync(url);
            }
        }

        public async Task Handle(UpdateUrlCommand request, CancellationToken cancellationToken)
            => await _urlRepo.UpdateAsync(request.UrlToUpdate, request.UrlDTO);
    }
}