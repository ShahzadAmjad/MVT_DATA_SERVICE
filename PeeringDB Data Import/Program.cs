using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using PeeringDB_Data_Import;
using PeeringDB_Data_Import.Models;
using System.Net;

Console.WriteLine("MVT Data Service started");
//variable reserved for batching
int insertedBatchCount = 0;
//to resume operation after restart we save the inserted collections
string MetafilesDirectoryPath = AppDomain.CurrentDomain.BaseDirectory + @"\MetaFiles";
bool exists = System.IO.Directory.Exists(MetafilesDirectoryPath);

if (!exists)
    System.IO.Directory.CreateDirectory(MetafilesDirectoryPath);

List<string> InsertedcollectionsList = new List<string>();
string InsertedcollectionsListFilePath = AppDomain.CurrentDomain.BaseDirectory + @"\MetaFiles\inserted_collectionsList.txt";
if (File.Exists(InsertedcollectionsListFilePath))
{
    InsertedcollectionsList = File.ReadAllLines(InsertedcollectionsListFilePath).Select(x => (x)).ToList();
}
//list of all collections 
List<string> collectionList = new List<string> 
{"pdb_datacenters","pdb_internet_exchanges","pdb_internet_exchange_facilities",
    "pdb_internet_exchange_networks","pdb_internet_exchange_prefixes","pdb_networks",
    "pdb_network_pocs","pdb_network_facilities","pdb_network_to_ix_connection","pdb_organizations","pdb_as_set"};

//key value pair for collection vs uri's
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
CollectionvsUri.Add(new KeyValuePair<string, string>("pdb_organizations", "https://www.peeringdb.com/api/org"));
CollectionvsUri.Add(new KeyValuePair<string, string>("pdb_as_set", "https://www.peeringdb.com/api/as_set"));


foreach (var collectionName in collectionList)
{
    //check if already updated or not
    if(InsertedcollectionsList.Contains(collectionName))
    {
        Console.WriteLine(collectionName+ ": Recently updated");
    }
    else
    {
        WebRequestResponse response = new WebRequestResponse();
        CMongodb mongodb = new CMongodb();

        //This block only get the id's list for each collection
        //saving file to root directory
        string dd = (DateTime.Now).Day.ToString("00");
        string mm = (DateTime.Now).Month.ToString("00");
        string yyyy = (DateTime.Now).Year.ToString();
        //file name format is CollectionName_idList_yyyymmdd
        string idListFilePath = AppDomain.CurrentDomain.BaseDirectory + @"\MetaFiles\" + collectionName + "_idList_"+yyyy+mm+dd+".txt";
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
                string responseJson = response.getWebRequestData(requestUri);
                
                ////for testing
                //string requestUri = "";
                //string responseJson = "";
                   
                //if(collectionName== "pdb_network_pocs")
                //{
                //    requestUri = CollectionvsUri.First(kvp => kvp.Key == collectionName).Value + "/1168";
                //    responseJson = response.getWebRequestData(requestUri);
                //}
                //if (collectionName == "pdb_network_facilities")
                //{
                //    requestUri = CollectionvsUri.First(kvp => kvp.Key == collectionName).Value + "/580";
                //    responseJson = response.getWebRequestData(requestUri);
                //}
                //if (collectionName == "pdb_network_to_ix_connection")
                //{
                //    requestUri = CollectionvsUri.First(kvp => kvp.Key == collectionName).Value + "/589";
                //    responseJson = response.getWebRequestData(requestUri);
                //}
                

                /////////////////////testing script//////////////////////////////////
                Console.WriteLine("Deserializing Json Data id's List For: " + collectionName);

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
                else if (collectionName == "pdb_network_pocs")
                {
                    Root_pdb_NetworkPOC myDeserializedClass = JsonConvert.DeserializeObject<Root_pdb_NetworkPOC>(responseJson);
                    //Getting the List of ids
                    foreach (var pdb in myDeserializedClass.data)
                    {
                        idList.Add(pdb.id);
                    }
                    //to empty memory
                    myDeserializedClass = new Root_pdb_NetworkPOC();
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
                else if (collectionName == "pdb_organizations")
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
                    //no need for id list: just to parse the response as a whole
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
                    try
                    {
                        Console.WriteLine(collectionName + ": Getting Data for Id: " + id);
                        string NewrequestUri = CollectionvsUri.First(kvp => kvp.Key == collectionName).Value + "/" + id;
                        string newresponseJson = response.getWebRequestData(NewrequestUri);
                        Root_pdb_datacenters newMyDeserializedClass = JsonConvert.DeserializeObject<Root_pdb_datacenters>(newresponseJson);

                        pdb_datacenters pdbDatacenterObj = new pdb_datacenters();
                        pdbDatacenterObj = newMyDeserializedClass.data[0];

                        //tranforming object to new modal class
                        Cpdb_tranformation new_pdbDatacenterObj = new Cpdb_tranformation();
                        new_pdbDatacenterObj._id = (id).ToString();
                        new_pdbDatacenterObj.type = "Feature";
                        new_pdbDatacenterObj.geometry = new Geometry();
                        new_pdbDatacenterObj.geometry.type = "Point";
                        new_pdbDatacenterObj.geometry.coordinates = new List<double?>();
                        new_pdbDatacenterObj.geometry.coordinates.Add(pdbDatacenterObj.longitude);
                        new_pdbDatacenterObj.geometry.coordinates.Add(pdbDatacenterObj.latitude);
                        new_pdbDatacenterObj.properties = new pdb_datacenters();
                        new_pdbDatacenterObj.properties = pdbDatacenterObj;

                        new_pdbDatacenters_List.Add(new_pdbDatacenterObj);
                    }
                    catch (Exception ex)
                    { Console.WriteLine(ex.Message); }
                    
                }
                bool status = mongodb.InsertBatch(new_pdbDatacenters_List, collectionName, insertedBatchCount);

                if (status)
                {
                    Console.WriteLine(collectionName + ": Inserted to Mongodb Successfully");
                    //Write InsertedcollectionsList to file
                    InsertedcollectionsList.Add(collectionName);
                    File.WriteAllLines(InsertedcollectionsListFilePath, InsertedcollectionsList.Select(x => x.ToString()));

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
                    try
                    {
                        Console.WriteLine(collectionName + ": Getting Data for Id: " + id);
                        string NewrequestUri = CollectionvsUri.First(kvp => kvp.Key == collectionName).Value + "/" + id;
                        string newresponseJson = response.getWebRequestData(NewrequestUri);
                        Root_pdb_InternetExchange newMyDeserializedClass = JsonConvert.DeserializeObject<Root_pdb_InternetExchange>(newresponseJson);
                        pdbData_List.Add(newMyDeserializedClass.data[0]);
                    }
                    catch (Exception ex)
                    { Console.WriteLine(ex.Message); }
                    
                }
                bool status = mongodb.InsertBatch(pdbData_List, collectionName, insertedBatchCount);

                if (status)
                {
                    Console.WriteLine(collectionName + ": Inserted to Mongodb Successfully");
                    //Write InsertedcollectionsList to file
                    InsertedcollectionsList.Add(collectionName);
                    File.WriteAllLines(InsertedcollectionsListFilePath, InsertedcollectionsList.Select(x => x.ToString()));
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
                    try
                    {
                        Console.WriteLine(collectionName + ": Getting Data for Id: " + id);
                        string NewrequestUri = CollectionvsUri.First(kvp => kvp.Key == collectionName).Value + "/" + id;
                        string newresponseJson = response.getWebRequestData(NewrequestUri);
                        Root_pdb_InternetExchangeFacility newMyDeserializedClass = JsonConvert.DeserializeObject<Root_pdb_InternetExchangeFacility>(newresponseJson);
                        pdbData_List.Add(newMyDeserializedClass.data[0]);
                    }
                    catch (Exception ex)
                    { Console.WriteLine(ex.Message); }
                    
                }
                bool status = mongodb.InsertBatch(pdbData_List, collectionName, insertedBatchCount);

                if (status)
                {
                    //Write InsertedcollectionsList to file
                    InsertedcollectionsList.Add(collectionName);
                    File.WriteAllLines(InsertedcollectionsListFilePath, InsertedcollectionsList.Select(x => x.ToString()));
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
                    try
                    {
                        Console.WriteLine(collectionName + ": Getting Data for Id: " + id);
                        string NewrequestUri = CollectionvsUri.First(kvp => kvp.Key == collectionName).Value + "/" + id;
                        string newresponseJson = response.getWebRequestData(NewrequestUri);
                        Root_pdb_internet_exchange_networks newMyDeserializedClass = JsonConvert.DeserializeObject<Root_pdb_internet_exchange_networks>(newresponseJson);
                        pdbData_List.Add(newMyDeserializedClass.data[0]);
                    }
                    catch (Exception ex)
                    { Console.WriteLine(ex.Message); }
                    
                }
                bool status = mongodb.InsertBatch(pdbData_List, collectionName, insertedBatchCount);

                if (status)
                {
                    //Write InsertedcollectionsList to file
                    InsertedcollectionsList.Add(collectionName);
                    File.WriteAllLines(InsertedcollectionsListFilePath, InsertedcollectionsList.Select(x => x.ToString()));
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
                    try
                    {
                        Console.WriteLine(collectionName + ": Getting Data for Id: " + id);
                        string NewrequestUri = CollectionvsUri.First(kvp => kvp.Key == collectionName).Value + "/" + id;
                        string newresponseJson = response.getWebRequestData(NewrequestUri);
                        Root_pdb_internet_exchange_prefixes newMyDeserializedClass = JsonConvert.DeserializeObject<Root_pdb_internet_exchange_prefixes>(newresponseJson);
                        pdbData_List.Add(newMyDeserializedClass.data[0]);
                    }
                    catch (Exception ex)
                    { Console.WriteLine(ex.Message); }
                    
                }
                bool status = mongodb.InsertBatch(pdbData_List, collectionName, insertedBatchCount);

                if (status)
                {
                    //Write InsertedcollectionsList to file
                    InsertedcollectionsList.Add(collectionName);
                    File.WriteAllLines(InsertedcollectionsListFilePath, InsertedcollectionsList.Select(x => x.ToString()));
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
                    try
                    {
                        Console.WriteLine(collectionName + ": Getting Data for Id: " + id);
                        string NewrequestUri = CollectionvsUri.First(kvp => kvp.Key == collectionName).Value + "/" + id;
                        string newresponseJson = response.getWebRequestData(NewrequestUri);
                        Root_pdb_Network newMyDeserializedClass = JsonConvert.DeserializeObject<Root_pdb_Network>(newresponseJson);
                        pdbData_List.Add(newMyDeserializedClass.data[0]);
                    }
                    catch (Exception ex)
                    { Console.WriteLine(ex.Message); }
                    
                }
                bool status = mongodb.InsertBatch(pdbData_List, collectionName, insertedBatchCount);

                if (status)
                {
                    //Write InsertedcollectionsList to file
                    InsertedcollectionsList.Add(collectionName);
                    File.WriteAllLines(InsertedcollectionsListFilePath, InsertedcollectionsList.Select(x => x.ToString()));
                    Console.WriteLine(collectionName + ": Inserted to Mongodb Successfully");
                }
                else
                {
                    Console.WriteLine(collectionName + ": Exception occured while Inserting Data to Mongodb");
                }
            }
            else if (collectionName == "pdb_network_pocs")
            {
                List<pdb_NetworkPOC> pdbData_List = new List<pdb_NetworkPOC>();
                foreach (int id in idList)
                {
                    try
                    {
                        Console.WriteLine(collectionName + ": Getting Data for Id: " + id);
                        string NewrequestUri = CollectionvsUri.First(kvp => kvp.Key == collectionName).Value + "/" + id;
                        string newresponseJson = response.getWebRequestData(NewrequestUri);
                        Root_pdb_NetworkPOC newMyDeserializedClass = JsonConvert.DeserializeObject<Root_pdb_NetworkPOC>(newresponseJson);
                        pdbData_List.Add(newMyDeserializedClass.data[0]);
                    }
                    catch (Exception ex)
                    { Console.WriteLine(ex.Message); }
                    
                }
                bool status = mongodb.InsertBatch(pdbData_List, collectionName, insertedBatchCount);

                if (status)
                {
                    //Write InsertedcollectionsList to file
                    InsertedcollectionsList.Add(collectionName);
                    File.WriteAllLines(InsertedcollectionsListFilePath, InsertedcollectionsList.Select(x => x.ToString()));
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
                    try
                    {
                        Console.WriteLine(collectionName + ": Getting Data for Id: " + id);
                        string NewrequestUri = CollectionvsUri.First(kvp => kvp.Key == collectionName).Value + "/" + id;
                        string newresponseJson = response.getWebRequestData(NewrequestUri);
                        Root_pdb_NetworkFacility newMyDeserializedClass = JsonConvert.DeserializeObject<Root_pdb_NetworkFacility>(newresponseJson);
                        pdbData_List.Add(newMyDeserializedClass.data[0]);
                    }
                    catch (Exception ex)
                    { Console.WriteLine(ex.Message); }
                    
                }
                bool status = mongodb.InsertBatch(pdbData_List, collectionName, insertedBatchCount);

                if (status)
                {
                    //Write InsertedcollectionsList to file
                    InsertedcollectionsList.Add(collectionName);
                    File.WriteAllLines(InsertedcollectionsListFilePath, InsertedcollectionsList.Select(x => x.ToString()));
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
                    try
                    {
                        Console.WriteLine(collectionName + ": Getting Data for Id: " + id);
                        string NewrequestUri = CollectionvsUri.First(kvp => kvp.Key == collectionName).Value + "/" + id;
                        string newresponseJson = response.getWebRequestData(NewrequestUri);
                        Root_pdb_NetworkToIXConnection newMyDeserializedClass = JsonConvert.DeserializeObject<Root_pdb_NetworkToIXConnection>(newresponseJson);
                        pdbData_List.Add(newMyDeserializedClass.data[0]);
                    }
                    catch(Exception ex)
                    { Console.WriteLine(ex.Message); }                 
                }
                bool status = mongodb.InsertBatch(pdbData_List, collectionName, insertedBatchCount);

                if (status)
                {
                    //Write InsertedcollectionsList to file
                    InsertedcollectionsList.Add(collectionName);
                    File.WriteAllLines(InsertedcollectionsListFilePath, InsertedcollectionsList.Select(x => x.ToString()));
                    Console.WriteLine(collectionName + ": Inserted to Mongodb Successfully");
                }
                else
                {
                    Console.WriteLine(collectionName + ": Exception occured while Inserting Data to Mongodb");
                }
            }
            else if (collectionName == "pdb_organizations")
            {
                List<pdb_Organization> pdbData_List = new List<pdb_Organization>();
                foreach (int id in idList)
                {
                    try
                    {
                        Console.WriteLine(collectionName + ": Getting Data for Id: " + id);
                        string NewrequestUri = CollectionvsUri.First(kvp => kvp.Key == collectionName).Value + "/" + id;
                        string newresponseJson = response.getWebRequestData(NewrequestUri);
                        Root_pdb_Organization newMyDeserializedClass = JsonConvert.DeserializeObject<Root_pdb_Organization>(newresponseJson);
                        pdbData_List.Add(newMyDeserializedClass.data[0]);
                    }
                    catch(Exception ex)
                    { Console.WriteLine(ex.Message); }                  
                }
                bool status = mongodb.InsertBatch(pdbData_List, collectionName, insertedBatchCount);

                if (status)
                {
                    //Write InsertedcollectionsList to file
                    InsertedcollectionsList.Add(collectionName);
                    File.WriteAllLines(InsertedcollectionsListFilePath, InsertedcollectionsList.Select(x => x.ToString()));
                    Console.WriteLine(collectionName + ": Inserted to Mongodb Successfully");
                }
                else
                {
                    Console.WriteLine(collectionName + ": Exception occured while Inserting Data to Mongodb");
                }
            }
            else if (collectionName == "pdb_as_set")
            {
                List<pdb_AS_SET> pdb_List = new List<pdb_AS_SET>();
                string NewrequestUri = CollectionvsUri.First(kvp => kvp.Key == collectionName).Value;
                string responseJson = response.getWebRequestData(NewrequestUri);

                //Only for as set data deserialization
                string data = (responseJson.Split("[{"))[1];
                string[] dataList = data.Split(",");
                Console.WriteLine(collectionName + ": Parsing Data");
                foreach (string item in dataList)
                {
                    string[] itemList = item.Split(":");
                    if (itemList.Length >= 2)
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

                bool status = mongodb.InsertBatch(pdb_List, collectionName, insertedBatchCount);

                if (status)
                {
                    //Write InsertedcollectionsList to file
                    InsertedcollectionsList.Add(collectionName);
                    File.WriteAllLines(InsertedcollectionsListFilePath, InsertedcollectionsList.Select(x => x.ToString()));
                    Console.WriteLine(collectionName + ": Inserted to Mongodb Successfully");

                    //Delete Inserted collections File after all collections inserted

                    if(File.Exists(InsertedcollectionsListFilePath))
                    {
                        File.Delete(InsertedcollectionsListFilePath);
                    }
                }
                else
                {
                    Console.WriteLine(collectionName + ": Exception occured while Inserting Data to Mongodb");
                }

            }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }


    }


}






