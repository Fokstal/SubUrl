using MediatR;
using SubUrl.Models;

namespace SubUrl.Queries
{
    public record GetUrlListQuery() : IRequest<IEnumerable<Url>>;
    public record GetUrlByIdQuery(int Id) : IRequest<Url?>;
    public record GetUrlByLongValueQuery(string LongValue) : IRequest<Url?>;
    public record GetUrlByShortValueQuery(string ShortValue) : IRequest<Url?>;
}