using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDb.Entity;

public class User
{
    public void UpdateUser(string name,string lastName,DateTime birthDate)
    {
        this.Name = name;
        this.LastName = lastName;
        this.Birthdate = birthDate;
    }

    public static User CreateUser(string name,string lastName,DateTime birthDate)
    {
        return new User
        {
            Name = name,
            LastName = lastName,
            Birthdate = birthDate,
            Addresses = new List<Address>()
        };
    }

    [BsonId]
    public int Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    [BsonDateTimeOptions]
    public DateTime Birthdate { get; set; } = default;
    public ICollection<Address> Addresses { get; set; }
}
