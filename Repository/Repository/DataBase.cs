using System.Data.SqlClient;

namespace Repository
{
    public class DataBase
    {
        //подключение к БД
        public string connectionStr;
        public void Connect(string servername, string dbname)
             => connectionStr = $"Data Source={servername};Initial Catalog={dbname};Integrated Security=True";

        //логика входа в систему
        public bool LogSystem(string username, string password)
        {
            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM Users WHERE username = @username AND password = @password", connection);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);
                SqlDataReader reader = command.ExecuteReader();
                bool isValidUser = reader.Read();

                connection.Close();
                return isValidUser;
            }
        }

        //проверка роли
        public string GetUserRole(string username)
        {
            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("SELECT role FROM Users WHERE username = @role", connection);
                command.Parameters.AddWithValue("@role", username);

                string role = (string)command.ExecuteScalar();

                connection.Close();
                return role;
            }
        }
    }
}
