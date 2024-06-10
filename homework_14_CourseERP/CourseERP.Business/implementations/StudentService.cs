using CourseERP.Business.interfaces;
using CourseERP.Core.Models;
using CourseERP.Database.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseERP.Business.implementations
{
    public class StudentService : IStudentService
    {
        public void Create(Student student)
        {
            Data<Student>.Course.Add(student);
            Console.WriteLine($"Student by name {student.Fullname} and grade {student.Grade} was created successfully!");
            Console.WriteLine();
        }

        public Student Get(int id)
        {
            try
            {
                var wantedStudent = Data<Student>.Course.Find(x => x.Id == id);

                if (wantedStudent is null)
                {
                    throw new NullReferenceException("Student not found");
                }


                return wantedStudent;
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }

        public List<Student> GetAll()
        {
            return Data<Student>.Course;
        }

        public void Remove(int id)
        {
            try
            {
                IGroupService groupService = new GroupService();
                var wantedStudent = Data<Student>.Course.Find(x => x.Id == id);
                if (wantedStudent is not null)
                {
                    foreach (Group grp in groupService.GetAll())
                    {
                        if (grp.Students.Contains(wantedStudent))
                            grp.Students.Remove(wantedStudent);
                    }
                    Data<Student>.Course.Remove(wantedStudent);
                }
                else
                {
                    throw new NullReferenceException("Teacher not found");
                }
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
