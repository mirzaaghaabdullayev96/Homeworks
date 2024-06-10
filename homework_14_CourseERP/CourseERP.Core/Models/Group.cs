using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CourseERP.Core.Models
{
    public class Group
    {


        private static int _count;
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public List<Student> Students { get; set; } = [];
        public Group(string name)
        {
            Name = name;
            Id = ++_count;
            Code = Name.Substring(0, 2).ToUpper() + Id;
        }

        public override string ToString()
        {
            return $"{Id} - {Name} - Code:{Code} - this group {((Students != null && Students.Count > 0) ? "\nStudents:\n" + string.Join("\n", Students.Select(e => $"- {e.Fullname}")) : "has no students")}\n";
        }

    }
}
