using MediatR;
using SubUrl.Models.DTO;
using SubUrl.Models.Entities;

namespace SubUrl.MediatR.Commands
{
    public record CreateUrlCommand(string LongValue, string ShortValue) : IRequest<UrlEntity>;
    public record AddUrlCommand(UrlEntity UrlToAdd) : IRequest;
    public record UpdateUrlCommand(UrlEntity UrlToUpdate, UrlDTO UrlDTO) : IRequest;
    public record RemoveUrlCommand(UrlEntity UrlToRemove) : IRequest;
    public record IncUrlFollowCountCommand(UrlEntity Url) : IRequest;
}