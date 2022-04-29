// See https://aka.ms/new-console-template for more information
using DummyDataETL;
using MongoDBConnectivity;

Console.WriteLine("MongoDB Data Insertion!");
List<Student> studentList = new List<Student>() { new Student { studentId=1,name="dawood", degree="BSCS",semester=8},
new Student { studentId=2,name="Shehzad Amjad", degree="BSCS",semester=8},
new Student { studentId=3,name="Yahya Amjad", degree="BSCS",semester=8}};


//Dummy data Insert in Elasticsearch
CInsertMongo cInsertMongo = new CInsertMongo();

bool status = cInsertMongo.InsertBatch(studentList, "student","ETL", "https://localhost:9200/");
