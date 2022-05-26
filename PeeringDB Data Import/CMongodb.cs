using MongoDB.Driver;
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
        public bool InsertBatch<T>(List<T> list, string collectionName, int insertedBatchCount)
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
                    if (insertedBatchCount == 0)
                    {
                        database.DropCollection(collectionName);
                    }

                    //7th change
                    var collection = database.GetCollection<pdb_Organization>(collectionName);
                    collection.InsertMany((IEnumerable<pdb_Organization>)batch);

                    status=true;
                }
                else if (list is List<pdb_AS_SET>)
                {
                    status = false;
                }
                else if (list is List<pdb_AS_SET>)
                {

                }
                else if (list is List<pdb_AS_SET>)
                {

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
    }
}
