using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolERP.Core.Models
{
    public class Teacher : BaseModel
    {
        private static int _counter;
        public string FullName { get; set; }
        public double Salary { get; set; }
        public List<Student> Students { get; set; } = [];

        public Teacher()
        {
            Id = ++_counter;
        }
        public override string ToString()
        {
            return $"{Id} - {FullName} - {Salary}";
        }
    }
}
