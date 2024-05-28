using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homework_9_Static_Extension
{
    internal class Student
    {
        private static int counter;
        public int Id { get; }
        public string Fullname { get; set; }
        private int _point;
        public int Point
        {
            get => _point; 
            set
            {
                if(value<100 && value>0)
                    _point = value;

            }
        }

        public void StudentInfo()
        {
            Console.WriteLine($"ID - {Id}, Fullname - {Fullname}, Point - {Point}");
        }

        static Student()
        {
            counter = 0;
        }

        public Student(string name, int point)
        {
            counter++;
            Id = counter;
            Fullname = name;
            Point = point;
        }





    }
}
