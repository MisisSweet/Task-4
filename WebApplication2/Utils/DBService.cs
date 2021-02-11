using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.Utils
{
    public class DBService
    {
        private const string _connectedString = @"Data Source=DESKTOP-IUQN2RP\SQLEXPRESS;Initial Catalog=Users;Integrated Security=True";
        public static DBService Instanse;

        SqlConnection Connection;

        public DBService()
        {
            Connection = new SqlConnection(_connectedString);
            Connection.Open();
            Connection.Close();
        }

        public bool Login(string login, string password)
        {
            SqlDataAdapter adapter = new SqlDataAdapter($"select * from dbo.[View_Information] where Login = '{login}' and Password='{password}'", _connectedString);

            DataSet dst = new DataSet();
            adapter.Fill(dst);
            bool isLogin = dst.Tables[0].Rows.Count > 0;

            if (isLogin)
            {
                DataRow row = dst.Tables[0].Rows[0];

                Person person = new Person(
                        int.Parse(row[0].ToString()),
                        row[1].ToString(),
                        row[2].ToString(),
                        new Information(
                            int.Parse(row[3].ToString()),
                            row[4].ToString(),
                            row[5].ToString(),
                            row[6].ToString(),
                            DateTime.Parse(row[7].ToString()),
                            DateTime.Parse(row[8].ToString()),
                            int.Parse(row[9].ToString())));

                Person.LoginUser = person;
            }
            return isLogin;
        }

        //TODO доделать получение
        public List<Person> GetUsers()
        {
            SqlDataAdapter adapter = new SqlDataAdapter($"select * from dbo.[View_Information]", _connectedString);

            DataSet dst = new DataSet();
            adapter.Fill(dst);
            List<Person> persons = new List<Person>();

            foreach(DataRow row in dst.Tables[0].Rows)
            {
                Person person = new Person(
                    int.Parse(row[0].ToString()),
                    row[1].ToString(),
                    row[2].ToString(),
                    new Information(
                        int.Parse(row[3].ToString()),
                        row[4].ToString(),
                        row[5].ToString(),
                        row[6].ToString(),
                        DateTime.Parse(row[7].ToString()),
                        DateTime.Parse(row[8].ToString()),
                        int.Parse(row[9].ToString())));
                persons.Add(person);
            }

            return persons;
        }

    }
}
