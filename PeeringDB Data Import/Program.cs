using MongoDB.Bson;
using MongoDB.Driver;
using MongoDBConnectivity;
using Newtonsoft.Json;
using PeeringDB_Data_Import;
using PeeringDB_Data_Import.Models;
using System.Net;

Console.WriteLine("MVT Data Service started");
int insertedBatchCount = 0;
List<string> collectionList = new List<string> 
{"pdb_datacenters","pdb_internet_exchanges","pdb_internet_exchange_facilities",
    "pdb_internet_exchange_networks"," pdb_internet_exchange_prefixes","pdb_networks",
    "pdb_network_pocs","pdb_network_facilities","pdb_network_to_ix_connection","pdb_organizations","pdb_as_set"};

var CollectionvsUri= new List<KeyValuePair<string, string>>();
CollectionvsUri.Add(new KeyValuePair<string, string>("pdb_datacenters", "https://www.peeringdb.com/api/fac"));
CollectionvsUri.Add(new KeyValuePair<string, string>("pdb_internet_exchanges", "https://www.peeringdb.com/api/ix"));
CollectionvsUri.Add(new KeyValuePair<string, string>("pdb_internet_exchange_facilities", "https://www.peeringdb.com/api/ixfac"));
CollectionvsUri.Add(new KeyValuePair<string, string>("pdb_internet_exchange_networks", "https://www.peeringdb.com/api/ixlan"));
CollectionvsUri.Add(new KeyValuePair<string, string>("pdb_internet_exchange_prefixes", "https://www.peeringdb.com/api/ixpfx"));
CollectionvsUri.Add(new KeyValuePair<string, string>("pdb_networks", "https://www.peeringdb.com/api/net"));
CollectionvsUri.Add(new KeyValuePair<string, string>("pdb_network_pocs", "https://www.peeringdb.com/api/poc"));
CollectionvsUri.Add(new KeyValuePair<string, string>("pdb_network_facilities", "https://www.peeringdb.com/api/netfac"));
CollectionvsUri.Add(new KeyValuePair<string, string>("pdb_network_to_ix_connection", "https://www.peeringdb.com/api/netixlan"));
CollectionvsUri.Add(new KeyValuePair<string, string>(" pdb_organizations", "https://www.peeringdb.com/api/org"));
CollectionvsUri.Add(new KeyValuePair<string, string>("pdb_as_set", "https://www.peeringdb.com/api/as_set"));



    //string uridd = 


foreach (var collectionName in collectionList)
{
    
    //First Change
    //string collectionName = "pdb_organizations";
    //string idListFilePath = @"G:\LC\Service\idList\" + collectionName + "_idList.txt";
    
    //saving file to root directory
    string idListFilePath = AppDomain.CurrentDomain.BaseDirectory + @"\" + collectionName + "_idList.txt";
    
    List<int> idList = new List<int>();

    try
    {

        if (File.Exists(idListFilePath))
        {
            idList = File.ReadAllLines(idListFilePath).Select(x => Convert.ToInt32(x)).ToList();
        }
        else
        {
            //2nd Change
            string requestUri = CollectionvsUri.First(kvp => kvp.Key == collectionName).Value;
            WebRequestResponse response = new WebRequestResponse();
            string responseJson =  response.getWebRequestData(requestUri);


            if(collectionName== "pdb_Organization")
            {

            }
            else if(collectionName== "")
            {

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
            if (pdbData_List.Count >= 500)
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
                File.WriteAllLines(@"G:\LC\Service\idList\batchCount\" + collectionName + "_batchCountAndId.txt", batchcountAndId.Select(x => x.ToString()));

            }
        }




        //for last bactch insertion
        InsertBatch(pdbData_List, collectionName);


        Console.WriteLine("Task Completed Successfully");


    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
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