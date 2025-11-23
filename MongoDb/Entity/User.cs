using MongoDB.Bson.Serialization.Attributes;

namespace MongoDb.Entity;

public class User
{
    [BsonId]
    public int Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public DateOnly Birthdate { get; set; }
    public ICollection<Address> Addresses { get; set; }
}
