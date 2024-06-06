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
    public class TeacherService : ITeacherService
    {
        public void Create(Teacher teacher)
        {
            SchoolERPDatabase<Teacher>.Persons.Add(teacher);
        }
        public List<Teacher> GetAll()
        {
            return SchoolERPDatabase<Teacher>.Persons;
        }

        public Teacher Get(int id)
        {
            Teacher? wantedTeacher = SchoolERPDatabase<Teacher>.Persons.Find(x => x.Id == id);
            if (wantedTeacher is not null)
            {
                return wantedTeacher; ;
            }
            throw new NullReferenceException("Teacher not found");
        }

        
        public void Remove(int id)
        {
            Teacher? wantedTeacher = SchoolERPDatabase<Teacher>.Persons.Find(x => x.Id == id);
            if (wantedTeacher is not null)
            {
                foreach (var student in SchoolERPDatabase<Student>.Persons)
                {
                    if (student.Teacher == wantedTeacher)
                    {
                        student.Teacher = null;
                    }
                };
                SchoolERPDatabase<Teacher>.Persons.Remove(wantedTeacher);
            }
            else
            {
                throw new NullReferenceException("Teacher not found");
            }
        }
    }
}
