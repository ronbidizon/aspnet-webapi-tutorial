using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Stock
{
    public class CreateStockRequestDto
    {
        [Required]
        [MinLength(10, ErrorMessage = "Symbol must be at least 10 character long.")]
        public string Symbol { get; set; } = string.Empty;

        [Required]
        [MinLength(20, ErrorMessage = "Company name must be at least 20 characters long.")]
        public string CompanyName { get; set; } = string.Empty;

        [Required]
        [Range(1,10000000)]
        public decimal Purchase { get; set; }
        [Required]
        [Range(0.001, 1000)]
        public decimal LastDiv { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Industry cannot exceed 100 characters.")]
        public string Industry { get; set; } = string.Empty;

        [Required]
        [Range(1, 5000000000)]
        public int MarketCap { get; set; }
    }
}