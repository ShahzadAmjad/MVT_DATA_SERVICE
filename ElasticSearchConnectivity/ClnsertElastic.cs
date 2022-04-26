using DummyDataETL;
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
        public ElasticClient OpenDBConn(string defaultindex)
        {
            string IP = "https://localhost:9200";
            var node = new Uri(IP);
            var settings = new ConnectionSettings(node);
            // I like exceptions
            settings.ThrowExceptions(alwaysThrow: true);
            // Good for DEBUG
            settings.PrettyJson();
            var client = new ElasticClient(settings);
            //DB or Table Selection
            settings.DefaultIndex(defaultindex);
            return client;
        }

        public bool InsertBatch<T>(List<T> list, string index_name)
        {
            try
            {
                
                var client = OpenDBConn(index_name);
              

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
