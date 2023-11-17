using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Essence_Link_API.Models;
public class Review
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public string? Id { get; set; }

    public string UserId { get; set; } = null!;
    public string ProductId { get; set; } = null!;
    public decimal Score { get; set; }
    public string ReviewText { get; set; } = null;
}
