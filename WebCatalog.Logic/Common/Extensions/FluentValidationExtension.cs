using FluentValidation;

namespace WebCatalog.Logic.Common.Extensions;

public static class FluentValidationExtension
{
    public static IRuleBuilderOptions<T, string> ValidateCategoryName<T>(
        this IRuleBuilder<T, string?> ruleBuilder) where T : class
    {
        return ruleBuilder
            .NotEmpty().WithMessage("Category name can not be empty.")
            .MaximumLength(100).WithMessage("Too long length of category name.");
    }

    public static IRuleBuilderOptions<T, int> ValidateId<T>(this IRuleBuilder<T, int> ruleBuilder)
        where T : class
    {
        return ruleBuilder
            .Must(id => id >= 1).WithMessage("Invalid Id.");
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