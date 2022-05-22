using MongoDB.Bson;
using MongoDB.Driver;
using MongoDBConnectivity;
using Newtonsoft.Json;
using PeeringDB_Data_Import.Models;
using System.Net;

Console.WriteLine("MVT Data Service started");
int insertedBatchCount = 33;
//First Change
string collectionName = "pdb_organizations";
string idListFilePath = @"G:\LC\Service\idList\" + collectionName+ "_idList.txt";
List<int> idList = new List<int>();

try
{  

    if(File.Exists(idListFilePath))
    {
        idList = File.ReadAllLines(idListFilePath).Select(x => Convert.ToInt32(x)).ToList();
    }
    else
    {
        //2nd Change
        string requestUri = "https://www.peeringdb.com/api/org";
        string responseJson = "";

        HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(requestUri);
        httpWebRequest.Method = WebRequestMethods.Http.Get;
        httpWebRequest.Accept = "application/json";

        Console.WriteLine("Getting All data using Web request to get id's List");
        var response = (HttpWebResponse)httpWebRequest.GetResponse();
        using (var sr = new StreamReader(response.GetResponseStream()))
        {
            responseJson = sr.ReadToEnd();
        }
        Console.WriteLine("Deserializing Json Data id's List");
        Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(responseJson);
        
        //Getting the List of ids
        foreach (var pdb in myDeserializedClass.data)
        {
            idList.Add(pdb.id);
        }
        
        //to empty memory
        myDeserializedClass = new Root();
        
        //Write to File//saving to avoid block 
        File.WriteAllLines(idListFilePath, idList.Select(x => x.ToString()));
    }





//3rd change change
//Getting data one by one and  adding to list
List<pdb_Organization> pdbData_List = new List<pdb_Organization>();

foreach (int id in idList)
{
        //4th change
    string NewrequestUri = "https://www.peeringdb.com/api/org/" + id;
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
    pdbData_List.Add(newMyDeserializedClass.data[0]);
        
        //500 bach insertion
        if(pdbData_List.Count >= 500)
        {

            InsertBatch(pdbData_List, collectionName);
            Console.WriteLine("500 records Batch Inserted till  id: " + id);
            //5th change
            pdbData_List = new List<pdb_Organization>();
            insertedBatchCount++;
            Console.WriteLine("Batch count: " + insertedBatchCount);
            //to save in a file
            List<int> batchcountAndId = new List<int>();
            batchcountAndId.Add(insertedBatchCount);
            batchcountAndId.Add(id);
            //Write to File the last id inserted and total batches
            File.WriteAllLines(@"G:\LC\Service\idList\batchCount\" + collectionName+"_batchCountAndId.txt", batchcountAndId.Select(x => x.ToString()));

        }
}




    //for last bactch insertion
    InsertBatch(pdbData_List, collectionName);


//try
//{
//    Console.WriteLine("Inserting to Mongodb");
//    var client = new MongoClient(ConnectionStringCompass);
//    IMongoDatabase database = client.GetDatabase("mvt");
//    database.DropCollection(collectionName);
//        //5th change
//    var collection = database.GetCollection<pdb_NetworkToIXConnection>(collectionName);
//    collection.InsertMany((IEnumerable<pdb_NetworkToIXConnection>)pdbData_List);
    
//        status = true;
//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}


    Console.WriteLine("Task Completed Successfully");


}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}




bool InsertBatch<T>(List<T> list, string collectionName)
{
    bool status = false;
    try
    {
        var client = OpenDBConn();
        IMongoDatabase database = client.GetDatabase("mvt");

        if (list is List<pdb_NetworkToIXConnection>)
        {
            //6th change
            List<pdb_Organization> batch = list as List<pdb_Organization>;
            //to empty previous records if any
            if(insertedBatchCount==0)
            {
                database.DropCollection(collectionName);
            }
            
            //7th change
            var collection = database.GetCollection<pdb_Organization>(collectionName);
            collection.InsertMany((IEnumerable<pdb_Organization>)batch);


        }
        else
        {
            status = false;
        }
    }
    catch (Exception ex)
    {
        status = false;
    }
    return status;
}

MongoClient OpenDBConn()
{
    Console.WriteLine("opening connection to Mongodb");
    string ConnectionStringCompass = "mongodb://mvtdev:-B7Q7acF9%3FK%40KptN@dev.geomentary.com:27017/?authMechanism=SCRAM-SHA-256&authSource=mvt";
    var client = new MongoClient(ConnectionStringCompass);
    return client;
}