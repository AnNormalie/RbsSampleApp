namespace RbsSampleApi.Domain.Languages.Validators;

using FluentValidation;
using RBSSample.Shared.Dtos.Language;

public class LanguageForUpdateDtoValidator : AbstractValidator<LanguageForUpdateDto>
{
    public LanguageForUpdateDtoValidator()
    {
        // add fluent validation rules that should only be run on update operations here
        //https://fluentvalidation.net/
    }
}