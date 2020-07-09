using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BooksMongoDB.Models
{
    public class Books
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public string Author { get; set; }
    }
}