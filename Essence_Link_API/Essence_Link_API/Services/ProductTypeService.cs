using Essence_Link_API.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Essence_Link_API.Services;
public class ProductTypeService
{
    private readonly IMongoCollection<ProductType> _ProductTypeCollection;

    public ProductTypeService(
        IOptions<ELDatabaseSettings> elDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            elDatabaseSettings.Value.ConnectionString );

        var mongoDatabase = mongoClient.GetDatabase(
            elDatabaseSettings.Value.DatabaseName );

        _ProductTypeCollection = mongoDatabase.GetCollection<ProductType>(
            elDatabaseSettings.Value.ProductTypeCollectionName );
    }

    public async Task<List<ProductType>> GetAsync() =>
        await _ProductTypeCollection.Find(_ => true).ToListAsync();

    public async Task<ProductType?> GetAsync(string id) =>
        await _ProductTypeCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(ProductType newProductType) =>
        await _ProductTypeCollection.InsertOneAsync(newProductType);

    public async Task UpdateAsync(string id, ProductType updatedProductType) =>
        await _ProductTypeCollection.ReplaceOneAsync(x => x.Id == id, updatedProductType);

    public async Task RemoveAsync(string id) =>
        await _ProductTypeCollection.DeleteOneAsync(x => x.Id == id);
}
