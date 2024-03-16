using common;
using TechTrain.ReusableModules.WebApi.Common;

namespace TechTrain.ReusableModules.WebApi.Models;

public record CartItem {
    public int Id { get; set; }
    public int CartId { get; set; }
    public int ItemId { get; set; }
	public string? ItemName {get; set;}
    public decimal ItemPrice {get; set;}
    public string? ItemCurrencyCode {get; set;}
}