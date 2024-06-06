using SchoolERP.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolERP.Business.Services.Interfaces
{
    public interface IStudentService
    {
        void Create(Student student);
        List<Student> GetAll();
        Student Get(int id);
        void Remove(int id);
    }
}
