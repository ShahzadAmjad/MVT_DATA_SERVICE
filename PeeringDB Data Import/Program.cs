using MongoDB.Bson;
using MongoDB.Driver;
using MongoDBConnectivity;
using Newtonsoft.Json;
using PeeringDB_Data_Import.Models;
using System.Net;

Console.WriteLine("MVT Data Service started");


//First Change
string requestUri = "https://www.peeringdb.com/api/ix";
string responseJson = "";

try
{
    HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(requestUri);
    httpWebRequest.Method = WebRequestMethods.Http.Get;
    httpWebRequest.Accept = "application/json";

    Console.WriteLine("Getting All data using Web request to get id's List");
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


//Getting data one by one and  adding to list
List<pdb_InternetExchange> pdbData_List = new List<pdb_InternetExchange>();

foreach (int id in idList)
{
    string NewrequestUri = "https://www.peeringdb.com/api/ix/" + id;
    string newresponseJson = "";

    HttpWebRequest NewhttpWebRequest = (HttpWebRequest)WebRequest.Create(NewrequestUri);
    NewhttpWebRequest.Method = WebRequestMethods.Http.Get;
    NewhttpWebRequest.Accept = "application/json";

        //For thread sleep
        //var rnd = new Random(DateTime.Now.Millisecond);
        //int ticks = rnd.Next(3000, 7000);
        //Thread.Sleep(ticks);

    Console.WriteLine("Getting Web request Data for id: " + id);
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
string collectionName = "pdb_internet_exchanges";
string ConnectionStringCompass = "mongodb://mvtdev:-B7Q7acF9%3FK%40KptN@dev.geomentary.com:27017/?authMechanism=SCRAM-SHA-256&authSource=mvt";
bool status = false;

try
{
    Console.WriteLine("Inserting to Mongodb");
    var client = new MongoClient(ConnectionStringCompass);
    IMongoDatabase database = client.GetDatabase("mvt");
    database.DropCollection(collectionName);

    //5th change
    var collection = database.GetCollection<pdb_InternetExchange>(collectionName);
    collection.InsertMany((IEnumerable<pdb_InternetExchange>)pdbData_List);
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


}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}




