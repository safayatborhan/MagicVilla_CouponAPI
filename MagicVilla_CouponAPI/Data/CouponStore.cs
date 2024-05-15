using MagicVilla_CouponAPI.Models;

namespace MagicVilla_CouponAPI.Data;

public static class CouponStore
{
    public static List<Coupon> coupons = new List<Coupon>
    {
        new Coupon
        {
            Id = 1,
            Name = "100FF",
            Percentage = 10,
            IsActive = true,
            Created = null,
            LastUpdated = null,
        },
        new Coupon
        {
            Id = 2,
            Name = "200FF",
            Percentage = 20,
            IsActive = true,
            Created = null,
            LastUpdated = null,
        },
    };
}
