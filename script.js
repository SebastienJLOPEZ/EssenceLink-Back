const dbName = "EssenceLinkDB";
const db = db.getSiblingDB(dbName);

const collectionNames = [ "User", "Command", "Command_Product", "Product", "Product_Picture", "Product_Type", "Review", "Wishlist", "Addresses" ]

collectionNames.forEach( collName => db.createCollection( collName ) );

function insertDataFromFile(collectionNames, fileName){
    const collection = db.getCollection(collectionNames);
    const fileContent = cat(FileName);
    const data = JSON.parse(fileContent);
    collection.InsertMany(data);
}

collectionNames.forEach(collName => {
    const fileName = collName + ".json";
    insertDataFromFile(collName, fileName);
})

//Don't forget to add new data of each collections in their respectives files.
// To do that, either extract data from MongoDBCompass or add new elements in those files.

//Original files are templates, so when adding new data, just add new files with same name without "_Template"
//It's important because if you extract data from MDBCompass, you will have the _id in a format more complex that a string