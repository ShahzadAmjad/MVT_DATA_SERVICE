using MongoDB.Bson;
using MongoDB.Driver;
using MongoDBConnectivity;
using Newtonsoft.Json;
using PeeringDB_Data_Import.Models;
using System.Net;

Console.WriteLine("MVT Data Service started");

string requestUri = "https://www.peeringdb.com/api/ix/1";
string responseJson = ""; 

HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(requestUri);
httpWebRequest.Method = WebRequestMethods.Http.Get;
httpWebRequest.Accept = "application/json";


Console.WriteLine("Getting Response For web service");
var response = (HttpWebResponse)httpWebRequest.GetResponse();
using (var sr = new StreamReader(response.GetResponseStream()))
{
    responseJson = sr.ReadToEnd();
}


Console.WriteLine("Deserialize Json Data");
Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(responseJson);
List<pdb_InternetExchange> pdbInternetExchange_List = new List<pdb_InternetExchange>();
pdbInternetExchange_List = myDeserializedClass.data;

//Mongo db Configuration

//MongoDB: dev.geomentary.com:27017
//Auth Mechanism = SCRAM - SHA - 256
//Auth Database=mvt
//User: mvtdev
//Pwd: -B7Q7acF9 ? K@KptN


//Mongodb Connection
string collectionName = "pdb_internet_exchanges";
string ConnectionStringCompass = "mongodb://mvtdev:-B7Q7acF9%3FK%40KptN@dev.geomentary.com:27017/?authMechanism=SCRAM-SHA-256&authSource=mvt";
bool status = false;

try
{ 
    var client = new MongoClient(ConnectionStringCompass);
    IMongoDatabase database = client.GetDatabase("mvt");

    Console.WriteLine("Mongo Isertion started");
    bool collectionStatus = true;//CollectionExists(database, collectionName);
    if (collectionStatus)
    {
        //database.CreateCollection(collectionName);

        var collection = database.GetCollection<pdb_InternetExchange>(collectionName);
        collection.InsertMany((IEnumerable<pdb_InternetExchange>)pdbInternetExchange_List);
        status = true;
    }
    else
    {
        database.CreateCollection(collectionName);

        var collection = database.GetCollection<pdb_InternetExchange>(collectionName);
        collection.InsertMany((IEnumerable<pdb_InternetExchange>)pdbInternetExchange_List);
        status = true;
    }

}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}


Console.WriteLine("Task completed successfully");

//to check if a collection is exists

bool CollectionExists(IMongoDatabase database, string collectionName)
{
    var filter = new BsonDocument("name", collectionName);
    var options = new ListCollectionNamesOptions { Filter = filter };

    return database.ListCollectionNames(options).Any();
}





