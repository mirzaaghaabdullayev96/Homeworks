using CourseERP.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseERP.Business.interfaces
{
    public interface IGroupService
    {
        void Create(Group group);
        void Remove(int id);
        Group Get(int id);
        List<Group> GetAll();
    }
}

