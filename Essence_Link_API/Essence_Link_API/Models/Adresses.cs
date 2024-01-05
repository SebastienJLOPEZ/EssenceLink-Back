using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Essence_Link_API.Models;
public class Adresses
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string UserId { get; set; } = null!;
    public string NumberName { get; set; } = null!;
    public string PostalCode {  get; set; } = null!;
    public string City { get; set; } = null!;
}
