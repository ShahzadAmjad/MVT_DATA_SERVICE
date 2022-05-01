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
            IP= "mongodb://localhost:27017";
            var client= new MongoClient(IP);
            //IMongoDatabase database= client.GetDatabase(DbName);
            //var command = new BsonDocument { { "dbStats",1 }, {"scale",1 } };
            //var res= database.RunCommand<BsonDocument>(command);
            return client;
        }

        public bool InsertBatch<T>(List<T> list, string DbName, string tableName, string MongoIP)
        {
            bool status=false;
            try
            {
                var client = OpenDBConn(DbName, MongoIP);

                if (list is List<Employee>)
                {
                    List<Employee> batch = list as List<Employee>;
                    var database = client.GetDatabase(DbName);
                    var collection= database.GetCollection<Employee>(tableName);
                    collection.InsertMany((IEnumerable<Employee>)list);
                    status=true;
                    //return collection.ApiCall.Success;
                }
                else if (list is List<Student>)
                {
                    List<Student> batch = list as List<Student>;
                    var database = client.GetDatabase(DbName);
                    var collection = database.GetCollection<Student>(tableName);
                    collection.InsertMany((IEnumerable<Student>)list);
                    status = true;
                    //return index.ApiCall.Success;
                }
            }
            catch(Exception ex)
            { 
                status=false;
            }
            return status;
        }

    }
}
