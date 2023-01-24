using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace PetsMongo.Models
{
    public class Pet
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string Name { get; set; }
        public string CreatedAt { get; set; } = DateTime.Now.ToString();
        public string? DeletedAt { get; set; } = null;
        public string Type { get; set; }
        public int Age { get; set; }
    }
}
