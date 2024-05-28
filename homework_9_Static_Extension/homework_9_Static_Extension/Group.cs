using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homework_9_Static_Extension
{
    internal class Group
    {

        public string GroupNo { get; set; }

        private int _studentLimit;

        public int StudentLimit
        {
            get { return _studentLimit; }
            private set
            {
                _studentLimit = value;
            }
        }


        private Student[] students = new Student[0];

        public Group(string GroupNo, int StudentLimit)
        {

            this.GroupNo = GroupNo;
            this.StudentLimit = StudentLimit;
        }

        public static bool CheckGroupNo(string groupNo)
        {
            if (groupNo.Length == 5)
                if (char.IsUpper(groupNo[0]) && char.IsUpper(groupNo[1]))
                    if (char.IsDigit(groupNo[2]) && char.IsDigit(groupNo[3]) && char.IsDigit(groupNo[4]))
                        return true;

            return false;
        }


        public void AddingStudent(Student student)
        {
            AddStudent(ref students, student);
        }
        private void AddStudent(ref Student[] students, Student myNewStudent)
        {
            if (students.Length < _studentLimit)
            {
                Array.Resize(ref students, students.Length + 1);
                students[students.Length - 1] = myNewStudent;
                Console.WriteLine("New student was added");
            }
            else
                Console.WriteLine("Student's limit is reached, it is impossible to add more students");
        }

        public Student? GetStudentById(int? studentId)
        {
            if (students.Length == 0)
                return null;

            foreach (Student student in students)
            {
                if (student.Id == studentId)
                    return student;
            }
            return null;
        }

        public Student[]? GetAllStudents()
        {
            if (students.Length == 0)
            {
                Console.WriteLine();
                Console.WriteLine("There is no student added yet");

                return null;
            }
            else
                return students;
        }
    }
}
