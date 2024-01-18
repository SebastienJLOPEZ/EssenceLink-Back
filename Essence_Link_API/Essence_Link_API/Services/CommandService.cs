using Essence_Link_API.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Essence_Link_API.Services;
public class CommandService
{
    private readonly IMongoCollection<Command> _CommandCollection;

    public CommandService(
        IOptions<ELDatabaseSettings> elDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            elDatabaseSettings.Value.ConnectionString );

        var mongoDatabase = mongoClient.GetDatabase(
            elDatabaseSettings.Value.DatabaseName );

        _CommandCollection = mongoDatabase.GetCollection<Command>(
            elDatabaseSettings.Value.CommandCollectionName );
    }

    public async Task<List<Command>> GetAsync() =>
        await _CommandCollection.Find(_ => true).ToListAsync();

    public async Task<List<Command>> GetAsyncBU(string id) =>
        await _CommandCollection.Find(x => x.UserId == id).ToListAsync();

    public async Task<Command?> GetAsync(string id) =>
        await _CommandCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Command newCommand) =>
        await _CommandCollection.InsertOneAsync(newCommand);

    public async Task UpdateAsync(string id, Command updatedCommand) =>
        await _CommandCollection.ReplaceOneAsync(x => x.Id == id, updatedCommand);

    public async Task RemoveAsync(string id) =>
        await _CommandCollection.DeleteOneAsync(x => x.Id == id);
}
