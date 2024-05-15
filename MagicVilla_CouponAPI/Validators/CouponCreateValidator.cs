using FluentValidation;
using MagicVilla_CouponAPI.Models.DTO;

namespace MagicVilla_CouponAPI.Validators
{
    public class CouponCreateValidator : AbstractValidator<CouponCreateDTO>
    {
        public CouponCreateValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Percentage).InclusiveBetween(1, 100);
        }
    }
}
