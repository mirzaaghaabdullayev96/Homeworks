using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolERP.Core.Models
{
    public class Student:BaseModel
    {
        private static int _counter;
        public int TeacherId { get; set; }
        public string FullName { get; set; }
        public double Grade { get; set; }
        public Teacher Teacher { get; set; }
        public Student()
        {
            Id = ++_counter;
        }

        public override string ToString()
        {
            string teacher = Teacher == null ? "not assigned yet" : Teacher.FullName;
            return $"{Id} - {FullName} - {Grade} - and his teacher is {teacher}";
        }
    }
}
