using System.Data;
using System.Data.SqlClient;

namespace Repository
{
    public class Teacher
    {
        private DataBase db;
        public Teacher(DataBase database) => db = database;

        //отробразить группы
        public SqlDataReader ViewGroups()
        {
            SqlConnection connection = new SqlConnection(db.connectionStr);
            connection.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM Groups", connection);
            return command.ExecuteReader(CommandBehavior.CloseConnection);
        }

        //отобразить дисциплины
        public SqlDataReader ViewSubjects(int teacherId)
        {
            SqlConnection connection = new SqlConnection(db.connectionStr);
            connection.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM Subjects WHERE teacherId = @teacherId", connection);
            command.Parameters.AddWithValue("@teacherId", teacherId);
            return command.ExecuteReader(CommandBehavior.CloseConnection);
        }

        //изменить оценки
        public void EditGrade(int gradeId, int grade)
        {
            using (SqlConnection connection = new SqlConnection(db.connectionStr))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("UPDATE Grades SET grade = @grade WHERE gradeId = @gradeId", connection);
                command.Parameters.AddWithValue("@gradeId", gradeId);
                command.Parameters.AddWithValue("@grade", grade);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}

