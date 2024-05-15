﻿using System.ComponentModel.DataAnnotations;

namespace MagicVilla_CouponAPI.Models.DTO
{
    public class CouponCreateDTO
    {
        [Required]
        public string Name { get; set; }

        public int Percentage { get; set; }

        public bool IsActive { get; set; }
    }
}
