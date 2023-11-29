using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Essence_Link_API.Models;
public class CommandProduct
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string ProductId { get; set; } = null!;
    public string CommandId { get; set; } = null!;
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }

}
