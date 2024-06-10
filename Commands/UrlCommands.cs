using MediatR;
using SubUrl.Models;

namespace SubUrl.Commands
{
    public record CreateUrlCommand(string LongValue, string ShortValue) : IRequest<Url>;
    public record AddUrlCommand(Url UrlToAdd) : IRequest;
    public record UpdateUrlCommand(Url UrlToUpdate, Url NewUrl) : IRequest;
    public record RemoveUrlCommand(Url UrlToRemove) : IRequest;
    public record IncUrlFollowCountCommand(Url Url) : IRequest;
}