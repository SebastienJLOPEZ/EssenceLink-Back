using Essence_Link_API.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Essence_Link_API.Services;
public class ReviewService
{
    private readonly IMongoCollection<Review> _ReviewCollection;

    public ReviewService(
        IOptions<ELDatabaseSettings> elDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            elDatabaseSettings.Value.ConnectionString );

        var mongoDatabase = mongoClient.GetDatabase(
            elDatabaseSettings.Value.DatabaseName );

        _ReviewCollection = mongoDatabase.GetCollection<Review>(
            elDatabaseSettings.Value.ReviewCollectionName );
    }

    public async Task<List<Review>> GetAsync() =>
        await _ReviewCollection.Find(_ => true).ToListAsync();

    public async Task<Review?> GetAsync(string id) =>
        await _ReviewCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task<List<Review>> GetAsyncP(string pid) =>
        await _ReviewCollection.Find(x => x.ProductId == pid).ToListAsync();

    public async Task<List<Review>> GetAsyncS (decimal stars) =>
        await _ReviewCollection.Find(x => x.Score == stars).ToListAsync();

    public async Task CreateAsync(Review newReview) =>
        await _ReviewCollection.InsertOneAsync(newReview);

    public async Task UpdateAsync(string id, Review updatedReview) =>
        await _ReviewCollection.ReplaceOneAsync(x => x.Id == id, updatedReview);

    public async Task RemoveAsync(string id) =>
        await _ReviewCollection.DeleteOneAsync(x => x.Id == id);
}
