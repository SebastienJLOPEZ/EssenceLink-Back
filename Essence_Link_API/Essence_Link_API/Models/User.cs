using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Essence_Link_API.Models;
public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Number { get; set; } = null!;
    public string Role { get; set; } = null!;
    public string BDate { get; set; } = null!;
    public string SignInDate { get; set; } = null!;
    public string Status { get; set; } = null!;
}
