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
    public class GroupService : IGroupService
    {
        public void Create(Group group)
        {
            Data<Group>.Course.Add(group);
            Console.WriteLine($"Group by name {group.Name} and code {group.Code} was created successfully!");
            Console.WriteLine();
        }

        public Group Get(int id)
        {
            try
            {
                var wantedGroup = Data<Group>.Course.Find(x => x.Id == id);

                if (wantedGroup is null)
                {
                    throw new NullReferenceException("Group not found\n");
                }
               


                return wantedGroup;
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine(ex.Message);
                return null;

            }
            
        }

        public List<Group> GetAll()
        {
            return Data<Group>.Course;
        }

        public void Remove(int id)
        {
            IStudentService studentService= new StudentService();
            var wantedGroup = Data<Group>.Course.Find(x => x.Id == id);
            foreach (var student in studentService.GetAll())
            {
                if (student.Group == wantedGroup)
                    student.Group = null;
            }
            Data<Group>.Course.Remove(wantedGroup);
        }
    }
}
