using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Repository;

namespace PPThemeOne
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //инициализация БД
        private DataBase db = new DataBase();
        private Admin admin;
        private Teacher teacher;
        private Student student;

        public MainWindow()
        {
            InitializeComponent(); 
            InitializeSettings();
        }

        private void InitializeSettings()
        {
            //настройки отображения UI
            AdminGrid.Visibility = Visibility.Hidden;
            TeacherGird.Visibility = Visibility.Hidden;
            StudentGrid.Visibility = Visibility.Hidden;
            SystemInfo.Visibility = Visibility.Hidden;

            //настройки БД
            db.Connect("JEFFPHARAON\\SQLEXPRESS", "Prakta");
            admin = new Admin(db);
            teacher = new Teacher(db);
            student = new Student(db);
        }

        //реализация входа в систему
        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            if (db.LogSystem(username, password))
            {
                string role = db.GetUserRole(username);

                if (role == "admin")
                    AdminGrid.Visibility = Visibility.Visible;

                else if (role == "teacher")
                    TeacherGird.Visibility = Visibility.Visible;

                else if (role == "student")
                    StudentGrid.Visibility = Visibility.Visible;
            }

            else SystemInfo.Visibility = Visibility.Visible;
        }

        //добавить студента
        private void AddStudentButton_Click(object sender, RoutedEventArgs e)
        {
            string firstName = StudentFirstNameTextBox.Text;
            string lastName = StudentLastNameTextBox.Text;
            int groupId = int.Parse(StudentGroupIDTextBox.Text);
            admin.AddStudent(firstName, lastName, groupId);
            MessageBox.Show("Student added successfully.");
        }

        //добавить учителя
        private void AddTeacherButton_Click(object sender, RoutedEventArgs e)
        {
            string firstName = TeacherFirstNameTextBox.Text;
            string lastName = TeacherLastNameTextBox.Text;
            admin.AddTeacher(firstName, lastName);
            MessageBox.Show("Teacher added successfully.");
        }

        //отобразить группы
        private void ViewGroupsButton_Click(object sender, RoutedEventArgs e)
        {
            GroupsListBox.Items.Clear();
            var reader = admin.ViewGroups();
            while (reader.Read())
                GroupsListBox.Items.Add($"{reader["GroupID"]}: {reader["GroupName"]}");
            reader.Close();
        }

        //отобразить субъект
        private void ViewSubjectsButton_Click(object sender, RoutedEventArgs e)
        {
            SubjectsListBox.Items.Clear();
            var reader = admin.ViewSubjects();
            while (reader.Read())
                SubjectsListBox.Items.Add($"{reader["SubjectId"]}: {reader["SubjectName"]}: {reader["TeacherID"]})");
            reader.Close();
        }

        //настройки группы
        private void AddEditGroupButton_Click(object sender, RoutedEventArgs e)
        {
            string groupName = GroupNameTextBox.Text;
            int? groupId = null;
            if (GroupsListBox.SelectedItem != null)
                groupId = int.Parse(GroupsListBox.SelectedItem.ToString().Split(':')[0]);
            admin.AddOrEditGroup(groupId, groupName);
        }

        //настройки субъектов
        private void AddEditSubjectButton_Click(object sender, RoutedEventArgs e)
        {
            string subjectName = SubjectNameTextBox.Text;
            int teacherId = int.Parse(SubjectTeacherIDTextBox.Text);
            int? subjectId = null;
            if (SubjectsListBox.SelectedItem != null)
                subjectId = int.Parse(SubjectsListBox.SelectedItem.ToString().Split(':')[0]);
            admin.AddOrEditSubject(subjectId, subjectName, teacherId);
        }

        //изменить оценки
        private void EditGradeButton_Click(object sender, RoutedEventArgs e)
        {
            int gradeId = int.Parse(GradeIDTextBox.Text);
            int grade = int.Parse(GradeTextBox.Text);
            admin.EditGrade(gradeId, grade);
        }

        //назначить учителя
        private void AssignTeacherButton_Click(object sender, RoutedEventArgs e)
        {
            int teacherId = int.Parse(AssignTeacherIDTextBox.Text);
            int groupId = int.Parse(AssignGroupIDTextBox.Text);
            admin.AssignTeacherToGroup(teacherId, groupId);
        }

        //скрытие UI
        private void RemovePlaceholderText(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && textBox.Foreground == Brushes.Gray)
            {
                textBox.Text = string.Empty;
                textBox.Foreground = Brushes.Black;
            }
        }

        //отображение UI
        private void AddPlaceholderText(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Foreground = Brushes.Gray;
                if (textBox.Name == "StudentFirstNameTextBox") textBox.Text = "First Name";
                else if (textBox.Name == "StudentLastNameTextBox") textBox.Text = "Last Name";
                else if (textBox.Name == "StudentGroupIDTextBox") textBox.Text = "Group ID";
                else if (textBox.Name == "TeacherFirstNameTextBox") textBox.Text = "First Name";
                else if (textBox.Name == "TeacherLastNameTextBox") textBox.Text = "Last Name";
                else if (textBox.Name == "GroupNameTextBox") textBox.Text = "Group Name";
                else if (textBox.Name == "SubjectNameTextBox") textBox.Text = "Subject Name";
                else if (textBox.Name == "SubjectTeacherIDTextBox") textBox.Text = "Teacher ID";
                else if (textBox.Name == "GradeIDTextBox") textBox.Text = "Grade ID";
                else if (textBox.Name == "GradeTextBox") textBox.Text = "Grade";
                else if (textBox.Name == "AssignTeacherIDTextBox") textBox.Text = "Teacher ID";
                else if (textBox.Name == "AssignGroupIDTextBox") textBox.Text = "Group ID";
            }
        }

        //отобразить группы
        private void ViewGroupButton_Click(object sender, RoutedEventArgs e)
        {
            GroupListBox.Items.Clear();
            var reader = teacher.ViewGroups();
            while (reader.Read())
            {
                GroupListBox.Items.Add($"{reader["groupId"]}: {reader["groupName"]}");
            }
            reader.Close();
        }

        //отобразить субъекты
        private void ViewSubjectButton_Click(object sender, RoutedEventArgs e)
        {
            SubjectListBox.Items.Clear();
            int teacherId = int.Parse(TeacherIdTextBox.Text);
            var reader = teacher.ViewSubjects(teacherId);
            while (reader.Read())
            {
                SubjectListBox.Items.Add($"{reader["subjectId"]}: {reader["subjectname"]} (Teacher ID: {reader["teacherId"]})");
            }
            reader.Close();
        }

        //изменить оценки
        private void EditGradesButton_Click(object sender, RoutedEventArgs e)
        {
            int gradeId = int.Parse(GradeIdTextBox.Text);
            int grade = int.Parse(GradesTextBox.Text);
            teacher.EditGrade(gradeId, grade);
            MessageBox.Show("Grade edited successfully.");
        }

        //скрытие UI 
        private void RemovePlaceholdersText(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && textBox.Foreground == Brushes.Gray)
            {
                textBox.Text = string.Empty;
                textBox.Foreground = Brushes.Black;
            }
        }

        //отображение UI
        private void AddPlaceholdersText(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Foreground = Brushes.Gray;
                if (textBox.Name == "TeacherIdTextBox")
                {
                    textBox.Text = "Teacher ID";
                }
                else if (textBox.Name == "GradeIdTextBox")
                {
                    textBox.Text = "Grade ID";
                }
                else if (textBox.Name == "GradeTextBox")
                {
                    textBox.Text = "Grade";
                }
            }
        }

        //отображение оценок
        private void ViewGradesButton_Click(object sender, RoutedEventArgs e)
        {
            GradesListBox.Items.Clear();
            int studentId = int.Parse(StudentIdTextBox.Text);
            var reader = student.ViewGrades(studentId);
            while (reader.Read())
            {
                GradesListBox.Items.Add($"{reader["subjectname"]}: {reader["grade"]}");
            }
            reader.Close();
        }

        //скрытие UI
        private void RemovePlaceholdeText(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && textBox.Foreground == Brushes.Gray)
            {
                textBox.Text = string.Empty;
                textBox.Foreground = Brushes.Black;
            }
        }

        //отображение UI
        private void AddPlaceholdeText(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Foreground = Brushes.Gray;
                if (textBox.Name == "StudentIdTextBox")
                {
                    textBox.Text = "Student ID";
                }
            }
        }
    }
}
