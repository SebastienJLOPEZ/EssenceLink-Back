using Essence_Link_API.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Essence_Link_API.Services;
public class ProductService
{
    private readonly IMongoCollection<Product> _ProductCollection;

    public ProductService(
        IOptions<ELDatabaseSettings> elDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            elDatabaseSettings.Value.ConnectionString );

        var mongoDatabase = mongoClient.GetDatabase(
            elDatabaseSettings.Value.DatabaseName );

        _ProductCollection = mongoDatabase.GetCollection<Product>(
            elDatabaseSettings.Value.ProductCollectionName );
    }

    public async Task<List<Product>> GetAsync() =>
        await _ProductCollection.Find(_ => true).ToListAsync();

    public async Task<Product?> GetAsync(string id) =>
        await _ProductCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task<List<Product>> GetAsyncN(string searchTerm) =>
        await _ProductCollection.Find(x => x.Name.ToLower().Contains(searchTerm.ToLower())
                        || x.Description.ToLower().Contains(searchTerm.ToLower())).ToListAsync();
    
    // Fetch By Product Type
    public async Task<List<Product>> GetAsyncHydro() =>
        await _ProductCollection.Find(x => x.Type == "Hydrolat").ToListAsync();
    public async Task<List<Product>> GetAsyncTnP() =>
        await _ProductCollection.Find(x => x.Type == "Tisane & Plante Sèche").ToListAsync();
    public async Task<List<Product>> GetAsyncGem() =>
        await _ProductCollection.Find(x => x.Type == "Gemmothérapie").ToListAsync();
    public async Task<List<Product>> GetAsyncArom() =>
        await _ProductCollection.Find(x => x.Type == "Aromates").ToListAsync();
    public async Task<List<Product>> GetAsyncDrink() =>
        await _ProductCollection.Find(x => x.Type == "Boisson").ToListAsync();
    
    // Fetch By Product Subtype
    public async Task<List<Product>> GetAsyncDrinkNA() =>
        await _ProductCollection.Find(x => x.Type == "Boisson" && x.SubType == "SansAlcool").ToListAsync();

    //TODO :
    // GetAsyncP -> Using range of price

    public async Task CreateAsync(Product newProduct) =>
        await _ProductCollection.InsertOneAsync(newProduct);

    public async Task UpdateAsync(string id, Product updatedProduct) =>
        await _ProductCollection.ReplaceOneAsync(x => x.Id == id, updatedProduct);

    public async Task RemoveAsync(string id) =>
        await _ProductCollection.DeleteOneAsync(x => x.Id == id);
}
// TODO :
// Create GetAsync specialized for each Subtype
// Make sure not be able to add twice the same product
