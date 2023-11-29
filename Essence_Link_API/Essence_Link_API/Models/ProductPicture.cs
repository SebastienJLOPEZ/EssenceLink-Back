using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Essence_Link_API.Models;
public class ProductPicture
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string ProductId { get; set; } = null;
    public string Name { get; set; } = null!;
}
