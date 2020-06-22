using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace MongoDB.DataAccess.Model
{
    public class Customer
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement]
        [Required]
        public int CustomerID { get; set; }
        [BsonElement]
        [Required]
        public string Name { get; set; }
        [BsonElement]
        [Required]
        [Range(15, 100, ErrorMessage = "Age must be between 15 and 100 years")]
        public int Age { get; set; }
        [BsonElement]
        [Required]
        [Range(1000, 10000, ErrorMessage = "Salery must be between 1000 and 10000")]
        public int Salary { get; set; }
    }
}
