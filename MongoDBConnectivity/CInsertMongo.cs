using DummyDataETL;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBConnectivity
{
    public class CInsertMongo
    {
        public MongoClient OpenDBConn(string DbName, string IP)
        {
            IP = "mongodb://dev.geomentary.com:27017";//"mongodb://localhost:27017";
            var client= new MongoClient(IP);
           
            IMongoDatabase database= client.GetDatabase(DbName);
            var command = new BsonDocument { { "dbStats",1 }, {"scale",1 } };
            var res= database.RunCommand<BsonDocument>(command);
            
            return client;
        }

        public bool InsertBatch<T>(List<T> list, string DbName, string collectionName, string MongoIP)
        {
            bool status=false;
            try
            {
                var client = OpenDBConn(DbName, MongoIP);
                var database = client.GetDatabase(DbName);

                if (list is List<Employee>)
                {
                    List<Employee> batch = list as List<Employee>; 
                    
                    if(CollectionExists(database, collectionName))
                    {
                        var collection = database.GetCollection<Employee>(collectionName);
                        collection.InsertMany((IEnumerable<Employee>)list);
                        status = true;
                    }

                    
                }
                else 
                {
                    //List<Student> batch = list as List<Student>;
                    //var collection = database.GetCollection<Student>(collectionName);
                    //collection.InsertMany((IEnumerable<Student>)list);
                    //status = true;
                }
            }
            catch(Exception ex)
            { 
                status=false;
            }
            return status;
        }

        public bool CollectionExists(IMongoDatabase database, string collectionName)
        {
            var filter = new BsonDocument("name", collectionName);
            var options = new ListCollectionNamesOptions { Filter = filter };

            return database.ListCollectionNames(options).Any();
        }
    }
}
