using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Essence_Link_API.Models;
public class Wishlist
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string UserId { get; set; } = null!;
    public string ProductId { get; set; } = null!;
}
