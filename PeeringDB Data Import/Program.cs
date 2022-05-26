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
    //string collectionName = "pdb_organizations";
    //string idListFilePath = @"G:\LC\Service\idList\" + collectionName + "_idList.txt";
    WebRequestResponse response = new WebRequestResponse();
    CMongodb mongodb = new CMongodb();

    //This block only get the id's list for each collection
    //saving file to root directory
    string idListFilePath = AppDomain.CurrentDomain.BaseDirectory + @"\" + collectionName + "_idList.txt";  
    List<int> idList = new List<int>();
    try
    {
        //load the list of ids for collection if already exist
        if (File.Exists(idListFilePath))
        {
            idList = File.ReadAllLines(idListFilePath).Select(x => Convert.ToInt32(x)).ToList();
        }
        else
        {
            //get data to make an id list for one by one request
            string requestUri = CollectionvsUri.First(kvp => kvp.Key == collectionName).Value;          
            string responseJson =  response.getWebRequestData(requestUri);

            Console.WriteLine("Deserializing Json Data id's List For: "+collectionName);
            
            if (collectionName== "pdb_Organization")
            {
                Root_pdb_Organization myDeserializedClass = JsonConvert.DeserializeObject<Root_pdb_Organization>(responseJson);
                //Getting the List of ids
                foreach (var pdb in myDeserializedClass.data)
                {
                    idList.Add(pdb.id);
                }

                //to empty memory
                myDeserializedClass = new Root_pdb_Organization();
            }
            else if(collectionName== "")
            {

            }

            
            //Write to File//saving to avoid block 
            File.WriteAllLines(idListFilePath, idList.Select(x => x.ToString()));
        }






        //Getting data one by one and  adding to list
        if (collectionName == "pdb_Organization")
        {
            List<pdb_Organization> pdbData_List = new List<pdb_Organization>();
            foreach (int id in idList)
            {
                string NewrequestUri = CollectionvsUri.First(kvp => kvp.Key == collectionName).Value + "/" + id;
                string newresponseJson = response.getWebRequestData(NewrequestUri);

                Root_pdb_Organization newMyDeserializedClass = JsonConvert.DeserializeObject<Root_pdb_Organization>(newresponseJson);
                pdbData_List.Add(newMyDeserializedClass.data[0]);

                //500 bach insertion
                if (pdbData_List.Count >= 500)
                {

                    mongodb.InsertBatch(pdbData_List, collectionName,insertedBatchCount);
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
                    File.WriteAllLines(AppDomain.CurrentDomain.BaseDirectory + @"\" + collectionName + "_batchCountAndId.txt", batchcountAndId.Select(x => x.ToString()));

                }
            }
            mongodb.InsertBatch(pdbData_List, collectionName, insertedBatchCount);
        }


        


        //for last bactch insertion
        


        Console.WriteLine("Task Completed Successfully");


    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}






