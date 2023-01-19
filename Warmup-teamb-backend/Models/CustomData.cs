using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Models;

public class CustomData
{

    [BsonId]

    [BsonRepresentation(BsonType.ObjectId)]

    public ObjectId? Id { get; set; } = null!; //unique document id.

    public string Message { get; set; } = null!;

    public long CreatedAt { get; set; }

    public string CategoryType { get; set; } = null!;

}