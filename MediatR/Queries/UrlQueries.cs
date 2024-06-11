using MediatR;
using SubUrl.Models.Entities;

namespace SubUrl.MediatR.Queries
{
    public record GetUrlListQuery() : IRequest<IEnumerable<UrlEntity>>;
    public record GetUrlByIdQuery(int Id) : IRequest<UrlEntity?>;
    public record GetUrlByLongValueQuery(string LongValue) : IRequest<UrlEntity?>;
    public record GetUrlByShortValueQuery(string ShortValue) : IRequest<UrlEntity?>;
}