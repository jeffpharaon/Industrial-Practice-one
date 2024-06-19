using System.Data.SqlClient;
using System.Data;

namespace Repository
{
    public class Student
    {
        private DataBase db;
        public Student(DataBase database) => db = database;

        //отобразить оценки
        public SqlDataReader ViewGrades(int studentId)
        {
            SqlConnection connection = new SqlConnection(db.connectionStr);
            connection.Open();
            SqlCommand command = new SqlCommand(
                "SELECT s.subjectname, g.grade " +
                "FROM Grades g " +
                "JOIN Subjects s ON g.subjectId = s.subjectId " +
                "WHERE g.studentId = @studentId", connection);
            command.Parameters.AddWithValue("@studentId", studentId);
            return command.ExecuteReader(CommandBehavior.CloseConnection);
        }
    }
}

