using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Essence_Link_API.Models;
public class ProductType
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string Name { get; set; } = null!;
    public string ImageNumber { get; set; } = null!;
    public string Description { get; set; } = null!;
    public List<string> Subtypes { get; set; }
}
