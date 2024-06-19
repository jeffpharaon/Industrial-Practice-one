using System.Data.SqlClient;
using System.Data;

namespace Repository
{
    public class Admin
    {
        private DataBase db;
        public Admin(DataBase database) => db = database;

        //добавление студентов/учителей/дисциплин и их отображение
        public void AddStudent(string firstName, string lastName, int groupId)
        {
            using (SqlConnection connection = new SqlConnection(db.connectionStr))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO Students (firstname, lastname, groupId) VALUES (@firstname, @lastname, @groupId)", connection);
                command.Parameters.AddWithValue("@firstname", firstName);
                command.Parameters.AddWithValue("@lastname", lastName);
                command.Parameters.AddWithValue("@groupId", groupId);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void AddTeacher(string firstName, string lastName)
        {
            using (SqlConnection connection = new SqlConnection(db.connectionStr))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO Teachers (firstname, lastname) VALUES (@firstname, @lastname)", connection);
                command.Parameters.AddWithValue("@firstname", firstName);
                command.Parameters.AddWithValue("@lastname", lastName);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public SqlDataReader ViewGroups()
        {
            SqlConnection connection = new SqlConnection(db.connectionStr);
            connection.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM Groups", connection);
            return command.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public SqlDataReader ViewSubjects()
        {
            SqlConnection connection = new SqlConnection(db.connectionStr);
            connection.Open();
            SqlCommand command = new SqlCommand("SELECT subjectId, subjectname, teacherId FROM Subjects", connection);
            return command.ExecuteReader(CommandBehavior.CloseConnection);
        }

        //настройка дисциплин
        public void AddOrEditSubject(int? subjectId, string subjectName, int teacherId)
        {
            using (SqlConnection connection = new SqlConnection(db.connectionStr))
            {
                connection.Open();
                SqlCommand command;

                if (subjectId.HasValue)
                {
                    command = new SqlCommand("UPDATE Subjects SET subjectname = @subjectname, teacherId = @teacherId WHERE subjectId = @subjectId", connection);
                    command.Parameters.AddWithValue("@subjectId", subjectId.Value);
                }

                else
                {
                    command = new SqlCommand("INSERT INTO Subjects (subjectname, teacherId) VALUES (@subjectname, @teacherId)", connection);
                }

                command.Parameters.AddWithValue("@subjectname", subjectName);
                command.Parameters.AddWithValue("@teacherId", teacherId);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        //настройка групп
        public void AddOrEditGroup(int? groupId, string groupName)
        {
            using (SqlConnection connection = new SqlConnection(db.connectionStr))
            {
                connection.Open();
                SqlCommand command;

                if (groupId.HasValue)
                {
                    command = new SqlCommand("UPDATE Groups SET groupName = @groupName WHERE groupId = @groupId", connection);
                    command.Parameters.AddWithValue("@groupId", groupId.Value);
                }

                else
                {
                    command = new SqlCommand("INSERT INTO Groups (groupName) VALUES (@groupName)", connection);
                }

                command.Parameters.AddWithValue("@groupName", groupName);
                command.ExecuteNonQuery();
                connection.Close();
            }
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

        //привязка учителя к дисциплине
        public void AssignTeacherToGroup(int teacherId, int groupId)
        {
            using (SqlConnection connection = new SqlConnection(db.connectionStr))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO TeacherGroups (teacherId, groupId) VALUES (@teacherId, @groupId)", connection);
                command.Parameters.AddWithValue("@teacherId", teacherId);
                command.Parameters.AddWithValue("@groupId", groupId);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}

