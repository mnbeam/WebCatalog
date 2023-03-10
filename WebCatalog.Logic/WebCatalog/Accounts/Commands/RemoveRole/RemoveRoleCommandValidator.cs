using FluentValidation;

namespace WebCatalog.Logic.WebCatalog.Accounts.Commands.RemoveRole;

public class RemoveRoleCommandValidator : AbstractValidator<RemoveRoleCommand>
{
    public RemoveRoleCommandValidator()
    {
        RuleFor(v => v.Role)
            .IsInEnum().WithMessage("InvalidRole");
    }
}