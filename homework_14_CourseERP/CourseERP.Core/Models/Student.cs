using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseERP.Core.Models
{
    public class Student
    {
        private static int _count;
        public int Id { get; set; }
        public string Fullname { get; set; }
        public double Grade { get; set; }
        public Group Group { get; set; } 
        public Student(string fullname, double grade)
        {
            Fullname = fullname;
            Grade = grade;
            Id = ++_count;
        }

        public override string ToString()
        {
            return $"{Id} - {Fullname} - Grade:{Grade} - {(Group == null ? "Group is not assigned yet" :$"studies in this group {Group.Name}")}\n";
        }

    }
}
