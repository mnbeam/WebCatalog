using MediatR;
using WebCatalog.Domain.Entities;

namespace WebCatalog.Logic.CQRS.Tokens.Commands.CreateAccessToken;

public class CreateAccessTokenCommand : IRequest<string>
{
    public AppUser AppUser { get; set; }
}