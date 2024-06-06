using SchoolERP.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolERP.Business.Services.Interfaces
{
    public interface ITeacherService
    {
        void Create(Teacher teacher);
        List<Teacher> GetAll();
        Teacher Get(int id);
        void Remove(int id);

    }
}
