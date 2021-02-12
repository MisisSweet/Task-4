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
                            row[8].ToString(),
                            int.Parse(row[9].ToString())));

                Person.LoginUser = person;

                SqlCommand sqlCommand = new SqlCommand($"UPDATE Information SET LastLogin ='{DateTime.Now.ToString("dd - MM - yyyy")}' where [IDInformation]={person.Information.ID}", new SqlConnection(_connectedString));
                sqlCommand.Connection.Open();
                sqlCommand.ExecuteNonQuery();
                sqlCommand.Connection.Close();
            }
            return isLogin;
        }
        public bool AddUser(string lastname, string firstname, string email, string login, string password)
        {
            //try
            //{
                string query = $"INSERT INTO [Authorization] ([Login], [Password]) VALUES ('{login}','{password}'); select max(IDAuthorization) from [Authorization]";
                SqlDataAdapter adapter = new SqlDataAdapter(query, _connectedString);
                DataSet dst = new DataSet();
                adapter.Fill(dst);

                int Id = int.Parse(dst.Tables[0].Rows[0][0].ToString());
                SqlCommand sqlCommand = new SqlCommand($"INSERT INTO Information (LastName, FirstName, Email, DateRegistr, IDAuthorization, IDStatus) VALUES ('{lastname}','{firstname}','{email}','{DateTime.Now.ToString("dd - MM - yyyy")}',{Id},0)", new SqlConnection(_connectedString)); ;
                sqlCommand.Connection.Open();
                sqlCommand.ExecuteNonQuery();
                sqlCommand.Connection.Close();
            return true;
            //}
            //catch
            //{
             //   return false;
            //}


        }

        internal static void updateStatus(int id, int value)
        {
            string query = $"update [Information] set IDStatus = {value} where IDInformation = {id}";
            SqlCommand sqlCommand = new SqlCommand(query, new SqlConnection(_connectedString));
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            sqlCommand.Connection.Close();
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
                        row[8].ToString(),
                        int.Parse(row[9].ToString())));
                persons.Add(person);
            }

            return persons;
        }

    }
}
