namespace RbsSampleApi.Domain.Languages.Validators;

using RBSSample.Shared.Dtos.Language;
using FluentValidation;

public class LanguageForCreationDtoValidator : AbstractValidator<LanguageForCreationDto>
{
    public LanguageForCreationDtoValidator()
    {
        // add fluent validation rules that should only be run on creation operations here
        //https://fluentvalidation.net/
    }
}