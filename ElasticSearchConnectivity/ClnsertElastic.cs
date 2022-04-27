using DummyDataETL;
using Elasticsearch.Net;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace ElasticSearchConnectivity
{
    public class ClnsertElastic
    {
        public ElasticClient OpenDBConn(string defaultindex, string IP)
        {
            var connsettings = new ConnectionSettings(new Uri("https://localhost:9200"))
                .CertificateFingerprint("8388a3eda2c5fb6498375ccae44e343407e6a54f3b4ea16a90ca146c4f280c26")
                .BasicAuthentication("elastic", "psgED4PQJKD3_4CdcHAm");

            var client = new ElasticClient(connsettings);

            var settings = new IndexSettings { NumberOfReplicas = 1, NumberOfShards = 2 };
            var indexConfig = new IndexState
            {
                Settings = settings
            };
            // I am creating database. check if exist before creating the new  

            if (!client.Indices.Exists(defaultindex).Exists)
            {
                if (defaultindex == "student")
                {
                    var response = client.Indices.Create(defaultindex,
                                        index => index.Map<Student>(
                                            x => x.AutoMap()
                                        ));
                }
                else if (defaultindex == "employee")
                {
                    var response = client.Indices.Create(defaultindex,
                                        index => index.Map<Employee>(
                                            x => x.AutoMap()
                                        ));
                }
            }

            //DB or Table(Index) Selection
            connsettings.DefaultIndex(defaultindex);
            return client;
        }

        public bool InsertBatch<T>(List<T> list, string index_name, string ElasticIP)
        {
            try
            {
                
                var client = OpenDBConn(index_name, ElasticIP);

               
                if (list is List<Employee>)
                {
                    List<Employee> batch = list as List<Employee>;
                    var index = client.IndexMany(batch);
                    //Console.WriteLine(batch[0].date_time.ToString());
                    return index.ApiCall.Success;
                }
                else if (list is List<Student>)
                {
                    List<Student> batch = list as List<Student>;
                    var index = client.IndexMany(batch);
                    return index.ApiCall.Success;
                }
                
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message.ToString());
                return false;
            }
            return false;
        }
    }
}



//var cloudId = "cloudid:xxxxxxxx";
//var credentials = new BasicAuthenticationCredentials("elastic", "xxxxxxxxxxxx");
//var pool = new CloudConnectionPool(cloudId, credentials);
//var settings = new ConnectionSettings(pool)
//    .ThrowExceptions()
//    .EnableDebugMode();

//var client = new ElasticClient(settings);