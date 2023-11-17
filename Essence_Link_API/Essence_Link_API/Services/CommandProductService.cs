using Essence_Link_API.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Essence_Link_API.Services;
public class CommandProductService
{
    private readonly IMongoCollection<CommandProduct> _CommandProductCollection;

    public CommandProductService(
        IOptions<ELDatabaseSettings> elDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            elDatabaseSettings.Value.ConnectionString );

        var mongoDatabase = mongoClient.GetDatabase(
            elDatabaseSettings.Value.DatabaseName );

        _CommandProductCollection = mongoDatabase.GetCollection<CommandProduct>(
            elDatabaseSettings.Value.CommandProductCollectionName );
    }

    public async Task<List<CommandProduct>> GetAsync() =>
        await _CommandProductCollection.Find(_ => true).ToListAsync();

    public async Task<CommandProduct?> GetAsync(string id) =>
        await _CommandProductCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(CommandProduct newCommandProduct) =>
        await _CommandProductCollection.InsertOneAsync(newCommandProduct);

    public async Task UpdateAsync(string id, CommandProduct updatedCommandProduct) =>
        await _CommandProductCollection.ReplaceOneAsync(x => x.Id == id, updatedCommandProduct);

    public async Task RemoveAsync(string id) =>
        await _CommandProductCollection.DeleteOneAsync(x => x.Id == id);
}
