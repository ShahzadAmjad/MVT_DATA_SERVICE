using MongoDB.Bson;
using MongoDB.Driver;
using MongoDBConnectivity;
using Newtonsoft.Json;
using PeeringDB_Data_Import.Models;
using System.Net;

Console.WriteLine("MVT Data Service started");


//First Change
string requestUri = "https://www.peeringdb.com/api/poc";
string responseJson = "";

HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(requestUri);
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
List<Cpdb_tranformation> cpdbTransformObj_List = new List<Cpdb_tranformation>();

foreach (int id in idList)
{
    //2nd change
    string NewrequestUri = "https://www.peeringdb.com/api/poc/" + id;
    string newresponseJson = "";
    HttpWebRequest NewhttpWebRequest = (HttpWebRequest)WebRequest.Create(NewrequestUri);
    NewhttpWebRequest.Method = WebRequestMethods.Http.Get;
    NewhttpWebRequest.Accept = "application/json";

    Console.WriteLine("Getting Web request Data for id: " + id);
    var Newresponse = (HttpWebResponse)NewhttpWebRequest.GetResponse();

    using (var sr = new StreamReader(Newresponse.GetResponseStream()))
    {
        newresponseJson = sr.ReadToEnd();
    }

    Root newMyDeserializedClass = JsonConvert.DeserializeObject<Root>(newresponseJson);

    //pdb_datacenters pdbDatacenterObj = new pdb_datacenters();
    //pdbDatacenterObj = newMyDeserializedClass.data[0];


    //3rd change
    //tranforming object to new modal class
    Cpdb_tranformation cpdbTransformObj = new Cpdb_tranformation();
    cpdbTransformObj._id = (id).ToString();
    cpdbTransformObj.type = "Feature";
    cpdbTransformObj.geometry = new Geometry();
    cpdbTransformObj.geometry.type = "Point";
    cpdbTransformObj.geometry.coordinates = new List<double?>();
    //4th change
    //no lat long in pdb_internet_exchange_prefixes
    cpdbTransformObj.geometry.coordinates.Add(newMyDeserializedClass.data[0].net.org.longitude);
    cpdbTransformObj.geometry.coordinates.Add(newMyDeserializedClass.data[0].net.org.latitude);
    cpdbTransformObj.properties = new pdb_NetworkPOC();
    cpdbTransformObj.properties = newMyDeserializedClass.data[0];

    cpdbTransformObj_List.Add(cpdbTransformObj);
}


//5th change
//Mongodb Connection
string collectionName = "pdb_network_pocs";
string ConnectionStringCompass = "mongodb://mvtdev:-B7Q7acF9%3FK%40KptN@dev.geomentary.com:27017/?authMechanism=SCRAM-SHA-256&authSource=mvt";
bool status = false;

try
{
    Console.WriteLine("Inserting to Mongodb");
    var client = new MongoClient(ConnectionStringCompass);
    IMongoDatabase database = client.GetDatabase("mvt");

    database.DropCollection(collectionName);
    var collection = database.GetCollection<Cpdb_tranformation>(collectionName);
    collection.InsertMany((IEnumerable<Cpdb_tranformation>)cpdbTransformObj_List);
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





