using System.Threading.Channels;

namespace homework_9_Static_Extension
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region creating user first part

            //creating user
            Console.WriteLine("Creating user");
            Console.WriteLine("Please, enter your name");
            string name = Console.ReadLine();
            Console.WriteLine("Please, enter your email");
            string email = Console.ReadLine();
            Console.WriteLine("Please, enter your password");
            string pass = Console.ReadLine();
            User user1 = new(email, name, pass);
            Console.WriteLine();

            user1.ShowInfo();
            Console.WriteLine();

            #endregion





            #region creating group and students second part
            Console.WriteLine("Let's create new group and add students");
            Console.WriteLine("===Creating new group===\n");

            string group = "";
            int limit = 0;
            while (true)
            {
                Console.WriteLine("Enter group number. It must start with two upper letters and then 3 digits");
                group = Console.ReadLine();
                if (Group.CheckGroupNo(group))
                    break;
            }
            while (true)
            {
                Console.WriteLine("Enter students limit. It must be between 5 and 18");
                limit = Convert.ToInt32(Console.ReadLine());
                if (limit <= 18 && limit >= 5)
                    break;
            }

            Group myGroup = new Group(group, limit);
            bool flag = true;
            while (flag)
            {
                Console.WriteLine();
                Console.WriteLine("1.Show all student\n2.Get student by Id\n3.Add student\n0.Quit");
                Console.WriteLine();
                int newChoice = Convert.ToInt32(Console.ReadLine());
                switch (newChoice)
                {
                    case 1:
                        var students = myGroup.GetAllStudents();
                        if (students == null)
                            break;
                        foreach (var student in students)
                            student.StudentInfo();
                        break;
                    case 2:
                        Console.WriteLine("Enter Id of student");
                        int idOfStudent = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine();
                        if (myGroup.GetStudentById(idOfStudent) != null)
                            myGroup.GetStudentById(idOfStudent).StudentInfo();
                        else
                            Console.WriteLine("Not found");
                        break;
                    case 3:
                        int pointOfStudent = 0;
                        Console.WriteLine("==Creating new student==");
                        Console.WriteLine("Enter fullname of student");
                        string studentFullName = Console.ReadLine();
                        while (true)
                        {
                            Console.WriteLine("Enter point of student. It must be between 1 and 100");
                            pointOfStudent = Convert.ToInt32(Console.ReadLine());
                            if (pointOfStudent > 0 && pointOfStudent <= 100)
                            {
                                Console.WriteLine();
                                break;
                            }

                        }

                        myGroup.AddingStudent(new(studentFullName, pointOfStudent));

                        break;
                    case 0:
                        Console.WriteLine("Quiting");
                        flag = false;
                        break;
                    default:
                        Console.WriteLine("No such choice");
                        break;
                }

            }

            #endregion





        }
    }
}
