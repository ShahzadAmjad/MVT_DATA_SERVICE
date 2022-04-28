using DummyDataETL;
using ElasticSearchConnectivity;
using ETLServiceManagement.Models.Service;
using Newtonsoft.Json;
//using System.Timers;

namespace ETLServiceManagement.Models
{
    public class ServiceHandler
    {
        //System.Timers.Timer timer = new Timer();
        Models.Service.Service _service { get; set; }
        public ServiceHandler(Models.Service.Service service)
        {
            _service = service;

            createService();
        }
        public void createService()
        {
            List<Student> studentList = new List<Student>();
            List<Employee> EmployeeList = new List<Employee>();

            if(_service.DataSource== "Json")
            {
                foreach (string file in Directory.EnumerateFiles(_service.SourceFolder, "*.json"))
                {
                    using ( StreamReader r = new StreamReader(_service.SourceFolder))
                    {
                        string json = r.ReadToEnd();
                        if (_service.Mapping == "student")
                        {
                            try
                            {
                                studentList = JsonConvert.DeserializeObject<List<Student>>(json);
                            }
                            catch(Exception ex)
                            {

                            }        

                            if (_service.DestinationDb == "ElasticSeach")
                            {
                                ClnsertElastic cInsertElastic = new ClnsertElastic();
                                bool status = cInsertElastic.InsertBatch(studentList, _service.DbName, _service.DbUrl);
                            }
                            else if (_service.DestinationDb == "MongoDb")
                            {
                                //Mongo codes goes here
                            }

                        }
                        else if (_service.Mapping == "employee")
                        {
                            EmployeeList = JsonConvert.DeserializeObject<List<Employee>>(json);
                            if (_service.DestinationDb == "ElasticSeach")
                            {
                                ClnsertElastic cInsertElastic = new ClnsertElastic();
                                bool status = cInsertElastic.InsertBatch(studentList, _service.DbName, _service.DbUrl);
                            }
                            else if (_service.DestinationDb == "MongoDb")
                            {
                                //Mongo codes goes here
                            }
                        }
                    }
                }
            }
            else if (_service.DataSource == "CSV")
            {
                foreach (string file in Directory.EnumerateFiles(_service.SourceFolder, "*.csv"))
                {
                    //Will be done in 2nd phase
                }
            }
            


            

            

        }
    }
}
