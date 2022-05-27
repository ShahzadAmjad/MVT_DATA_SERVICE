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
    string idListFilePath = AppDomain.CurrentDomain.BaseDirectory + @"MetaFiles\" + collectionName + "_idList.txt";  
    List<int> idList = new List<int>();
    try
    {
        //load the list of ids for collection if already exist
        if (File.Exists(idListFilePath))
        {
            idList = File.ReadAllLines(idListFilePath).Select(x => Convert.ToInt32(x)).ToList();
        }
        //else create the idlist 
        else
        {
            //get data to make an id list for one by one request
            string requestUri = CollectionvsUri.First(kvp => kvp.Key == collectionName).Value;          
            string responseJson =  response.getWebRequestData(requestUri);
            Console.WriteLine("Deserializing Json Data id's List For: "+collectionName);

            if (collectionName == "pdb_datacenters")
            {
                Root_pdb_datacenters myDeserializedClass = JsonConvert.DeserializeObject<Root_pdb_datacenters>(responseJson);
                //Getting the List of ids
                foreach (var pdb in myDeserializedClass.data)
                {
                    idList.Add(pdb.id);
                }
                //to empty memory
                myDeserializedClass = new Root_pdb_datacenters();
            }
            else if (collectionName == "pdb_internet_exchanges")
            {
                Root_pdb_InternetExchange myDeserializedClass = JsonConvert.DeserializeObject<Root_pdb_InternetExchange>(responseJson);
                //Getting the List of ids
                foreach (var pdb in myDeserializedClass.data)
                {
                    idList.Add(pdb.id);
                }
                //to empty memory
                myDeserializedClass = new Root_pdb_InternetExchange();
            }
            else if (collectionName == "pdb_internet_exchange_facilities")
            {
                Root_pdb_InternetExchangeFacility myDeserializedClass = JsonConvert.DeserializeObject<Root_pdb_InternetExchangeFacility>(responseJson);
                //Getting the List of ids
                foreach (var pdb in myDeserializedClass.data)
                {
                    idList.Add(pdb.id);
                }
                //to empty memory
                myDeserializedClass = new Root_pdb_InternetExchangeFacility();
            }
            else if (collectionName == "pdb_internet_exchange_networks")
            {
                Root_pdb_internet_exchange_networks myDeserializedClass = JsonConvert.DeserializeObject<Root_pdb_internet_exchange_networks>(responseJson);
                //Getting the List of ids
                foreach (var pdb in myDeserializedClass.data)
                {
                    idList.Add(pdb.id);
                }
                //to empty memory
                myDeserializedClass = new Root_pdb_internet_exchange_networks();
            }
            else if (collectionName == "pdb_internet_exchange_prefixes")
            {
                Root_pdb_internet_exchange_prefixes myDeserializedClass = JsonConvert.DeserializeObject<Root_pdb_internet_exchange_prefixes>(responseJson);
                //Getting the List of ids
                foreach (var pdb in myDeserializedClass.data)
                {
                    idList.Add(pdb.id);
                }
                //to empty memory
                myDeserializedClass = new Root_pdb_internet_exchange_prefixes();
            }
            else if (collectionName == "pdb_networks")
            {
                Root_pdb_Network myDeserializedClass = JsonConvert.DeserializeObject<Root_pdb_Network>(responseJson);
                //Getting the List of ids
                foreach (var pdb in myDeserializedClass.data)
                {
                    idList.Add(pdb.id);
                }
                //to empty memory
                myDeserializedClass = new Root_pdb_Network();
            }
            else if (collectionName == "pdb_network_facilities")
            {
                Root_pdb_InternetExchangeFacility myDeserializedClass = JsonConvert.DeserializeObject<Root_pdb_InternetExchangeFacility>(responseJson);
                //Getting the List of ids
                foreach (var pdb in myDeserializedClass.data)
                {
                    idList.Add(pdb.id);
                }
                //to empty memory
                myDeserializedClass = new Root_pdb_InternetExchangeFacility();
            }
            else if (collectionName == "pdb_network_to_ix_connection")
            {
                Root_pdb_InternetExchangeFacility myDeserializedClass = JsonConvert.DeserializeObject<Root_pdb_InternetExchangeFacility>(responseJson);
                //Getting the List of ids
                foreach (var pdb in myDeserializedClass.data)
                {
                    idList.Add(pdb.id);
                }
                //to empty memory
                myDeserializedClass = new Root_pdb_InternetExchangeFacility();
            }
            else if (collectionName== "pdb_Organization")
            {
                Root_pdb_NetworkToIXConnection myDeserializedClass = JsonConvert.DeserializeObject<Root_pdb_NetworkToIXConnection>(responseJson);
                //Getting the List of ids
                foreach (var pdb in myDeserializedClass.data)
                {
                    idList.Add(pdb.id);
                }
                //to empty memory
                myDeserializedClass = new Root_pdb_NetworkToIXConnection();
            } 
            else if (collectionName == "pdb_as_set")
            {
                //no need for id list/just to parse the response as a whole
            }

            //Write to File//saving to avoid block 
            File.WriteAllLines(idListFilePath, idList.Select(x => x.ToString()));
        }


        //2nd part
        //Getting data one by one and  adding to list



        if (collectionName == "pdb_datacenters")
        {
            //To handled for transformation
            List<Cpdb_tranformation> new_pdbDatacenters_List = new List<Cpdb_tranformation>();
            foreach (int id in idList)
            {
                string NewrequestUri = CollectionvsUri.First(kvp => kvp.Key == collectionName).Value + "/" + id;
                string newresponseJson = response.getWebRequestData(NewrequestUri);
                Root_pdb_InternetExchange newMyDeserializedClass = JsonConvert.DeserializeObject<Root_pdb_InternetExchange>(newresponseJson);
                
                
                
                pdbData_List.Add(newMyDeserializedClass.data[0]);
            }
            bool status = mongodb.InsertBatch(pdbData_List, collectionName, insertedBatchCount);

            if (status)
            {
                Console.WriteLine(collectionName + ": Inserted to Mongodb Successfully");
            }
            else
            {
                Console.WriteLine(collectionName + ": Exception occured while Inserting Data to Mongodb");
            }
        }
        else if (collectionName == "pdb_internet_exchanges")
        {
            List<pdb_InternetExchange> pdbData_List = new List<pdb_InternetExchange>();
            foreach (int id in idList)
            {
                string NewrequestUri = CollectionvsUri.First(kvp => kvp.Key == collectionName).Value + "/" + id;
                string newresponseJson = response.getWebRequestData(NewrequestUri);
                Root_pdb_InternetExchange newMyDeserializedClass = JsonConvert.DeserializeObject<Root_pdb_InternetExchange>(newresponseJson);
                pdbData_List.Add(newMyDeserializedClass.data[0]);
            }
            bool status = mongodb.InsertBatch(pdbData_List, collectionName, insertedBatchCount);

            if (status)
            {
                Console.WriteLine(collectionName + ": Inserted to Mongodb Successfully");
            }
            else
            {
                Console.WriteLine(collectionName + ": Exception occured while Inserting Data to Mongodb");
            }
        }
        else if (collectionName == "pdb_internet_exchange_facilities")
        {
            List<pdb_InternetExchangeFacility> pdbData_List = new List<pdb_InternetExchangeFacility>();
            foreach (int id in idList)
            {
                string NewrequestUri = CollectionvsUri.First(kvp => kvp.Key == collectionName).Value + "/" + id;
                string newresponseJson = response.getWebRequestData(NewrequestUri);
                Root_pdb_InternetExchangeFacility newMyDeserializedClass = JsonConvert.DeserializeObject<Root_pdb_InternetExchangeFacility>(newresponseJson);
                pdbData_List.Add(newMyDeserializedClass.data[0]);
            }
            bool status = mongodb.InsertBatch(pdbData_List, collectionName, insertedBatchCount);

            if (status)
            {
                Console.WriteLine(collectionName + ": Inserted to Mongodb Successfully");
            }
            else
            {
                Console.WriteLine(collectionName + ": Exception occured while Inserting Data to Mongodb");
            }
        }
        else if (collectionName == "pdb_internet_exchange_networks")
        {
            List<pdb_internet_exchange_networks> pdbData_List = new List<pdb_internet_exchange_networks>();
            foreach (int id in idList)
            {
                string NewrequestUri = CollectionvsUri.First(kvp => kvp.Key == collectionName).Value + "/" + id;
                string newresponseJson = response.getWebRequestData(NewrequestUri);
                Root_pdb_internet_exchange_networks newMyDeserializedClass = JsonConvert.DeserializeObject<Root_pdb_internet_exchange_networks>(newresponseJson);
                pdbData_List.Add(newMyDeserializedClass.data[0]);
            }
            bool status = mongodb.InsertBatch(pdbData_List, collectionName, insertedBatchCount);

            if (status)
            {
                Console.WriteLine(collectionName + ": Inserted to Mongodb Successfully");
            }
            else
            {
                Console.WriteLine(collectionName + ": Exception occured while Inserting Data to Mongodb");
            }
        }
        else if (collectionName == "pdb_internet_exchange_prefixes")
        {
            List<pdb_internet_exchange_prefixes> pdbData_List = new List<pdb_internet_exchange_prefixes>();
            foreach (int id in idList)
            {
                string NewrequestUri = CollectionvsUri.First(kvp => kvp.Key == collectionName).Value + "/" + id;
                string newresponseJson = response.getWebRequestData(NewrequestUri);
                Root_pdb_internet_exchange_prefixes newMyDeserializedClass = JsonConvert.DeserializeObject<Root_pdb_internet_exchange_prefixes>(newresponseJson);
                pdbData_List.Add(newMyDeserializedClass.data[0]);
            }
            bool status = mongodb.InsertBatch(pdbData_List, collectionName, insertedBatchCount);

            if (status)
            {
                Console.WriteLine(collectionName + ": Inserted to Mongodb Successfully");
            }
            else
            {
                Console.WriteLine(collectionName + ": Exception occured while Inserting Data to Mongodb");
            }
        }
        else if (collectionName == "pdb_networks")
        {
            List<pdb_Network> pdbData_List = new List<pdb_Network>();
            foreach (int id in idList)
            {
                string NewrequestUri = CollectionvsUri.First(kvp => kvp.Key == collectionName).Value + "/" + id;
                string newresponseJson = response.getWebRequestData(NewrequestUri);
                Root_pdb_Network newMyDeserializedClass = JsonConvert.DeserializeObject<Root_pdb_Network>(newresponseJson);
                pdbData_List.Add(newMyDeserializedClass.data[0]);
            }
            bool status = mongodb.InsertBatch(pdbData_List, collectionName, insertedBatchCount);

            if (status)
            {
                Console.WriteLine(collectionName + ": Inserted to Mongodb Successfully");
            }
            else
            {
                Console.WriteLine(collectionName + ": Exception occured while Inserting Data to Mongodb");
            }
        }
        else if (collectionName == "pdb_network_facilities")
        {
            List<pdb_NetworkFacility> pdbData_List = new List<pdb_NetworkFacility>();
            foreach (int id in idList)
            {
                string NewrequestUri = CollectionvsUri.First(kvp => kvp.Key == collectionName).Value + "/" + id;
                string newresponseJson = response.getWebRequestData(NewrequestUri);
                Root_pdb_NetworkFacility newMyDeserializedClass = JsonConvert.DeserializeObject<Root_pdb_NetworkFacility>(newresponseJson);
                pdbData_List.Add(newMyDeserializedClass.data[0]);
            }
            bool status = mongodb.InsertBatch(pdbData_List, collectionName, insertedBatchCount);

            if (status)
            {
                Console.WriteLine(collectionName + ": Inserted to Mongodb Successfully");
            }
            else
            {
                Console.WriteLine(collectionName + ": Exception occured while Inserting Data to Mongodb");
            }
        }
        else if (collectionName == "pdb_network_to_ix_connection")
        {
            List<pdb_NetworkToIXConnection> pdbData_List = new List<pdb_NetworkToIXConnection>();
            foreach (int id in idList)
            {
                string NewrequestUri = CollectionvsUri.First(kvp => kvp.Key == collectionName).Value + "/" + id;
                string newresponseJson = response.getWebRequestData(NewrequestUri);
                Root_pdb_NetworkToIXConnection newMyDeserializedClass = JsonConvert.DeserializeObject<Root_pdb_NetworkToIXConnection>(newresponseJson);
                pdbData_List.Add(newMyDeserializedClass.data[0]);
            }
            bool status = mongodb.InsertBatch(pdbData_List, collectionName, insertedBatchCount);

            if (status)
            {
                Console.WriteLine(collectionName + ": Inserted to Mongodb Successfully");
            }
            else
            {
                Console.WriteLine(collectionName + ": Exception occured while Inserting Data to Mongodb");
            }
        }
        else if (collectionName == "pdb_Organization")
        {
            List<pdb_Organization> pdbData_List = new List<pdb_Organization>();
            foreach (int id in idList)
            {
                string NewrequestUri = CollectionvsUri.First(kvp => kvp.Key == collectionName).Value + "/" + id;
                string newresponseJson = response.getWebRequestData(NewrequestUri);
                Root_pdb_Organization newMyDeserializedClass = JsonConvert.DeserializeObject<Root_pdb_Organization>(newresponseJson);
                pdbData_List.Add(newMyDeserializedClass.data[0]);
            }
            bool status= mongodb.InsertBatch(pdbData_List, collectionName, insertedBatchCount);

            if(status)
            {
                Console.WriteLine(collectionName+": Inserted to Mongodb Successfully");
            }
            else
            {
                Console.WriteLine(collectionName + ": Exception occured while Inserting Data to Mongodb");
            }
        }
        else if (collectionName == "pdb_as_set")
        {
            //to be handled differently
        }

    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}






