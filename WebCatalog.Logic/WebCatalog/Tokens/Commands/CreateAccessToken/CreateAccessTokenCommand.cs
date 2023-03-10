using MediatR;
using WebCatalog.Domain.Entities;

namespace WebCatalog.Logic.WebCatalog.Tokens.Commands.CreateAccessToken;

public class CreateAccessTokenCommand : IRequest<string>
{
    public AppUser AppUser { get; set; }
}