﻿using MongoDB.Driver;
using PeeringDB_Data_Import.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeeringDB_Data_Import
{
    public class CMongodb
    {
        //public  dynamic GetCollectionasync(string collectionName)
        //{
        //    var client = OpenDBConn();
        //    IMongoDatabase database = client.GetDatabase("mvt");
        //    var collection = database.GetCollection<Cpdb_tranformation>(collectionName);
        //    var doc =await collection.Find(Builders<Cpdb_tranformation>.Filter.Empty).ToListAsync();
        //    return doc;
        //}
        public List<Cpdb_tranformation> LoadAll_pdb_datacenters(string collectionName)
        {
            
            List<Cpdb_tranformation> mylist = new List<Cpdb_tranformation>();
            var client = OpenDBConn();
            IMongoDatabase database = client.GetDatabase("mvt");
            var _mongoCollection = database.GetCollection<Cpdb_tranformation>(collectionName);

            using (var cursor = _mongoCollection.FindSync(Builders<Cpdb_tranformation>.Filter.Empty))
            {
                cursor.MoveNextAsync().Wait();
                Task<bool> cursorTask;
                do
                {
                    
                    var events = cursor.Current;
                    cursorTask = cursor.MoveNextAsync();                   
                    mylist.AddRange(events);
                    cursorTask.Wait(2000);
                    //yield return events.AsParallel().Select(e => e).ToList();
                } while (cursorTask.Result);
            }

            return mylist;
        }

        public List<pdb_InternetExchange> LoadAll_pdb_internet_exchanges(string collectionName)
        {

            List<pdb_InternetExchange> mylist = new List<pdb_InternetExchange>();
            var client = OpenDBConn();
            IMongoDatabase database = client.GetDatabase("mvt");
            var _mongoCollection = database.GetCollection<pdb_InternetExchange>(collectionName);

            using (var cursor = _mongoCollection.FindSync(Builders<pdb_InternetExchange>.Filter.Empty))
            {
                cursor.MoveNextAsync().Wait();
                Task<bool> cursorTask;
                do
                {

                    var events = cursor.Current;
                    cursorTask = cursor.MoveNextAsync();
                    mylist.AddRange(events);
                    cursorTask.Wait(2000);
                    //yield return events.AsParallel().Select(e => e).ToList();
                } while (cursorTask.Result);
            }

            return mylist;
        }
        public List<pdb_InternetExchangeFacility> LoadAll_pdb_internet_exchange_facilities(string collectionName)
        {
            List<pdb_InternetExchangeFacility> mylist = new List<pdb_InternetExchangeFacility>();
            try
            {
                
                var client = OpenDBConn();
                IMongoDatabase database = client.GetDatabase("mvt");
                var _mongoCollection = database.GetCollection<pdb_InternetExchangeFacility>(collectionName);
                
                var options = new FindOptions<pdb_InternetExchangeFacility>
                {
                    // Get 10 docs at a time
                    BatchSize = 100
                };
                
                AggregateOptions aggregateOptions = new AggregateOptions();
                aggregateOptions.BatchSize = 50;

                using (var cursor =  _mongoCollection.FindSync(Builders<pdb_InternetExchangeFacility>.Filter.Empty))
                {
                    //cursor.batchSize(10);
                    cursor.MoveNextAsync().Wait();
                    Task<bool> cursorTask;
                    do
                    {

                        var events = cursor.Current;
                        cursorTask = cursor.MoveNextAsync();
                        mylist.AddRange(events);
                        cursorTask.Wait(2000);
                        //yield return events.AsParallel().Select(e => e).ToList();
                    } while (cursorTask.Result);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            

            return mylist;
        }

        public async Task<List<pdb_internet_exchange_networks>> AsyncLoadAll_pdb_internet_exchange_networks(string collectionName)
        {
            List<pdb_internet_exchange_networks> mylist = new List<pdb_internet_exchange_networks>();
            var client = OpenDBConn();
            IMongoDatabase database = client.GetDatabase("mvt");
            var _mongoCollection = database.GetCollection<pdb_internet_exchange_networks>(collectionName);
            var filter = Builders<pdb_internet_exchange_networks>.Filter.Empty;
            var options = new FindOptions<pdb_internet_exchange_networks>
            {
                // Get 10 docs at a time
                BatchSize = 10,
                //AllowPartialResults = true,
                //Limit =10

            };


            using (var cursor = _mongoCollection.FindSync(filter))
            {
                cursor.MoveNextAsync().Wait();
                Task<bool> cursorTask;
                do
                {
                    var events = cursor.Current;
                    cursorTask = cursor.MoveNextAsync();
                    mylist.AddRange(events);
                    cursorTask.Wait(2000);

                    //yield return events.AsParallel().Select(e => e).ToList();
                } while (cursorTask.Result);
            }

            return mylist;
        }

        public async Task<dynamic> getListAsync(string collectionName)
        {
            var client = OpenDBConn();
            IMongoDatabase database = client.GetDatabase("mvt");
            var collection = database.GetCollection<Cpdb_tranformation>(collectionName);

            var list = await collection.Find(Builders<Cpdb_tranformation>.Filter.Empty).Limit(5000).ToListAsync();
            return list;
        }
        public  dynamic GetCollection(string collectionName)
        {
            try
            {
                var client = OpenDBConn();
                IMongoDatabase database = client.GetDatabase("mvt");

                if (collectionName == "pdb_datacenters")
                {
                    try
                    {
                        var collection = database.GetCollection<Cpdb_tranformation>(collectionName);
                        return collection.Find(Builders<Cpdb_tranformation>.Filter.Empty).ToList();

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                }
                else if (collectionName == "pdb_internet_exchanges")
                {
                    //FindOptions options = new FindOptions {  NoCursorTimeout=true};
                    var collection = database.GetCollection<pdb_InternetExchange>(collectionName);
                    return collection.Find(Builders<pdb_InternetExchange>.Filter.Empty).ToList();
                    //var cursor= collection.FindAsync(Builders<pdb_InternetExchange>.Filter.Empty);
                    //return cursor;
                    //return doc;
                }
                else if (collectionName == "pdb_internet_exchange_facilities")
                {
                    var collection = database.GetCollection<pdb_InternetExchangeFacility>(collectionName);
                    return collection.Find(Builders<pdb_InternetExchangeFacility>.Filter.Empty).ToList();
                }
                else if (collectionName == "pdb_internet_exchange_networks")
                {
                    var collection = database.GetCollection<pdb_internet_exchange_networks>(collectionName);
                    return collection.Find(Builders<pdb_internet_exchange_networks>.Filter.Empty).ToList();
                }
                else if (collectionName == "pdb_internet_exchange_prefixes")
                {
                    var collection = database.GetCollection<pdb_internet_exchange_prefixes>(collectionName);
                    var filter = Builders<pdb_internet_exchange_prefixes>.Filter.Empty;
                    
                    return collection.Find(Builders<pdb_internet_exchange_prefixes>.Filter.Empty).ToList();
                }
                else if (collectionName == "pdb_networks")
                {
                    var collection = database.GetCollection<pdb_Network>(collectionName);
                    return collection.Find(Builders<pdb_Network>.Filter.Empty).ToList();
                }
                else if (collectionName == "pdb_network_pocs")
                {
                    var collection = database.GetCollection<pdb_NetworkPOC>(collectionName);
                    return collection.Find(Builders<pdb_NetworkPOC>.Filter.Empty).ToList();
                }
                else if (collectionName == "pdb_network_facilities")
                {
                    FindOptions options = new FindOptions();
                    TimeSpan span = TimeSpan.FromSeconds(500);
                    options.MaxAwaitTime = span;
                    options.NoCursorTimeout = true;
                    var collection = database.GetCollection<pdb_NetworkFacility>(collectionName);
                    return collection.Find(Builders<pdb_NetworkFacility>.Filter.Empty, options).Limit(200).ToListAsync();
                }
                else if (collectionName == "pdb_network_to_ix_connection")
                {
                    var collection = database.GetCollection<pdb_NetworkToIXConnection>(collectionName);
                    return collection.Find(Builders<pdb_NetworkToIXConnection>.Filter.Empty).ToList();
                }
                else if (collectionName == "pdb_organizations")
                {
                    var collection = database.GetCollection<pdb_Organization>(collectionName);
                    return collection.Find(Builders<pdb_Organization>.Filter.Empty).ToList();
                }
                else if (collectionName == "pdb_as_set")
                {
                    var collection = database.GetCollection<pdb_AS_SET>(collectionName);
                    return collection.Find(Builders<pdb_AS_SET>.Filter.Empty).ToList();
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                
            }
            return null;

        }
        public bool InsertBatch<T>(List<T> list, string collectionName, int insertedBatchCount)
        {
            bool status = false;
            try
            {
                var client = OpenDBConn();
                IMongoDatabase database = client.GetDatabase("mvt");

                //for pdb_datacenters
                if (list is List<Cpdb_tranformation>)
                {
                    List<Cpdb_tranformation> batch = list as List<Cpdb_tranformation>;
                    //to empty previous records if any
                    if (insertedBatchCount == 0)
                    {
                        database.DropCollection(collectionName);
                    }
                    var collection = database.GetCollection<Cpdb_tranformation>(collectionName);
                    collection.InsertMany((IEnumerable<Cpdb_tranformation>)batch);
                    status = true;
                }
                else if (list is List<pdb_InternetExchange>)
                {
                    List<pdb_InternetExchange> batch = list as List<pdb_InternetExchange>;
                    //to empty previous records if any
                    if (insertedBatchCount == 0)
                    {
                        database.DropCollection(collectionName);
                    }
                    var collection = database.GetCollection<pdb_InternetExchange>(collectionName);
                    collection.InsertMany((IEnumerable<pdb_InternetExchange>)batch);
                    status = true;
                }
                else if(list is List<pdb_InternetExchangeFacility>)
                {
                    List<pdb_InternetExchangeFacility> batch = list as List<pdb_InternetExchangeFacility>;
                    //to empty previous records if any
                    if (insertedBatchCount == 0)
                    {
                        database.DropCollection(collectionName);
                    }
                    var collection = database.GetCollection<pdb_InternetExchangeFacility>(collectionName);
                    collection.InsertMany((IEnumerable<pdb_InternetExchangeFacility>)batch);
                    status = true;
                }
                else if(list is List<pdb_internet_exchange_networks>)
                {
                    List<pdb_internet_exchange_networks> batch = list as List<pdb_internet_exchange_networks>;
                    //to empty previous records if any
                    if (insertedBatchCount == 0)
                    {
                        database.DropCollection(collectionName);
                    }
                    var collection = database.GetCollection<pdb_internet_exchange_networks>(collectionName);
                    collection.InsertMany((IEnumerable<pdb_internet_exchange_networks>)batch);
                    status = true;
                }
                else if(list is List<pdb_internet_exchange_prefixes>)
                {
                    List<pdb_internet_exchange_prefixes> batch = list as List<pdb_internet_exchange_prefixes>;
                    //to empty previous records if any
                    if (insertedBatchCount == 0)
                    {
                        database.DropCollection(collectionName);
                    }
                    var collection = database.GetCollection<pdb_internet_exchange_prefixes>(collectionName);
                    collection.InsertMany((IEnumerable<pdb_internet_exchange_prefixes>)batch);
                    status = true;
                }
                else if (list is List<pdb_Network>)
                {
                    List<pdb_Network> batch = list as List<pdb_Network>;
                    //to empty previous records if any
                    if (insertedBatchCount == 0)
                    {
                        database.DropCollection(collectionName);
                    }
                    var collection = database.GetCollection<pdb_Network>(collectionName);
                    collection.InsertMany((IEnumerable<pdb_Network>)batch);
                    status = true;
                }
                else if (list is List<pdb_NetworkPOC>)
                {
                    List<pdb_NetworkPOC> batch = list as List<pdb_NetworkPOC>;
                    //to empty previous records if any
                    if (insertedBatchCount == 0)
                    {
                        database.DropCollection(collectionName);
                    }
                    var collection = database.GetCollection<pdb_NetworkPOC>(collectionName);
                    collection.InsertMany((IEnumerable<pdb_NetworkPOC>)batch);
                    status = true;
                }
                else if (list is List<pdb_NetworkFacility>)
                {
                    List<pdb_NetworkFacility> batch = list as List<pdb_NetworkFacility>;
                    //to empty previous records if any
                    if (insertedBatchCount == 0)
                    {
                        database.DropCollection(collectionName);
                    }
                    var collection = database.GetCollection<pdb_NetworkFacility>(collectionName);
                    collection.InsertMany((IEnumerable<pdb_NetworkFacility>)batch);
                    status = true;
                }
                else if (list is List<pdb_NetworkToIXConnection>)
                {
                    List<pdb_NetworkToIXConnection> batch = list as List<pdb_NetworkToIXConnection>;
                    //to empty previous records if any
                    if (insertedBatchCount == 0)
                    {
                        database.DropCollection(collectionName);
                    }
                    var collection = database.GetCollection<pdb_NetworkToIXConnection>(collectionName);
                    collection.InsertMany((IEnumerable<pdb_NetworkToIXConnection>)batch);
                    status = true;
                }
                else if (list is List<pdb_Organization>)
                {
                    List<pdb_Organization> batch = list as List<pdb_Organization>;
                    //to empty previous records if any
                    if (insertedBatchCount == 0)
                    {
                        database.DropCollection(collectionName);
                    }
                    var collection = database.GetCollection<pdb_Organization>(collectionName);
                    collection.InsertMany((IEnumerable<pdb_Organization>)batch);

                    status=true;
                }
                else if (list is List<pdb_AS_SET>)
                {
                    List<pdb_AS_SET> batch = list as List<pdb_AS_SET>;
                    //to empty previous records if any
                    if (insertedBatchCount == 0)
                    {
                        database.DropCollection(collectionName);
                    }
                    var collection = database.GetCollection<pdb_AS_SET>(collectionName);
                    collection.InsertMany((IEnumerable<pdb_AS_SET>)batch);
                    status = true;
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
           
           
            //MongoUrl url = new MongoUrl("mongodb://mvtdev:-B7Q7acF9%3FK%40KptN@dev.geomentary.com:27017/?authMechanism=SCRAM-SHA-256&authSource=mvt&maxPoolSize=200");
            //url.MaxConnectionPoolSize = 5000;
            
            //string ConnectionStringCompass = "mongodb://mvtdev:-B7Q7acF9%3FK%40KptN@dev.geomentary.com:27017/?authMechanism=SCRAM-SHA-256&authSource=mvt";
            
            string ConnectionStringCompass = "mongodb://mvtdev:-B7Q7acF9%3FK%40KptN@dev.geomentary.com:27017/?connectTimeoutMS=3000&authMechanism=SCRAM-SHA-256&authSource=mvt";
            
            MongoClientSettings settings = MongoClientSettings.FromConnectionString(ConnectionStringCompass);
            settings.ReadConcern = ReadConcern.Majority;
            //settings.WaitQueueTimeout = TimeSpan.FromMinutes(1);
            //settings.MinConnectionPoolSize = 100;
            //settings.MaxConnectionPoolSize = 500;


            //settings.Server = new MongoServerAddress( ConnectionStringCompass);
            var client = new MongoClient(settings);
            //client.Settings.ReadConcern = ReadConcern.Majority;
            return client;
        }
    }
}
