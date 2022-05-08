using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DummyDataETL
{
    public class Student
    {
        public Student()
        {

        }

        public Student(string line)
        {
            var split = line.Split(',');
            studentId = Int32.Parse( split[0]);
            name = split[1];
            degree = split[2];
            semester = Int32.Parse(split[3]);
            
        }
        public int studentId { get; set; }
        public string name { get; set; }
        public string degree { get; set; }
        public int semester { get; set; }
    }
}
