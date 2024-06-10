using MediatR;
using Microsoft.EntityFrameworkCore;
using SubUrl.Commands;
using SubUrl.Data;
using SubUrl.Models;
using SubUrl.Queries;

namespace SubUrl.Handlers
{
    public class GetUrlListHandler : IRequestHandler<GetUrlListQuery, IEnumerable<Url>>
    {
        private readonly AppDbContext _db;

        public GetUrlListHandler(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Url>> Handle(GetUrlListQuery request, CancellationToken cancellationToken)
            => await _db.Url.ToListAsync();
    }

    public class GetUrlByIdHandler : IRequestHandler<GetUrlByIdQuery, Url?>
    {
        private readonly AppDbContext _db;

        public GetUrlByIdHandler(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Url?> Handle(GetUrlByIdQuery request, CancellationToken cancellationToken)
            => await _db.Url.FirstOrDefaultAsync(u => u.Id == request.Id);
    }

    public class GetUrlByLongValueHandler : IRequestHandler<GetUrlByLongValueQuery, Url?>
    {
        private readonly AppDbContext _db;

        public GetUrlByLongValueHandler(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Url?> Handle(GetUrlByLongValueQuery request, CancellationToken cancellationToken)
            => await _db.Url.FirstOrDefaultAsync(u => u.LongValue.ToLower() == request.LongValue.ToLower());
    }

    public class GetUrlByShortValueHandler : IRequestHandler<GetUrlByShortValueQuery, Url?>
    {
        private readonly AppDbContext _db;

        public GetUrlByShortValueHandler(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Url?> Handle(GetUrlByShortValueQuery request, CancellationToken cancellationToken)
            => await _db.Url.FirstOrDefaultAsync(u => u.ShortValue.ToLower() == request.ShortValue.ToLower());
    }

    public class CreateUrlHandler : IRequestHandler<CreateUrlCommand, Url>
    {
        private readonly AppDbContext _db;

        public CreateUrlHandler(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Url> Handle(CreateUrlCommand request, CancellationToken cancellationToken)
        {
            Url url = new()
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
            request.UrlToUpdate.LongValue = request.NewUrl.LongValue;
            request.UrlToUpdate.ShortValue = request.NewUrl.ShortValue;
            request.UrlToUpdate.DateCreated = request.NewUrl.DateCreated;

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