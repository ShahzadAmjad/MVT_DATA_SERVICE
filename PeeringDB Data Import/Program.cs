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


Console.WriteLine("Getting All data using Web request to get id");
var response = (HttpWebResponse)httpWebRequest.GetResponse();
using (var sr = new StreamReader(response.GetResponseStream()))
{
    responseJson = sr.ReadToEnd();
}

Console.WriteLine("Deserializing Json Data");
Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(responseJson);


//Getting the List of ids
List<int> idList= new List<int>();
foreach (var pdb in myDeserializedClass.data)
{
    idList.Add(pdb.id);
}

//to empty the memory
myDeserializedClass=new Root();


//Getting data one by one
//List<pdb_datacenters> pdbDatacenters_List = new List<pdb_datacenters>();
List<Newpdb_datacenters> new_pdbDatacenters_List = new List<Newpdb_datacenters>();

foreach (int id in idList)
{
    string NewrequestUri = "https://www.peeringdb.com/api/fac/"+id;
    string newresponseJson = "";
    HttpWebRequest NewhttpWebRequest = (HttpWebRequest)WebRequest.Create(NewrequestUri);
    NewhttpWebRequest.Method = WebRequestMethods.Http.Get;
    NewhttpWebRequest.Accept = "application/json";

    Console.WriteLine("Getting Web request Data for id: "+id);
    var Newresponse = (HttpWebResponse)NewhttpWebRequest.GetResponse();

    using (var sr = new StreamReader(Newresponse.GetResponseStream()))
    {
        newresponseJson = sr.ReadToEnd();
    }

    //Console.WriteLine("Deserializing Json Data: "+id);
    Root newMyDeserializedClass = JsonConvert.DeserializeObject<Root>(newresponseJson);
    pdb_datacenters pdbDatacenterObj = new pdb_datacenters();
    pdbDatacenterObj = newMyDeserializedClass.data[0];
    //pdbDatacenters_List.Add(pdbDatacenterObj);


    //tranforming object to new modal class
    Newpdb_datacenters new_pdbDatacenterObj = new Newpdb_datacenters();
    new_pdbDatacenterObj._id = (id).ToString();
    new_pdbDatacenterObj.type = "Feature";
    new_pdbDatacenterObj.geometry = new Geometry();
    new_pdbDatacenterObj.geometry.type = "Point";
    new_pdbDatacenterObj.geometry.coordinates = new List<double?>();
    new_pdbDatacenterObj.geometry.coordinates.Add(pdbDatacenterObj.longitude);
    new_pdbDatacenterObj.geometry.coordinates.Add(pdbDatacenterObj.latitude);
    new_pdbDatacenterObj.properties = new pdb_datacenters();
    new_pdbDatacenterObj.properties= pdbDatacenterObj;

    new_pdbDatacenters_List.Add(new_pdbDatacenterObj);
}



//Mongo db Configuration

//MongoDB: dev.geomentary.com:27017
//Auth Mechanism = SCRAM - SHA - 256
//Auth Database=mvt
//User: mvtdev
//Pwd: -B7Q7acF9 ? K@KptN


//Mongodb Connection
string collectionName = "pdb_datacenters";
string ConnectionStringCompass = "mongodb://mvtdev:-B7Q7acF9%3FK%40KptN@dev.geomentary.com:27017/?authMechanism=SCRAM-SHA-256&authSource=mvt";
bool status = false;

try
{
    Console.WriteLine("Inserting to Mongodb");
    var client = new MongoClient(ConnectionStringCompass);
    IMongoDatabase database = client.GetDatabase("mvt");

    database.DropCollection(collectionName);

    var collection = database.GetCollection<Newpdb_datacenters>(collectionName);

    collection.InsertMany((IEnumerable<Newpdb_datacenters>)new_pdbDatacenters_List);
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





