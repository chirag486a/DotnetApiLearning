using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Stock;

public class UpdateStockRequestDto
{

  [Required]
  [MaxLength(10, ErrorMessage = "Symbol cannot be over 10 characters")]
  public string Symbol { get; set; } = string.Empty;
  [Required]
  [MaxLength(255, ErrorMessage = "Company name cannot be over 255 names")]
  public string CompanyName { get; set; } = string.Empty;

  [Required]
  [Range(1, 100000000000)]
  public decimal Price { get; set; }

  [Required]
  [Range(0.00001, 100)]
  public decimal LastDiv { get; set; }

  [Required]
  [MaxLength(50, ErrorMessage = "Industry cannot be over 10 characters")]
  public string Industry { get; set; } = string.Empty;
  [Required]
  [Range(1, 1000000000000)]
  public decimal MarketCap { get; set; }

}