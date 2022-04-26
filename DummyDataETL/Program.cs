// See https://aka.ms/new-console-template for more information
using DummyDataETL;
using System.Text.Json;

Console.WriteLine("Dummy Data Producer!");

List<Student> studentList = new List<Student>() { new Student { studentId=1,name="dawood", degree="BSCS",semester=8},
new Student { studentId=2,name="Shehzad Amjad", degree="BSCS",semester=8},
new Student { studentId=3,name="Yahya Amjad", degree="BSCS",semester=8}};


List<Employee> EmpList = new List<Employee>() { new Employee { EmployeeId=1,name="dawood", Dept="BSCS",salary=500},
new Employee { EmployeeId=2,name="Shehzad Amjad", Dept="BSCS",salary=700},
new Employee { EmployeeId=3,name="Yahya Amjad", Dept="BSCS",salary=900}};


var EmployeeJson= JsonSerializer.Serialize(EmpList);

var StudentJson = JsonSerializer.Serialize(studentList);

//File.WriteAllText(@"D:\Employee.json", EmpList.ToString());
//File.WriteAllText(@"D:\Student.json", studentList.ToString());

await using FileStream createStream = File.Create(@"D:\Employees.json");
await JsonSerializer.SerializeAsync(createStream, EmpList);

await using FileStream createStream1 = File.Create(@"D:\Students.json");
await JsonSerializer.SerializeAsync(createStream1, studentList);