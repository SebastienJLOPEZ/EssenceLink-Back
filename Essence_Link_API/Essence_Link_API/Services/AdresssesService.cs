using Essence_Link_API.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Essence_Link_API.Services;
public class AdressesService
{
    private readonly IMongoCollection<Adresses> _adressesCollection;

    public AdressesService(
        IOptions<ELDatabaseSettings> elDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            elDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            elDatabaseSettings.Value.DatabaseName);

        _adressesCollection = mongoDatabase.GetCollection<Adresses>(
            elDatabaseSettings.Value.AdressesCollectionName);
    }

    public async Task<List<Adresses>> GetAsync() =>
        await _adressesCollection.Find(_ => true).ToListAsync();

    public async Task<Adresses?> GetAsync(string id) =>
        await _adressesCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task<List<Adresses>> GetAsyncUList(string Uid) =>
        await _adressesCollection.Find(x => x.UserId == Uid).ToListAsync();

    public async Task CreateAsync(Adresses newadresses) =>
        await _adressesCollection.InsertOneAsync(newadresses);

    public async Task UpdateAsync(string id, Adresses updatedadresses) =>
        await _adressesCollection.ReplaceOneAsync(x => x.Id == id, updatedadresses);

    public async Task RemoveAsync(string id) =>
        await _adressesCollection.DeleteOneAsync(x => x.Id == id);
}
