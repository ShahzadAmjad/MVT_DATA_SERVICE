using MongoDB.Bson;
using MongoDB.Driver;
using MongoDBConnectivity;
using Newtonsoft.Json;
using PeeringDB_Data_Import.Models;
using System.Net;

Console.WriteLine("MVT Data Service started");


//First Change
<<<<<<< HEAD
string requestUri = "https://www.peeringdb.com/api/ixfac";
=======
string requestUri = "https://www.peeringdb.com/api/poc";
>>>>>>> 5bc26f4bf18c6c9eeafceb11bbbc643ced29cc98
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
<<<<<<< HEAD

//2nd Change 
List<pdb_InternetExchangeFacility> pdbData_List = new List<pdb_InternetExchangeFacility>();


foreach (int id in idList)
{
    //3rd change
    string NewrequestUri = "https://www.peeringdb.com/api/ixfac/" + id;
=======
List<Cpdb_tranformation> cpdbTransformObj_List = new List<Cpdb_tranformation>();

foreach (int id in idList)
{
    //2nd change
    string NewrequestUri = "https://www.peeringdb.com/api/poc/" + id;
>>>>>>> 5bc26f4bf18c6c9eeafceb11bbbc643ced29cc98
    string newresponseJson = "";
    HttpWebRequest NewhttpWebRequest = (HttpWebRequest)WebRequest.Create(NewrequestUri);
    NewhttpWebRequest.Method = WebRequestMethods.Http.Get;
    NewhttpWebRequest.Accept = "application/json";

<<<<<<< HEAD
    //var rnd = new Random(DateTime.Now.Millisecond);
    //int ticks = rnd.Next(3000, 7000);

    Console.WriteLine("Getting Web request Data for id: " + id);
    //Thread.Sleep(ticks);
=======
    Console.WriteLine("Getting Web request Data for id: " + id);
>>>>>>> 5bc26f4bf18c6c9eeafceb11bbbc643ced29cc98
    var Newresponse = (HttpWebResponse)NewhttpWebRequest.GetResponse();

    using (var sr = new StreamReader(Newresponse.GetResponseStream()))
    {
        newresponseJson = sr.ReadToEnd();
    }

    Root newMyDeserializedClass = JsonConvert.DeserializeObject<Root>(newresponseJson);
<<<<<<< HEAD
    pdbData_List.Add(newMyDeserializedClass.data[0]);
}


//4th change
//Mongodb Connection
string collectionName = "pdb_internet_exchange_facilities";
=======

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
>>>>>>> 5bc26f4bf18c6c9eeafceb11bbbc643ced29cc98
string ConnectionStringCompass = "mongodb://mvtdev:-B7Q7acF9%3FK%40KptN@dev.geomentary.com:27017/?authMechanism=SCRAM-SHA-256&authSource=mvt";
bool status = false;

try
{
    Console.WriteLine("Inserting to Mongodb");
    var client = new MongoClient(ConnectionStringCompass);
    IMongoDatabase database = client.GetDatabase("mvt");
    database.DropCollection(collectionName);
<<<<<<< HEAD

    //5th change
    var collection = database.GetCollection<pdb_InternetExchangeFacility>(collectionName);
    collection.InsertMany((IEnumerable<pdb_InternetExchangeFacility>)pdbData_List);
=======
    var collection = database.GetCollection<Cpdb_tranformation>(collectionName);
    collection.InsertMany((IEnumerable<Cpdb_tranformation>)cpdbTransformObj_List);
>>>>>>> 5bc26f4bf18c6c9eeafceb11bbbc643ced29cc98
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





