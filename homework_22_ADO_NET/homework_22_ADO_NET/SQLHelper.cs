using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homework_22_ADO_NET
{
    public static class SQLHelper
    {
        const string sqlPath = "Server=MIRZA-PC-OMEN\\SQLEXPRESS;Database=AcademyADO_NET;Trusted_Connection=True;TrustServerCertificate=True";
        static SqlConnection sqlConnection = new SqlConnection(sqlPath);

        public static void Execute(string command, string message)
        {
            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand(command, sqlConnection);
            int check = cmd.ExecuteNonQuery();
            Console.WriteLine($"{(check == 1 ? message : "Error")}");
            sqlConnection.Close();
        }

        public static Student ReaderForOne(string command)
        {
            sqlConnection.Open();
            Student student = null;
            SqlCommand cmd = new SqlCommand(command, sqlConnection);
            SqlDataReader datas = cmd.ExecuteReader();
            if (datas.Read())
            {
                student = new Student() { Id = (int)datas["Id"], Name = (string)datas["Name"], Surname = (string)datas["Surname"], Age = Convert.ToInt16(datas["Age"]), Grant = (decimal)datas["Grant"], GroupId = (int)datas["GroupId"] };
            }
            sqlConnection.Close();
            return student;
        }

        public static DataTable ReadAll(string command)
        {
            sqlConnection.Open();
            DataTable dataTable = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(command, sqlConnection))
            {
                adapter.Fill(dataTable);
            }

            sqlConnection.Close();
            return dataTable;
        }




    }
}
