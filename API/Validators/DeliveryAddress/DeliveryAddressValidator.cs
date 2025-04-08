using Data.Dtos.DeliveryAddresses;
using FluentValidation;

namespace API.Validators.DeliveryAddress
{
    public class DeliveryAddressValidator : AbstractValidator<DeliveryAddressCreateDto>
    {
        public DeliveryAddressValidator()
        {
            RuleFor(x => x.Country).NotEmpty().WithMessage("Ülke bilgisi zorunludur.");
            RuleFor(x => x.Province).NotEmpty().WithMessage("İl bilgisi zorunludur.");
            RuleFor(x => x.District).NotEmpty().WithMessage("İlçe bilgisi zorunludur.");
            RuleFor(x => x.PostalCode).NotEmpty().WithMessage("Posta kodu zorunludur.");
        }
    }
}
