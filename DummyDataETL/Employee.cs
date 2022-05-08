using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DummyDataETL
{
    public class Employee
    {
        public Employee()
        {

        }

        public Employee(string line)
        {          
           var split = line.Split(',');
           EmployeeId = Int32.Parse(split[0]);
           name = split[1];
           Dept = split[2];
           salary = Int32.Parse(split[3]);          
        }
        public int EmployeeId { get; set; }
        
        public string name { get; set; }
        public string Dept { get; set; }
        public int salary { get; set; }
    }
}
