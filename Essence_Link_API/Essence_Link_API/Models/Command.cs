using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Essence_Link_API.Models;
public class Command
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string UserId { get; set; } = null!;
    public decimal TotalPrice { get; set; }
    public string Shipment_Status { get; set; } = null!;
    public string Shipment_Adress { get; set; } = null!;
    public string Date { get; set; } = null!;

}
