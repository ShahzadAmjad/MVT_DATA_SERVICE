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
