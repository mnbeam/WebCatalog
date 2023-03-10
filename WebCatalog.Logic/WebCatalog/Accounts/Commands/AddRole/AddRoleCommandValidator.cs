using FluentValidation;

namespace WebCatalog.Logic.WebCatalog.Accounts.Commands.AddRole;

public class AddRoleCommandValidator : AbstractValidator<AddRoleCommand>
{
    public AddRoleCommandValidator()
    {
        RuleFor(v => v.Role)
            .IsInEnum().WithMessage("InvalidRole");
    }
}