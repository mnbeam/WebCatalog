using MediatR;

namespace WebCatalog.Logic.CQRS.Tokens.Commands.CreateRefreshToken;

internal class CreateRefreshTokenCommand : IRequest<string>
{
    public int UserId { get; set; }
}