using FluentValidation;
using FluentValidation.Results;
using SimRegisPortal.Core.Localization;

namespace SimRegisPortal.Application.Extensions;

public static class FluentValidationExtensions
{
    public static IRuleBuilderOptions<T, TProperty> WithTemplate<T, TProperty>(
        this IRuleBuilderOptions<T, TProperty> rule,
        string template,
        params object[] parameters)
    {
        return rule.WithMessage(template).WithState(x => parameters);
    }

    public static string GetLocalizedMessage(this ValidationFailure failure, IErrorLocalizer errorLocalizer)
    {
        return errorLocalizer.Localize(failure.ErrorMessage, (object[])failure.CustomState);
    }
}
