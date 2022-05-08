using DummyDataETL;
using ElasticSearchConnectivity;
using ETLServiceManagement.Models.Service;
using MongoDBConnectivity;
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
                    using ( StreamReader r = new StreamReader(file))
                    {
                        string json = r.ReadToEnd();
                        if (_service.Mapping == "student")
                        {
                            try
                            {
                                if(json!=null)
                                    studentList = JsonConvert.DeserializeObject<List<Student>>(json);
                                
                                
                            }
                            catch(Exception ex)
                            {

                            }        

                            if (_service.DestinationDb == "ElasticSeach" && studentList.Count>0)
                            {
                                ClnsertElastic cInsertElastic = new ClnsertElastic();
                                bool status = cInsertElastic.InsertBatch(studentList, _service.DbName, _service.DbUrl);
                            }
                            else if (_service.DestinationDb == "MongoDb" && studentList.Count > 0)
                            {
                                //Mongo codes goes here
                                CInsertMongo cInsertMongo = new CInsertMongo();
                                bool status = cInsertMongo.InsertBatch(studentList, _service.DbName,_service.TableName, _service.DbUrl);
                            }

                        }
                        else if (_service.Mapping == "employee")
                        {
                            try
                            {
                                if (json != null)
                                    EmployeeList = JsonConvert.DeserializeObject<List<Employee>>(json);
                            }
                            catch (Exception ex)
                            {}
                            
                            if (_service.DestinationDb == "ElasticSeach" && EmployeeList.Count > 0)
                            {
                                ClnsertElastic cInsertElastic = new ClnsertElastic();

                                bool status = cInsertElastic.InsertBatch(EmployeeList, _service.DbName, _service.DbUrl);
                            }
                            else if (_service.DestinationDb == "MongoDb" && EmployeeList.Count > 0)
                            {
                                //Mongo codes goes here
                                CInsertMongo cInsertMongo = new CInsertMongo();
                                bool status = cInsertMongo.InsertBatch(EmployeeList, _service.DbName, _service.TableName, _service.DbUrl);
                            }
                        }
                    }
                }
            }
            else if (_service.DataSource == "CSV")
            {
                foreach (string file in Directory.EnumerateFiles(_service.SourceFolder, "*.csv"))
                {
                    if (_service.Mapping == "student")
                    {
                        try
                        {
                            studentList = File.ReadLines(file).Select(line => new Student(line)).ToList();
                        }
                        catch (Exception ex)
                        { }
                    }
                    else if (_service.Mapping == "employee")
                    {
                        try
                        {
                            EmployeeList = File.ReadLines(file).Select(line => new Employee(line)).ToList();
                        }
                        catch (Exception ex)
                        { }
                    }
                }
            }
            


            

            

        }
    }
}
