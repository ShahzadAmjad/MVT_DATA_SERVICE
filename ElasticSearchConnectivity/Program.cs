// See https://aka.ms/new-console-template for more information
using DummyDataETL;
using ElasticSearchConnectivity;

Console.WriteLine("Elasticsearch Insertion API");

List<Student> studentList = new List<Student>() { new Student { studentId=1,name="dawood", degree="BSCS",semester=8},
new Student { studentId=2,name="Shehzad Amjad", degree="BSCS",semester=8},
new Student { studentId=3,name="Yahya Amjad", degree="BSCS",semester=8}};


//Dummy data Insert in Elasticsearch
ClnsertElastic cInsertElastic = new ClnsertElastic();

bool status = cInsertElastic.InsertBatch(studentList, "student", "https://localhost:9200/");

