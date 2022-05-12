using MongoDB.Bson;
using MongoDB.Driver;
using MongoDBConnectivity;
using Newtonsoft.Json;
using PeeringDB_Data_Import.Models;
using System.Net;

Console.WriteLine("MVT Data Service started");

string requestUri = "https://www.peeringdb.com/api/as_set";
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

List<pdb_AS_SET> pdb_List = new List<pdb_AS_SET>();



Console.WriteLine("Deserialize Json Data");


//Only for as set data deserialization
string data = (responseJson.Split("[{"))[1];
string[] dataList = data.Split(",");

foreach(string item in dataList)
{
    string[] itemList = item.Split(":");
    if(itemList.Length>=2)
    {
        string[] newitemList = new string[2]; 
            Array.Copy(itemList, 0, newitemList, 0, 2);
        pdb_AS_SET pdb_AS_SET = new pdb_AS_SET();


        int n;
        bool isNumeric = int.TryParse((newitemList[0].Replace('"', ' ').Trim()), out n);

        if (isNumeric)
        {
            pdb_AS_SET.id = Convert.ToInt32(newitemList[0].Replace('"', ' ').Trim());
            pdb_AS_SET.value = newitemList[1].Replace('"', ' ').Trim();
            pdb_List.Add(pdb_AS_SET);
        }
        
    }
    
}




//Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(responseJson);
//List<pdb_AS_SET> pdb_List = new List<pdb_AS_SET>();
//pdb_List = myDeserializedClass.data;

//Mongo db Configuration

//MongoDB: dev.geomentary.com:27017
//Auth Mechanism = SCRAM - SHA - 256
//Auth Database=mvt
//User: mvtdev
//Pwd: -B7Q7acF9 ? K@KptN


//Mongodb Connection
string collectionName = " pdb_as_set";
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

        var collection = database.GetCollection<pdb_AS_SET>(collectionName);
        collection.InsertMany((IEnumerable<pdb_AS_SET>)pdb_List);
        status = true;
    }
    else
    {
        database.CreateCollection(collectionName);

        var collection = database.GetCollection<pdb_AS_SET>(collectionName);
        collection.InsertMany((IEnumerable<pdb_AS_SET>)pdb_List);
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





