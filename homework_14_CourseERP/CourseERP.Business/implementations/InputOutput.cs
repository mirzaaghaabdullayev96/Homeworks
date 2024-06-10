using CourseERP.Business.interfaces;
using CourseERP.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ClassLibraryMyHelper;

namespace CourseERP.Business.implementations
{
    public static class InputOutput
    {

        public static void MainChoices()
        {
            Console.WriteLine("""
                1. Group Operations
                2. Student Operations
                3. Add student to group
                0. Exit
                """);
            Console.WriteLine();
        }

        public static void GroupChoices()
        {
            Console.WriteLine("What would you like to do with groups?\n");
            Console.WriteLine("""
                1. Create
                2. Get
                3. Get all
                4. Remove
                5. Exit to main page
                """);
            Console.WriteLine();
        }

        public static void StudentChoices()
        {
            Console.WriteLine("What would you like to do with students?\n");
            Console.WriteLine("""
                1. Create
                2. Get
                3. Get all
                4. Remove
                5. Exit to main page
                """);
            Console.WriteLine();
        }


        public static void CreateGroup()
        {
            IGroupService groupService = new GroupService();
            Console.WriteLine("Enter name of the group");
            string name = Console.ReadLine();
            Console.WriteLine();
            while (name.Length < 3)
            {
                Console.WriteLine("Group name must contain at least 2 letters. Please, enter valid name.");
            }
            Console.WriteLine();
            groupService.Create(new Group(name));
        }

        public static void RemoveGroup()
        {
            ShowAllGroupsInfo();
            Console.WriteLine("Which Group would you like to remove? Write ID");
            int id = 0;
            while (!MyHelper.IdCheck(id))
            {
                try
                {
                    Console.WriteLine("ID should contain only numbers and must be more than 0. For quiting press 7");
                    id = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine();
                    if (id == 7)
                        return;
                }
                catch (Exception)
                {


                }

            }
            IGroupService groupService = new GroupService();
            Console.WriteLine($"{groupService.Get(id).Name} was deleted successfully\n");
            groupService.Remove(id);
        }

        public static void ShowAllGroupsInfo()
        {
            IGroupService groupService = new GroupService();
            if (groupService.GetAll().Count > 0)
            {
                foreach (Group group in groupService.GetAll())
                {
                    Console.WriteLine(group);
                }
            }
            else Console.WriteLine("No group has been created yet\n");
        }


        public static void CreateStudent()
        {
            IStudentService studentService = new StudentService();
            Console.WriteLine("Enter name of the student");
            string name = Console.ReadLine();
            while (!MyHelper.NameChecker(name))
            {
                Console.WriteLine("Enter name properly, you may use only letters and max length is 10. For quiting press Q");
                name = Console.ReadLine();
                Console.WriteLine();
                if (name == "Q".ToLower())
                    return;
            }
            Console.WriteLine("Enter grade of the student");
            double grade = 0;
            while (!MyHelper.GradeChecker(grade))
            {
                try
                {
                    Console.WriteLine("Grade must be between 1 and 100. Must contain only digits. For quiting press 7");
                    grade = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine();
                    if (grade==7)
                        return;
                }
                catch (Exception)
                {

                    if (grade.ToString() == "Q".ToLower())
                        return;
                }

            }
            Console.WriteLine();
            studentService.Create(new Student(name, grade));
        }

        public static void RemoveStudent()
        {
            ShowAllStudentsInfo();
            Console.WriteLine("Which student would you like to remove? Write ID");
            int id = 0;
            while (!MyHelper.IdCheck(id))
            {
                try
                {
                    Console.WriteLine("ID should contain only numbers and must be more than 0. For quiting press 7");
                    id = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine();
                    if (id==7)
                        return;
                }
                catch (Exception)
                {


                }

            }
            Console.WriteLine();
            IStudentService studentService = new StudentService();
            Console.WriteLine($"{studentService.Get(id).Fullname} was deleted successfully\n");
            studentService.Remove(id);
        }

        public static void ShowAllStudentsInfo()
        {
            IStudentService studentService = new StudentService();
            if ((studentService.GetAll().Count > 0))
            {
                foreach (Student student in studentService.GetAll())
                {
                    Console.WriteLine(student);
                }
            }
            else Console.WriteLine("No student has been created yet");
        }
        public static void ShowAllStudentsWithoutGroupInfo()
        {
            IStudentService studentService = new StudentService();
            if ((studentService.GetAll().Count > 0))
            {
                foreach (Student student in studentService.GetAll())
                {
                    if (student.Group is null)
                        Console.WriteLine(student);
                }
            }
            else Console.WriteLine("No student has been created yet");
        }

        public static void AddStudentToGroup()
        {
            IGroupService groupService = new GroupService();
            IStudentService studentService = new StudentService();
            //check if group or students exist
            if (studentService.GetAll().Count == 0 && groupService.GetAll().Count == 0)
            {
                Console.WriteLine("No student nor group has been created yet, You need to create student first.\n");
                return;
            }
            if (studentService.GetAll().Count == 0)
            {
                Console.WriteLine("No student has been created yet, You need to create student first.\n");
                return;
            }
            if (groupService.GetAll().Count == 0)
            {
                Console.WriteLine("No group has been created yet, You need to create group first.\n");
                return;
            }

            Console.WriteLine("Which student would you like to add?");
            ShowAllStudentsWithoutGroupInfo();
            int studentChoice = -1;
            while (!MyHelper.ChoiceCheck(studentChoice))
            {
                try
                {
                    Console.WriteLine("Choice should contain only numbers and must be equal or more than 0. For quiting press 7");
                    studentChoice = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine();
                    if (studentChoice == 7)
                        return;
                }
                catch (Exception)
                {


                }

            }
            //checking if we have student by this ID
            try
            {
                if (studentService.GetAll().FirstOrDefault(std => std.Id == studentChoice) == null)
                {
                    throw new StudentNotFoundException("Student by this ID was not found\n");
                }
            }
            catch (StudentNotFoundException ex)
            {

                Console.WriteLine(ex.Message);
                return;
            }
            Console.WriteLine();
            Console.WriteLine("To which group would you like to add?");
            ShowAllGroupsInfo();
            int groupChoice = -1;
           
            while (!MyHelper.ChoiceCheck(groupChoice))
            {
                try
                {
                    Console.WriteLine("Choice should contain only numbers and must be equal or more than 0. For quiting press 7");
                    groupChoice = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine();
                        if (groupChoice == 7)
                            return;
                }
                catch (Exception)
                {


                }

            }
            //checking if we have group by this ID
            try
            {
                if (groupService.GetAll().FirstOrDefault(grp => grp.Id == groupChoice) == null)
                    throw new GroupNotFoundException("Group by this ID was not found\n");
            }
            catch (GroupNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            Console.WriteLine();
            groupService.Get(groupChoice).Students.Add(studentService.Get(studentChoice));
            studentService.Get(studentChoice).Group = groupService.Get(groupChoice);
            Console.WriteLine($"{studentService.Get(studentChoice).Fullname} was added successfully to group {groupService.Get(groupChoice).Code}\n");


        }
    }

}


