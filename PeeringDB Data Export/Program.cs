// See https://aka.ms/new-console-template for more information
using MongoDB.Driver;
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
        IMongoDatabase database = mongodb.GetMongoDatabase();
        var _mongoCollection = database.GetCollection<Cpdb_tranformation>(collectionName);
        var filter = Builders<Cpdb_tranformation>.Filter.Empty;
        var options = new FindOptions<Cpdb_tranformation>
        {
            // Get 10 docs at a time
            BatchSize = 10
        };

        using (var cursor = _mongoCollection.FindSync(filter, options))
        {
            // Move to the next batch of docs
            while (await cursor.MoveNextAsync())
            {
                var batch = cursor.Current;
                foreach (var doc in batch)
                {
                    // TO DO
                }
                //await cursor.ForEachAsync(doc =>
                //{
                //    // process doc
                //});
            }
        }
    }
    else if (collectionName == "pdb_internet_exchanges")
    {
        Console.WriteLine("Getting data for: " + collectionName);
        IMongoDatabase database = mongodb.GetMongoDatabase();
        var _mongoCollection = database.GetCollection<pdb_InternetExchange>(collectionName);
        var filter = Builders<pdb_InternetExchange>.Filter.Empty;
        var options = new FindOptions<pdb_InternetExchange>
        {
            // Get 10 docs at a time
            BatchSize = 10
        };

        using (var cursor = _mongoCollection.FindSync(filter, options))
        {
            // Move to the next batch of docs
            while (await cursor.MoveNextAsync())
            {
                var batch = cursor.Current;
                foreach (var doc in batch)
                {
                    // TO DO
                }
            }
        }
    }
    else if (collectionName == "pdb_internet_exchange_facilities")
    {
        Console.WriteLine("Getting data for: " + collectionName);
        IMongoDatabase database = mongodb.GetMongoDatabase();
        var _mongoCollection = database.GetCollection<pdb_InternetExchangeFacility>(collectionName);
        var filter = Builders<pdb_InternetExchangeFacility>.Filter.Empty;
        var options = new FindOptions<pdb_InternetExchangeFacility>
        {
            // Get 10 docs at a time
            BatchSize = 10
        };

        using (var cursor = _mongoCollection.FindSync(filter, options))
        {
            // Move to the next batch of docs
            while (await cursor.MoveNextAsync())
            {
                var batch = cursor.Current;
                foreach (var doc in batch)
                {
                    // TO DO
                }
            }
        }
    }
    else if (collectionName == "pdb_internet_exchange_networks")
    {
        Console.WriteLine("Getting data for: " + collectionName);
        IMongoDatabase database = mongodb.GetMongoDatabase();
        var _mongoCollection = database.GetCollection<pdb_internet_exchange_networks>(collectionName);
        var filter = Builders<pdb_internet_exchange_networks>.Filter.Empty;
        var options = new FindOptions<pdb_internet_exchange_networks>
        {
            // Get 10 docs at a time
            BatchSize = 10
        };

        using (var cursor = _mongoCollection.FindSync(filter, options))
        {
            // Move to the next batch of docs
            while (await cursor.MoveNextAsync())
            {
                var batch = cursor.Current;
                foreach (var doc in batch)
                {
                    // TO DO
                }
            }
        }
    }
    else if (collectionName == "pdb_internet_exchange_prefixes")
    {
        Console.WriteLine("Getting data for: " + collectionName);
        IMongoDatabase database = mongodb.GetMongoDatabase();
        var _mongoCollection = database.GetCollection<pdb_internet_exchange_prefixes>(collectionName);
        var filter = Builders<pdb_internet_exchange_prefixes>.Filter.Empty;
        var options = new FindOptions<pdb_internet_exchange_prefixes>
        {
            // Get 10 docs at a time
            BatchSize = 10
        };

        using (var cursor = _mongoCollection.FindSync(filter, options))
        {
            // Move to the next batch of docs
            while (await cursor.MoveNextAsync())
            {
                var batch = cursor.Current;
                foreach (var doc in batch)
                {
                    // TO DO
                }
            }
        }
    }
    else if (collectionName == "pdb_networks")
    {
        Console.WriteLine("Getting all data for: " + collectionName);
        IMongoDatabase database = mongodb.GetMongoDatabase();
        var _mongoCollection = database.GetCollection<pdb_Network>(collectionName);
        var filter = Builders<pdb_Network>.Filter.Empty;
        var options = new FindOptions<pdb_Network>
        {
            // Get 10 docs at a time
            BatchSize = 10
        };

        using (var cursor = _mongoCollection.FindSync(filter, options))
        {
            // Move to the next batch of docs
            while (await cursor.MoveNextAsync())
            {
                var batch = cursor.Current;
                foreach (var doc in batch)
                {
                    // TO DO
                }
            }
        }
    }
    else if (collectionName == "pdb_network_pocs")
    {
        Console.WriteLine("Getting data for: " + collectionName);
        IMongoDatabase database = mongodb.GetMongoDatabase();
        var _mongoCollection = database.GetCollection<pdb_NetworkPOC>(collectionName);
        var filter = Builders<pdb_NetworkPOC>.Filter.Empty;
        var options = new FindOptions<pdb_NetworkPOC>
        {
            // Get 10 docs at a time
            BatchSize = 10
        };

        using (var cursor = _mongoCollection.FindSync(filter, options))
        {
            // Move to the next batch of docs
            while (await cursor.MoveNextAsync())
            {
                var batch = cursor.Current;
                foreach (var doc in batch)
                {
                    // TO DO
                }
            }
        }
    }
    else if (collectionName == "pdb_network_facilities")
    {
        Console.WriteLine("Getting all data for: " + collectionName);
        IMongoDatabase database = mongodb.GetMongoDatabase();
        var _mongoCollection = database.GetCollection<pdb_NetworkFacility>(collectionName);
        var filter = Builders<pdb_NetworkFacility>.Filter.Empty;
        var options = new FindOptions<pdb_NetworkFacility>
        {
            // Get 10 docs at a time
            BatchSize = 10
        };

        using (var cursor = _mongoCollection.FindSync(filter, options))
        {
            // Move to the next batch of docs
            while (await cursor.MoveNextAsync())
            {
                var batch = cursor.Current;
                foreach (var doc in batch)
                {
                    // TO DO
                }
            }
        }
    }
    else if (collectionName == "pdb_network_to_ix_connection")
    {
        Console.WriteLine("Getting all data for: " + collectionName);
        IMongoDatabase database = mongodb.GetMongoDatabase();
        var _mongoCollection = database.GetCollection<pdb_NetworkToIXConnection>(collectionName);
        var filter = Builders<pdb_NetworkToIXConnection>.Filter.Empty;
        var options = new FindOptions<pdb_NetworkToIXConnection>
        {
            // Get 10 docs at a time
            BatchSize = 10
        };

        using (var cursor = _mongoCollection.FindSync(filter, options))
        {
            // Move to the next batch of docs
            while (await cursor.MoveNextAsync())
            {
                var batch = cursor.Current;
                foreach (var doc in batch)
                {
                    // TO DO
                }
            }
        }
    }
    else if (collectionName == "pdb_organizations")
    {
        Console.WriteLine("Getting data for: " + collectionName);
        IMongoDatabase database = mongodb.GetMongoDatabase();
        var _mongoCollection = database.GetCollection<pdb_Organization>(collectionName);
        var filter = Builders<pdb_Organization>.Filter.Empty;
        var options = new FindOptions<pdb_Organization>
        {
            // Get 10 docs at a time
            BatchSize = 10
        };

        using (var cursor = _mongoCollection.FindSync(filter, options))
        {
            // Move to the next batch of docs
            while (await cursor.MoveNextAsync())
            {
                var batch = cursor.Current;
                foreach (var doc in batch)
                {
                    // TO DO
                }
            }
        }
    }
    else if (collectionName == "pdb_as_set")
    {
        Console.WriteLine("Getting all data for: " + collectionName);
        //List<pdb_AS_SET> pdbData_List = new List<pdb_AS_SET>();
        IMongoDatabase database = mongodb.GetMongoDatabase();
        var _mongoCollection = database.GetCollection<pdb_AS_SET>(collectionName);
        var filter = Builders<pdb_AS_SET>.Filter.Empty;
        var options = new FindOptions<pdb_AS_SET>
        {
            // Get 10 docs at a time
            BatchSize = 10
        };

        using (var cursor = _mongoCollection.FindSync(filter, options))
        {
            // Move to the next batch of docs
            while (await cursor.MoveNextAsync())
            {
                var batch = cursor.Current;
                foreach (var doc in batch)
                {
                    // TO DO
                }
            }
        }
    }

}