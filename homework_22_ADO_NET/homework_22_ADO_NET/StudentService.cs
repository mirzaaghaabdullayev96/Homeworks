using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homework_22_ADO_NET
{
    public class StudentService
    {
        public void Create(Student student)
        {
            SQLHelper.Execute($"insert into Students values('{student.Name}','{student.Surname}',{student.Age},{student.Grant},{student.GroupId})", "Successfully added");
        }

        public Student GetDataStudentById(int id)
        {
            return SQLHelper.ReaderForOne($"select * from Students where id={id}");
        }

        public void Delete(int id)
        {
            SQLHelper.Execute($"delete from Students where Id={id}", "Successfully deleted");
        }

        public void Update(int id, string columnName, string value)
        {
            string message = "Updated successfully";
            string command = $"update Students set {columnName}={(columnName == "Grant" ? decimal.Parse(value) : $"'{value}'")} where Id={id}";
            SQLHelper.Execute(command, message);
        }

        public List<Student> GetAllStudents()
        {
            List<Student> students = new List<Student>();
            string query = "SELECT Id, Name, Surname, Age, [Grant], GroupId FROM Students";
            DataTable dataOfStudent = SQLHelper.ReadAll(query);
            foreach (DataRow row in dataOfStudent.Rows)
            {
                students.Add(new Student() { Age = Convert.ToInt16(row["Age"]), Grant = (decimal)row["Grant"], GroupId = (int)row["GroupId"], Id = (int)row["Id"], Name = (string)row["Name"], Surname = (string)row["Surname"] });
            }
            return students;            
        }

    }

}
