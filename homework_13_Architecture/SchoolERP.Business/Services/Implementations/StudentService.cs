using SchoolERP.Business.Services.Interfaces;
using SchoolERP.Core.Models;
using SchoolERP.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolERP.Business.Services.Implementations
{
    public class StudentService : IStudentService
    {
        public void Create(Student Student)
        {
            SchoolERPDatabase<Student>.Persons.Add(Student);
        }
        public List<Student> GetAll()
        {
            return SchoolERPDatabase<Student>.Persons;
        }

        public Student Get(int id)
        {
            Student? wantedStudent = SchoolERPDatabase<Student>.Persons.Find(x => x.Id == id);
            if (wantedStudent is not null)
            {
                return wantedStudent; ;
            }
            throw new NullReferenceException("Student not found");
        }


        public void Remove(int id)
        {
            Student? wantedStudent = SchoolERPDatabase<Student>.Persons.Find(x => x.Id == id);
            if (wantedStudent is not null)
            {
                wantedStudent.Teacher.Students.Remove(wantedStudent);
                SchoolERPDatabase<Student>.Persons.Remove(wantedStudent);
            }
            else
            {
                throw new NullReferenceException("Teacher not found");
            }
        }
    }
}
