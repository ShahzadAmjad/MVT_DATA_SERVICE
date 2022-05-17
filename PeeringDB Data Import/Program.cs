using MongoDB.Bson;
using MongoDB.Driver;
using MongoDBConnectivity;
using Newtonsoft.Json;
using PeeringDB_Data_Import.Models;
using System.Net;

Console.WriteLine("MVT Data Service started");


//First Change
string requestUri = "https://www.peeringdb.com/api/ixfac";
string responseJson = "";

HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(requestUri);
//httpWebRequest.Headers.Add("Accept", "application/json");
httpWebRequest.Method = WebRequestMethods.Http.Get;
httpWebRequest.Accept = "application/json";


Console.WriteLine("Getting All data using Web request to get id");
var response = (HttpWebResponse)httpWebRequest.GetResponse();
using (var sr = new StreamReader(response.GetResponseStream()))
{
    responseJson = sr.ReadToEnd();
}

Console.WriteLine("Deserializing Json Data");
Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(responseJson);


//Getting the List of ids
List<int> idList = new List<int>();
foreach (var pdb in myDeserializedClass.data)
{
    idList.Add(pdb.id);
}

//to empty the memory
myDeserializedClass = new Root();


//Getting data one by one adding to list

//2nd Change 
List<pdb_InternetExchangeFacility> pdbData_List = new List<pdb_InternetExchangeFacility>();


foreach (int id in idList)
{
    //3rd change
    string NewrequestUri = "https://www.peeringdb.com/api/ixfac/" + id;
    string newresponseJson = "";
    HttpWebRequest NewhttpWebRequest = (HttpWebRequest)WebRequest.Create(NewrequestUri);
    NewhttpWebRequest.Method = WebRequestMethods.Http.Get;
    NewhttpWebRequest.Accept = "application/json";

    //var rnd = new Random(DateTime.Now.Millisecond);
    //int ticks = rnd.Next(3000, 7000);

    Console.WriteLine("Getting Web request Data for id: " + id);
    //Thread.Sleep(ticks);
    var Newresponse = (HttpWebResponse)NewhttpWebRequest.GetResponse();

    using (var sr = new StreamReader(Newresponse.GetResponseStream()))
    {
        newresponseJson = sr.ReadToEnd();
    }

    Root newMyDeserializedClass = JsonConvert.DeserializeObject<Root>(newresponseJson);
    pdbData_List.Add(newMyDeserializedClass.data[0]);
}


//4th change
//Mongodb Connection
string collectionName = "pdb_internet_exchange_facilities";
string ConnectionStringCompass = "mongodb://mvtdev:-B7Q7acF9%3FK%40KptN@dev.geomentary.com:27017/?authMechanism=SCRAM-SHA-256&authSource=mvt";
bool status = false;

try
{
    Console.WriteLine("Inserting to Mongodb");
    var client = new MongoClient(ConnectionStringCompass);
    IMongoDatabase database = client.GetDatabase("mvt");
    database.DropCollection(collectionName);

    //5th change
    var collection = database.GetCollection<pdb_InternetExchangeFacility>(collectionName);
    collection.InsertMany((IEnumerable<pdb_InternetExchangeFacility>)pdbData_List);
    status = true;
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}


if (status)
{
    Console.WriteLine("Task Completed Successfully");
}
else
{
    Console.WriteLine("Task Completed with some exceptions");
}





