using FluentValidation;

namespace WebCatalog.Logic.Common.Extensions;

public static class FluentValidationExtension
{
    public static IRuleBuilderOptions<T, string> ValidateName<T>(
        this IRuleBuilder<T, string?> ruleBuilder) where T : class
    {
        return ruleBuilder
            .NotEmpty().WithMessage("Name can not be empty.")
            .MaximumLength(100).WithMessage("Too long length of Name.");
    }

    public static IRuleBuilderOptions<T, int> MustBePositive<T>(
        this IRuleBuilder<T, int> ruleBuilder)
        where T : class
    {
        return ruleBuilder
            .Must(id => id >= 1).WithMessage("Must be positive number.");
    }

    public static IRuleBuilderOptions<T, int> ValidateRating<T>(
        this IRuleBuilder<T, int> ruleBuilder)
        where T : class
    {
        return ruleBuilder
            .Must(rating => rating >= 1 && rating <= 5)
            .WithMessage("Rating must be between 1 and 5");
    }
}