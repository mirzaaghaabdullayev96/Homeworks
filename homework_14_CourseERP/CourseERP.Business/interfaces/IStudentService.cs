using CourseERP.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseERP.Business.interfaces
{
    public interface IStudentService
    {
        void Create(Student student);
        void Remove(int id);
        Student Get(int id);
        List<Student> GetAll();
    }
}
