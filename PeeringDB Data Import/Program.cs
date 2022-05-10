using MongoDB.Bson;
using MongoDB.Driver;
using MongoDBConnectivity;
using Newtonsoft.Json;
using PeeringDB_Data_Import.Models;
using System.Net;

Console.WriteLine("MVT Data Service started");

string requestUri = "https://www.peeringdb.com/api/fac";
string responseJson = ""; 

HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(requestUri);
httpWebRequest.Method = WebRequestMethods.Http.Get;
httpWebRequest.Accept = "application/json";



var response = (HttpWebResponse)httpWebRequest.GetResponse();

using (var sr = new StreamReader(response.GetResponseStream()))
{
    responseJson = sr.ReadToEnd();
}

Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(responseJson);

List<pdb_datacenters> pdbDatacenters_List = new List<pdb_datacenters>();
pdbDatacenters_List = myDeserializedClass.data;

//Mongo db Configuration

//MongoDB: dev.geomentary.com:27017
//Auth Mechanism = SCRAM - SHA - 256
//Auth Database=mvt
//User: mvtdev
//Pwd: -B7Q7acF9 ? K@KptN




//Mongodb Connection
//string password = "-B7Q7acF9 ? K@KptN";
string collectionName = "pdb_datacenters";
//string ConnectionString = "mongodb://mvtdev:" + password +"@dev.geomentary.com:27017";
string ConnectionStringCompass = "mongodb://mvtdev:-B7Q7acF9%3FK%40KptN@dev.geomentary.com:27017/?authMechanism=SCRAM-SHA-256&authSource=mvt";
///?authMechanism=SCRAM-SHA-256&authSource=mvt
bool status = false;

try
{

    //var credential = MongoCredential.CreateMongoCRCredential("mvt", "mvtdev", "-B7Q7acF9 ? K@KptN");
    //var settings = new MongoClientSettings
    //{
    //    Credentials = new[] { credential },
    //    Server = new MongoServerAddress("dev.geomentary.com", 27017)
        
    //};
    ////var client = new MongoClient(settings);

    
    var client = new MongoClient(ConnectionStringCompass);

    List<String> databaseNames = client.ListDatabaseNames().ToList<string>();
    IMongoDatabase database = client.GetDatabase("mvt");

    bool collectionStatus = true;//CollectionExists(database, collectionName);
    if (collectionStatus)
    {
        //database.CreateCollection(collectionName);

        var collection = database.GetCollection<pdb_datacenters>(collectionName);
        collection.InsertMany((IEnumerable<pdb_datacenters>)pdbDatacenters_List);
        status = true;
    }
    else
    {
        database.CreateCollection(collectionName);

        var collection = database.GetCollection<pdb_datacenters>(collectionName);
        collection.InsertMany((IEnumerable<pdb_datacenters>)pdbDatacenters_List);
        status = true;
    }

}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}



//CInsertMongo cInsertMongo = new CInsertMongo();
//bool status = cInsertMongo.InsertBatch(pdbDatacenters_List, "pdb_Db", "pdb_datacenters", "mongodb://dev.geomentary.com:27017");



Console.WriteLine(responseJson);





bool CollectionExists(IMongoDatabase database, string collectionName)
{
    var filter = new BsonDocument("name", collectionName);
    var options = new ListCollectionNamesOptions { Filter = filter };

    return database.ListCollectionNames(options).Any();
}





