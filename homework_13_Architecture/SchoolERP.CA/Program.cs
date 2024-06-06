using SchoolERP.Business.Services.Implementations;
using SchoolERP.Business.Services.Interfaces;
using SchoolERP.Core.Models;

namespace SchoolERP.CA
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ITeacherService teacherService = new TeacherService();
            IStudentService studentService = new StudentService();
            teacherService.Create(new Teacher() { FullName = "Eli Eliyev", Salary = 2600 });
            var teacher = teacherService.Get(1);

            studentService.Create(new Student() { FullName = "Abbas", Grade = 13, Teacher = teacher });
            var studentAbbas = studentService.Get(1);
            studentService.Create(new Student() { FullName = "Ali", Grade = 23, Teacher = teacher });
            var studentAli= studentService.Get(2);
            studentService.Create(new Student() { FullName = "Terlan", Grade = 54, Teacher = teacher });
            var studentTerlan = studentService.Get(3);
            studentService.Create(new Student() { FullName = "Nuray", Grade = 67, Teacher = teacher });
            var studentNuray = studentService.Get(4);
            studentService.Create(new Student() { FullName = "Ceyhun", Grade = 89, Teacher = teacher });
            var studentCeyhun = studentService.Get(5);
            var studentsOfEli = new List<Student>() { studentAbbas, studentAli, studentTerlan, studentNuray, studentCeyhun };

            teacher.Students.AddRange(studentsOfEli);
            //before removing
            foreach (Student student in teacher.Students)
                Console.WriteLine(student);

            studentService.Remove(3);
            //after removing from the list

            foreach(Student student in teacher.Students)
                Console.WriteLine(student);

            



            //removing teacher and checking
            //try
            //{
            //    Console.WriteLine(teacher);
            //    Console.WriteLine(studentAbbas);

            //    teacherService.Remove(1);

            //    Console.WriteLine("==========after removing teacher=========");
            //    Console.WriteLine(studentAbbas);
            //    Console.WriteLine(teacherService.Get(1));
            //}
            //catch (NullReferenceException ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}








        }
    }
}
