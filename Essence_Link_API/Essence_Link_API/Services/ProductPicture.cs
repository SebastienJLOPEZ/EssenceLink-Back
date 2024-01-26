using Essence_Link_API.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Essence_Link_API.Services;
public class ProductPictureService
{
    private readonly IMongoCollection<ProductPicture> _ProductPictureCollection;

    public ProductPictureService(
        IOptions<ELDatabaseSettings> elDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            elDatabaseSettings.Value.ConnectionString );

        var mongoDatabase = mongoClient.GetDatabase(
            elDatabaseSettings.Value.DatabaseName );

        _ProductPictureCollection = mongoDatabase.GetCollection<ProductPicture>(
            elDatabaseSettings.Value.ProductPictureCollectionName );
    }

    public async Task<List<ProductPicture>> GetAsync() =>
        await _ProductPictureCollection.Find(_ => true).ToListAsync();

    public async Task<ProductPicture?> GetAsync(string id) =>
        await _ProductPictureCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
    public async Task<ProductPicture?> GetAsyncByPID(string id) =>
        await _ProductPictureCollection.Find(x => x.ProductId == id).FirstOrDefaultAsync();

    public async Task CreateAsync(ProductPicture newProductPicture) =>
        await _ProductPictureCollection.InsertOneAsync(newProductPicture);

    public async Task UpdateAsync(string id, ProductPicture updatedProductPicture) =>
        await _ProductPictureCollection.ReplaceOneAsync(x => x.Id == id, updatedProductPicture);

    public async Task RemoveAsync(string id) =>
        await _ProductPictureCollection.DeleteOneAsync(x => x.Id == id);
}
