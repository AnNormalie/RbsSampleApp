namespace RbsSampleApi.Domain.Locations.Validators;

using RBSSample.Shared.Dtos.Location;
using FluentValidation;

public class LocationForUpdateDtoValidator : AbstractValidator<LocationForUpdateDto>
{
    public LocationForUpdateDtoValidator()
    {
        // add fluent validation rules that should only be run on update operations here
        //https://fluentvalidation.net/
    }
}