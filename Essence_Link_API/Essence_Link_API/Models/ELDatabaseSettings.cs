namespace Essence_Link_API.Models; 
public class ELDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string UserCollectionName { get; set; } = null!;
    public string ProductTypeCollectionName { get; set; } = null!;
    public string ProductCollectionName { get; set; } = null!;
    public string ProductPictureCollectionName { get; set; } = null!;
    public string ReviewCollectionName { get; set; } = null!;
    public string CommandCollectionName { get; set; } = null!;
    public string CommandProductCollectionName { get; set; } = null!;

}
