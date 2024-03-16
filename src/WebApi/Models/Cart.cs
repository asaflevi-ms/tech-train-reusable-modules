using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace TechTrain.ReusableModules.WebApi.Models;

[DataContract]
public record Cart {
    public required int CartId {get; set;}
    public int NumberOfItemsInCart { get; set; }
    public required decimal SumTotal {get; set;}
    public required string Currency {get; set;}
}