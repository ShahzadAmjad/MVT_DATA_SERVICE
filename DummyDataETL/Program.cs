// See https://aka.ms/new-console-template for more information
using DummyDataETL;
using System.ComponentModel;
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


//For Jason data Generaor
//await using FileStream createStream = File.Create(@"G:\Employees.json");
//await JsonSerializer.SerializeAsync(createStream, EmpList);

//await using FileStream createStream1 = File.Create(@"G:\Students.json");
//await JsonSerializer.SerializeAsync(createStream1, studentList);

//For CSV

string CSVFilePathEMP = @"G:\EmployeesCSV.csv";
SaveToCsv(EmpList, CSVFilePathEMP);

string CSVFilePathSTD = @"G:\StudentsSCV.csv";
SaveToCsv(studentList, CSVFilePathSTD);


void SaveToCsv<T>(List<T> ObjList, string path)
{
    var lines = new List<string>();
    IEnumerable<PropertyDescriptor> props = TypeDescriptor.GetProperties(typeof(T)).OfType<PropertyDescriptor>();
    var header = string.Join(",", props.ToList().Select(x => x.Name));
    lines.Add(header);
    var valueLines = ObjList.Select(row => string.Join(",", header.Split(',').Select(a => row.GetType().GetProperty(a).GetValue(row, null))));
    lines.AddRange(valueLines);
    File.WriteAllLines(path, lines.ToArray());
}