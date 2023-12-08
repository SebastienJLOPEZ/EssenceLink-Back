using Essence_Link_API.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Essence_Link_API.Services;
public class WishlistService
{
    private readonly IMongoCollection<Wishlist> _WishlistCollection;

    public WishlistService(
        IOptions<ELDatabaseSettings> elDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            elDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            elDatabaseSettings.Value.DatabaseName);

        _WishlistCollection = mongoDatabase.GetCollection<Wishlist>(
            elDatabaseSettings.Value.WishlistCollectionName);
    }

    // Seeing all wishlisted product not wanted for now at least
    public async Task<List<Wishlist>> GetAsync() =>
        await _WishlistCollection.Find(_ => true).ToListAsync();

    public async Task<List<Wishlist>> GetAsync(string id) =>
        await _WishlistCollection.Find(x => x.UserId == id).ToListAsync();

    //TODO : Code to find lastest element of list

    public async Task CreateAsync(Wishlist newWishlist) =>
        await _WishlistCollection.InsertOneAsync(newWishlist);

    public async Task RemoveAsync(string id) =>
        await _WishlistCollection.DeleteOneAsync(x => x.Id == id);
}
