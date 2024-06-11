using MediatR;
using Microsoft.EntityFrameworkCore;
using SubUrl.Data;
using SubUrl.Models.Entities;
using SubUrl.MediatR.Commands;
using SubUrl.MediatR.Queries;

namespace SubUrl.MediatR.Handlers
{
    public class GetUrlListHandler : IRequestHandler<GetUrlListQuery, IEnumerable<UrlEntity>>
    {
        private readonly AppDbContext _db;

        public GetUrlListHandler(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<UrlEntity>> Handle(GetUrlListQuery request, CancellationToken cancellationToken)
            => await _db.Url.ToListAsync();
    }

    public class GetUrlByIdHandler : IRequestHandler<GetUrlByIdQuery, UrlEntity?>
    {
        private readonly AppDbContext _db;

        public GetUrlByIdHandler(AppDbContext db)
        {
            _db = db;
        }

        public async Task<UrlEntity?> Handle(GetUrlByIdQuery request, CancellationToken cancellationToken)
            => await _db.Url.FirstOrDefaultAsync(u => u.Id == request.Id);
    }

    public class GetUrlByLongValueHandler : IRequestHandler<GetUrlByLongValueQuery, UrlEntity?>
    {
        private readonly AppDbContext _db;

        public GetUrlByLongValueHandler(AppDbContext db)
        {
            _db = db;
        }

        public async Task<UrlEntity?> Handle(GetUrlByLongValueQuery request, CancellationToken cancellationToken)
            => await _db.Url.FirstOrDefaultAsync(u => u.LongValue.ToLower() == request.LongValue.ToLower());
    }

    public class GetUrlByShortValueHandler : IRequestHandler<GetUrlByShortValueQuery, UrlEntity?>
    {
        private readonly AppDbContext _db;

        public GetUrlByShortValueHandler(AppDbContext db)
        {
            _db = db;
        }

        public async Task<UrlEntity?> Handle(GetUrlByShortValueQuery request, CancellationToken cancellationToken)
            => await _db.Url.FirstOrDefaultAsync(u => u.ShortValue.ToLower() == request.ShortValue.ToLower());
    }

    public class CreateUrlHandler : IRequestHandler<CreateUrlCommand, UrlEntity>
    {
        private readonly AppDbContext _db;

        public CreateUrlHandler(AppDbContext db)
        {
            _db = db;
        }

        public async Task<UrlEntity> Handle(CreateUrlCommand request, CancellationToken cancellationToken)
        {
            UrlEntity url = new()
            {
                LongValue = request.LongValue,
                ShortValue = request.ShortValue,
                DateCreated = DateTime.Now,
            };

            return await Task.FromResult(url);
        }
    }

    public class AddUrlHandler : IRequestHandler<AddUrlCommand>
    {
        private readonly AppDbContext _db;

        public AddUrlHandler(AppDbContext db)
        {
            _db = db;
        }

        public async Task Handle(AddUrlCommand request, CancellationToken cancellationToken)
        {
            await _db.AddAsync(request.UrlToAdd);

            await _db.SaveChangesAsync();

            return;
        }
    }

    public class UpdateUrlHandler : IRequestHandler<UpdateUrlCommand>
    {
        private readonly AppDbContext _db;

        public UpdateUrlHandler(AppDbContext db)
        {
            _db = db;
        }

        public async Task Handle(UpdateUrlCommand request, CancellationToken cancellationToken)
        {
            request.UrlToUpdate.LongValue = request.UrlDTO.LongValue;
            request.UrlToUpdate.ShortValue = request.UrlDTO.ShortValue;
            request.UrlToUpdate.DateCreated = DateTime.Now;

            await _db.SaveChangesAsync();

            return;
        }
    }

    public class IncUrlFollowCountHandler : IRequestHandler<IncUrlFollowCountCommand>
    {
        private readonly AppDbContext _db;

        public IncUrlFollowCountHandler(AppDbContext db)
        {
            _db = db;
        }

        public async Task Handle(IncUrlFollowCountCommand request, CancellationToken cancellationToken)
        {
            request.Url.FollowCount++;

            await _db.SaveChangesAsync();

            return;
        }
    }

    public class RemoveUrlHandler : IRequestHandler<RemoveUrlCommand>
    {
        private readonly AppDbContext _db;

        public RemoveUrlHandler(AppDbContext db)
        {
            _db = db;
        }

        public async Task Handle(RemoveUrlCommand request, CancellationToken cancellationToken)
        {
            _db.Remove(request.UrlToRemove);

            await _db.SaveChangesAsync();

            return;
        }
    }
}