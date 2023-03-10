using MediatR;

namespace WebCatalog.Logic.WebCatalog.Tokens.Commands.CreateRefreshToken;

internal class CreateRefreshTokenCommand : IRequest<string>
{
    public int UserId { get; set; }
}