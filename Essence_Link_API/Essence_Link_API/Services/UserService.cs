using Essence_Link_API.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Essence_Link_API.Services;
public class UserService
{
    private readonly IMongoCollection<User> _UserCollection;

    public UserService(
        IOptions<ELDatabaseSettings> elDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            elDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            elDatabaseSettings.Value.DatabaseName);

        _UserCollection = mongoDatabase.GetCollection<User>(
            elDatabaseSettings.Value.UserCollectionName);
    }

    public async Task<List<User>> GetAsync() =>
        await _UserCollection.Find(_ => true).ToListAsync();

    public async Task<User?> GetAsync(string id) =>
        await _UserCollection.Find(x => x.Email == id).FirstOrDefaultAsync();
    public async Task<List<User>> GetAsyncSearch(string name) =>
        await _UserCollection.Find(x => x.FirstName.ToLower().Contains(name.ToLower()) 
                        || x.LastName.ToLower().Contains(name.ToLower())).ToListAsync();

    public async Task CreateAsync(User newUser) =>
        await _UserCollection.InsertOneAsync(newUser);

    public async Task UpdateAsync(string id, User updatedUser) =>
        await _UserCollection.ReplaceOneAsync(x => x.Email == id, updatedUser);

    public async Task RemoveAsync(string id) =>
        await _UserCollection.DeleteOneAsync(x => x.Email == id);
}
