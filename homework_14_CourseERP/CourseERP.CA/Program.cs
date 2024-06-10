using CourseERP.Business.implementations;
using CourseERP.Business.interfaces;
using CourseERP.Core.Models;
using CourseERP.Database.DAL;
using System.Xml.Linq;
using ClassLibraryMyHelper;

namespace CourseERP.CA
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IGroupService groupService = new GroupService();
            IStudentService studentService = new StudentService();
            bool flag = true;
        Returning:
            while (flag)
            {

                InputOutput.MainChoices();

                Console.WriteLine("Select operation number by writing it");
                int choice = -1;
                NewChoice:
                while (!MyHelper.ChoiceCheck(choice))
                {
                    try
                    {
                        Console.WriteLine("ID should contain only numbers and must be more than 0. For quiting press 7");
                        choice = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine();
                        if (choice == 7)
                            goto Returning;
                    }
                    catch (Exception)
                    {
                        goto NewChoice;

                    }

                }
                Console.WriteLine();
                switch (choice)
                {
                    case 1:
                        InputOutput.GroupChoices();
                        Console.WriteLine("Select operation number by writing it");
                        int groupChoice = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine();
                        switch (groupChoice)
                        {
                            case 1:
                                InputOutput.CreateGroup();
                                break;
                            case 2:
                                Console.WriteLine("Write ID of the group");
                                int idGroup = 0;
                                while (!MyHelper.IdCheck(idGroup))
                                {
                                    try
                                    {
                                        Console.WriteLine("ID should contain only numbers and must be more than 0. For quiting press 7");
                                        idGroup = Convert.ToInt32(Console.ReadLine());
                                        Console.WriteLine();
                                        if (idGroup == 7)
                                            return;
                                    }
                                    catch (Exception)
                                    {


                                    }

                                }
                                Console.WriteLine();
                                Console.WriteLine(groupService.Get(idGroup));
                                break;
                            case 3:
                                InputOutput.ShowAllGroupsInfo();
                                break;
                            case 4:
                                InputOutput.RemoveGroup();
                                break;
                            case 5:
                                break;
                            default:
                                Console.WriteLine("No such operation for Group\n");
                                break;
                        }
                        break;
                    case 2:
                        InputOutput.StudentChoices();
                        Console.WriteLine("Select operation number by writing it");
                        int studentChoice = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine();
                        switch (studentChoice)
                        {
                            case 1:
                                InputOutput.CreateStudent();
                                break;
                            case 2:
                                Console.WriteLine("Write ID of the group");
                                int idStudent = 0;
                                while (!MyHelper.IdCheck(idStudent))
                                {
                                    try
                                    {
                                        Console.WriteLine("ID should contain only numbers and must be more than 0. For quiting press 7");
                                        idStudent = Convert.ToInt32(Console.ReadLine());
                                        Console.WriteLine();
                                        if (idStudent == 7)
                                            return;
                                    }
                                    catch (Exception)
                                    {


                                    }

                                }
                                Console.WriteLine();
                                Console.WriteLine(studentService.Get(idStudent));
                                break;
                            case 3:
                                InputOutput.ShowAllStudentsInfo();
                                break;
                            case 4:
                                InputOutput.RemoveStudent();
                                break;
                            case 5:
                                break;
                            default:
                                Console.WriteLine("No such operation for students\n");
                                break;
                        }
                        break;
                    case 3:
                        InputOutput.AddStudentToGroup();
                        break;
                    case 0:
                        Console.WriteLine("Program is closing...Bye!");
                        Thread.Sleep(2000);
                        flag = false;
                        break;
                    default:
                        Console.WriteLine("Your number must be 1,2,3,0 or 7\n");
                        break;
                }
            }

        }

    }
}
