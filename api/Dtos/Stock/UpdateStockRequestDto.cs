namespace api.Dtos.Stock;

public class UpdateStockRequestDto
{
  public string Symbol { get; set; } = string.Empty;
  public string CompanyName { get; set; } = string.Empty;

  public decimal Price { get; set; }

  public decimal LastDiv { get; set; }

  public string Industry { get; set; } = string.Empty;
  public decimal MarketCap { get; set; }

}