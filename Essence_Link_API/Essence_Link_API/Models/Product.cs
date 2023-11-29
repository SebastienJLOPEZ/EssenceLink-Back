using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Essence_Link_API.Models;
public class Product
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string Type { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string DateAdded { get; set; } = null!;
    public decimal Score { get; set; }

}
