using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using PetsMongo.Models;
using System.Web.Mvc;

namespace PetsMongo.Services
{
    public class MongoDBService
    {
        private readonly IMongoCollection<Pet> _petCollection;

        public MongoDBService(IOptions<MongoDBSettings> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
           
            _petCollection = database.GetCollection<Pet>(mongoDBSettings.Value.CollectionName);
        }

        public async Task<List<Pet>> GetAsync(int page = 0, int limit = 10)
        {
            return await _petCollection.Find(new BsonDocument())
                .Skip(page * limit)
                .Limit(limit)
                .ToListAsync();
        }

        public async Task CreateAsync(Pet pet) 
        {
            await _petCollection.InsertOneAsync(pet);
            return;
        }

        public async Task<Pet> DeleteAsync(string id) 
        {
            FilterDefinition<Pet> filter = Builders<Pet>.Filter.Eq("Id", id);

            var pet = _petCollection.Find(filter).FirstOrDefault();

            var deleteResult = _petCollection.DeleteOneAsync(filter);
            pet.DeletedAt = DateTime.Now.ToString();

            return pet;
        }

        public async Task<int> SumOfAgesAsync()
        {
            var all = await _petCollection.Find(new BsonDocument()).ToListAsync();

            var sum = all.Sum(item => item.Age);

            return sum;
        }
    }
}
