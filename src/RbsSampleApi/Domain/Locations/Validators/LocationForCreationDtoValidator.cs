namespace RbsSampleApi.Domain.Locations.Validators;

using FluentValidation;
using RBSSample.Shared.Dtos.Location;

public class LocationForCreationDtoValidator : AbstractValidator<LocationForCreationDto>
{
    public LocationForCreationDtoValidator()
    {
        // add fluent validation rules that should only be run on creation operations here
        //https://fluentvalidation.net/
    }
}