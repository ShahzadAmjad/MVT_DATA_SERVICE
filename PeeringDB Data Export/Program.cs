// See https://aka.ms/new-console-template for more information
using PeeringDB_Data_Import;
using PeeringDB_Data_Import.Models;

Console.WriteLine("MVT Data Export Service started");


//list of all collections 
List<string> collectionList = new List<string>
{"pdb_datacenters","pdb_internet_exchanges","pdb_internet_exchange_facilities",
    "pdb_internet_exchange_networks","pdb_internet_exchange_prefixes","pdb_networks",
    "pdb_network_pocs","pdb_network_facilities","pdb_network_to_ix_connection","pdb_organizations","pdb_as_set"};

CMongodb mongodb = new CMongodb();

foreach (var collectionName in collectionList)
{
    
    if (collectionName == "pdb_datacenters")
    {
        Console.WriteLine("Getting all data for: " + collectionName); 
        //List<Cpdb_tranformation> pdbData_List = new List<Cpdb_tranformation>();
        //pdbData_List = mongodb.GetCollection(collectionName);
        //foreach (var doc in pdbData_List)
        //{
        //    //TO DO
        //}
    }
    else if (collectionName == "pdb_internet_exchanges")
    {
        //Console.WriteLine("Getting all data for: " + collectionName);
        //List<pdb_InternetExchange> pdbData_List = new List<pdb_InternetExchange>();
        //pdbData_List =  mongodb.GetCollection("pdb_organizations");

        //foreach (var doc in pdbData_List)
        //{
        //    //TO DO
        //}
    }
    else if (collectionName == "pdb_internet_exchange_facilities")
    {
        Console.WriteLine("Getting all data for: " + collectionName);
        //List<pdb_InternetExchangeFacility> pdbData_List = new List<pdb_InternetExchangeFacility>();
        //pdbData_List = mongodb.GetCollection(collectionName);
        //foreach (var doc in pdbData_List)
        //{
        //    //TO DO
        //}
    }
    else if (collectionName == "pdb_internet_exchange_networks")
    {
        Console.WriteLine("Getting all data for: " + collectionName);
        //List<pdb_internet_exchange_networks> pdbData_List = new List<pdb_internet_exchange_networks>();
        //pdbData_List = mongodb.GetCollection(collectionName);
        //foreach (var doc in pdbData_List)
        //{
        //    //TO DO
        //}
    }
    else if (collectionName == "pdb_internet_exchange_prefixes")
    {
        Console.WriteLine("Getting all data for: " + collectionName);
        //List<pdb_internet_exchange_prefixes> pdbData_List = new List<pdb_internet_exchange_prefixes>();
        //pdbData_List = mongodb.GetCollection(collectionName);
        //foreach (var doc in pdbData_List)
        //{
        //    //TO DO
        //}
    }
    else if (collectionName == "pdb_networks")
    {
        Console.WriteLine("Getting all data for: " + collectionName);
        //List<pdb_Network> pdbData_List = new List<pdb_Network>();
        //pdbData_List = mongodb.GetCollection(collectionName);
        //foreach (var doc in pdbData_List)
        //{
        //    //TO DO
        //}
    }
    else if (collectionName == "pdb_network_pocs")
    {
        Console.WriteLine("Getting all data for: " + collectionName);
        //List<pdb_NetworkPOC> pdbData_List = new List<pdb_NetworkPOC>();
        //pdbData_List = mongodb.GetCollection(collectionName);
        //foreach (var doc in pdbData_List)
        //{
        //    //TO DO
        //}
    }
    else if (collectionName == "pdb_network_facilities")
    {
        Console.WriteLine("Getting all data for: " + collectionName);
        List<pdb_NetworkFacility> pdbData_List = new List<pdb_NetworkFacility>();
        pdbData_List = await mongodb.GetCollection(collectionName);
        foreach (var doc in pdbData_List)
        {
            //TO DO
        }
    }
    else if (collectionName == "pdb_network_to_ix_connection")
    {
        Console.WriteLine("Getting all data for: " + collectionName);
        //List<pdb_NetworkToIXConnection> pdbData_List = new List<pdb_NetworkToIXConnection>();
        //pdbData_List = mongodb.GetCollection(collectionName);
        //foreach (var doc in pdbData_List)
        //{
        //    //TO DO
        //}
    }
    else if (collectionName == "pdb_organizations")
    {
        Console.WriteLine("Getting all data for: " + collectionName);
        //List<pdb_Organization> pdbData_List = new List<pdb_Organization>();
        //pdbData_List = mongodb.GetCollection(collectionName);
        //foreach (var doc in pdbData_List)
        //{
        //    //TO DO
        //}
    }
    else if (collectionName == "pdb_as_set")
    {
        Console.WriteLine("Getting all data for: " + collectionName);
        List<pdb_AS_SET> pdbData_List = new List<pdb_AS_SET>();
        pdbData_List = mongodb.GetCollection(collectionName);
        foreach (var doc in pdbData_List)
        {
            //TO DO
        }
    }
}