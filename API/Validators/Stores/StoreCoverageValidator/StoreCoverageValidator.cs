using Data.Dtos.Stores.Locations;
using FluentValidation;

namespace API.Validators.Stores.StoreCoverageValidator
{
    public class StoreCoverageValidator
    {
        public class StoreCoverageCreateValidator : AbstractValidator<StoreCoverageCreateDto>
        {
            public StoreCoverageCreateValidator()
            {
                RuleFor(x => x.RegionIds).NotNull();
                RuleFor(x => x.CountryIds).NotNull();
            }
        }

        public class StoreCoverageDeleteValidator : AbstractValidator<StoreCoverageDeleteDto>
        {
            public StoreCoverageDeleteValidator()
            {
                RuleFor(x => x.RegionIds).NotNull();
                RuleFor(x => x.CountryIds).NotNull();
            }
        }
    }
}
