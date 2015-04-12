using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement
{
    class Manage : TakeInput
    {
        //Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\StudentDB.mdf;Integrated Security=True
        public String connectionString = "Data Source=NIHARIKA;Initial Catalog=StudentDB;Integrated Security=True";
        //public String connectionString = StudentManagement.Properties.Settings.Default.StudentDBConnectionString;

        /**********************************************************************************
         *                                Main menu                                       *
         *********************************************************************************/
        public void main_menu()
        {
            // Main Menu
            Console.WriteLine("\nEnter your command:"
            + "\n\t1 > Manage student"
            + "\n\t2 > Manage course"
            + "\n\t3 > Manage registration"
            + "\n\t0 > Exit program"
            );

            // To the next level of menu
            int number = user_choice();
            bool condition = true;
            while(condition)
            {
                switch (number)
                {
                    case 0:
                        Console.WriteLine();
                        condition = false;
                        break;
                    case 1:
                        // Student menu complete
                        student_menu();
                        break;
                    case 2:
                        course_menu();
                        break;
                    case 3:
                        registration_menu();
                        break;
                }

            }
        }


        /**********************************************************************************
         *                                Student menu                                    *
         *********************************************************************************/
        private void student_menu()
        {
            Console.WriteLine("\n[1] Managing students:" 
                + "\n\t1 > New student" 
                //+ "\n\t2 > Edit student (Does not work)" 
                + "\n\t3 > Delete student" 
                + "\n\t4 > Print student list"
                + "\n\t0 > Back to main menu"
                );

            Action new_student = () =>
            {
                // Take user input

                String StudentID = prompt("Enter new student ID");
                String StudentName = prompt("Enter new student name");

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    String sql = "INSERT INTO Students "
                        + "(StudentID,StudentName)"
                        + "VALUES (@StudentID, @StudentName)";

                    SqlCommand command = new SqlCommand(sql, connection);

                    SqlParameter param1 = new SqlParameter("@StudentID", SqlDbType.NChar, 10);
                    param1.Value = StudentID;
                    command.Parameters.Add(param1);

                    SqlParameter param2 = new SqlParameter("@StudentName", SqlDbType.NChar, 50);
                    param2.Value = StudentName;
                    command.Parameters.Add(param2);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        Console.WriteLine("New student information added");
                        connection.Close();
                        return;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Connection problem");
                    }
                }
            };
            /*
            Action edit_student = () =>
            {
                // Take user input
                String StudentID = prompt("Enter student ID");
                
                Console.WriteLine("Are you sure to overwrite?? ");
                if ("N" == Console.ReadLine() || "n" == Console.ReadLine())
                {
                    Console.WriteLine("Action cancelled.");
                    return;
                }
                
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    String StudentName = prompt("New Name");
                    String sql = "Update Students "
                        + "SET StudentName = @StudentName"
                        + "WHERE StudentID = @StudentID";

                    SqlCommand command = new SqlCommand(sql, connection);

                    SqlParameter param1 = new SqlParameter("@StudentName", SqlDbType.NChar, 50);
                    param1.Value = StudentID;
                    command.Parameters.Add(param1);
                    
                    SqlParameter param2 = new SqlParameter("@StudentID", SqlDbType.NChar, 10);
                    param2.Value = StudentID;
                    command.Parameters.Add(param2);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        Console.WriteLine("Student information deleted.");
                        connection.Close();
                        Console.ReadLine();
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Connection problem");
                    }
                }
            };
        */
            Action delete_student = () =>
            {
                // Take user input
                String StudentID = prompt("Enter student ID");

                Console.WriteLine("Are you sure to delete? ") ;
                if("N" == Console.ReadLine() || "n" == Console.ReadLine()){
                    Console.WriteLine("Action cancelled.");
                    return;
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    String sql = "DELETE FROM Students "
                        + "WHERE StudentID = @StudentID";

                    SqlCommand command = new SqlCommand(sql, connection);

                    SqlParameter param1 = new SqlParameter("@StudentID", SqlDbType.NChar, 10);
                    param1.Value = StudentID;
                    command.Parameters.Add(param1);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        Console.WriteLine("Student information deleted.");
                        connection.Close();
                        return;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Connection problem");
                    }
                }
            };

            Action show_student = () =>
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    String sql = "select * from Students";
                    SqlCommand command = new SqlCommand(sql, connection);

                    try
                    {
                        connection.Open();
                        Console.WriteLine("Student list: ");
                        SqlDataReader reader = command.ExecuteReader();
                        Console.WriteLine("\n\tStudent ID\tStudent Name");
                        while (reader.Read())
                        {
                            Console.WriteLine("\t" + reader["StudentID"].ToString() + "\t" + reader["StudentName"].ToString());
                            
                        }
                        connection.Close();
                        return;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Connection problem");
                    }
                }
            };
               

            // To the next level of menu
            int number = user_choice();
            switch (number)
            {
                case 0:
                    main_menu();
                    break;
                case 1:
                    new_student();
                    break;
            /*
                case 2:
                    edit_student();
                    break;

             */
                case 3:
                    delete_student();
                    break;
                case 4:
                    show_student();
                    break;
            }
        }

        /**********************************************************************************
         *                                Courses menu                                    *
         * *******************************************************************************/
        private void course_menu()
        {
            Console.WriteLine("\n[2] Managing courses:" 
                + "\n\t1 > New course" 
                //+ "\n\t2 > Edit course (Does not work)" 
                + "\n\t3 > Delete course" 
                + "\n\t4 > Print course list"
                + "\n\t0 > Back to main meny"
                );

            Action new_course = () =>
            {
                // Take user input
                String CourseID = prompt("Enter new course ID");
                String CourseName = prompt("Enter new course name");

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    String sql = "INSERT INTO Courses "
                        + "(CourseID,CourseName)"
                        + "VALUES (@CourseID, @CourseName)";

                    SqlCommand command = new SqlCommand(sql, connection);

                    SqlParameter param1 = new SqlParameter("@CourseID", SqlDbType.NChar, 10);
                    param1.Value = CourseID;
                    command.Parameters.Add(param1);

                    SqlParameter param2 = new SqlParameter("@CourseName", SqlDbType.NChar, 50);
                    param2.Value = CourseName;
                    command.Parameters.Add(param2);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        Console.WriteLine("New course information added");
                        connection.Close();
                        return;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Connection problem");
                    }
                }
            };
            /*
            Action edit_course = () =>
            {
                // Take user input
                String CourseID = prompt("Enter course ID");
                
                Console.WriteLine("Are you sure to overwrite?? ");
                if ("N" == Console.ReadLine() || "n" == Console.ReadLine())
                {
                    Console.WriteLine("Action cancelled.");
                    return;
                }
                
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    String CourseName = prompt("New Name");
                    String sql = "Update Courses "
                        + "SET CourseName = @CourseName"
                        + "WHERE CourseID = @CourseID";

                    SqlCommand command = new SqlCommand(sql, connection);

                    SqlParameter param1 = new SqlParameter("@CourseName", SqlDbType.NChar, 50);
                    param1.Value = CourseID;
                    command.Parameters.Add(param1);
                    
                    SqlParameter param2 = new SqlParameter("@CourseID", SqlDbType.NChar, 10);
                    param2.Value = CourseID;
                    command.Parameters.Add(param2);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        Console.WriteLine("Course information deleted.");
                        connection.Close();
                        Console.ReadLine();
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Connection problem");
                    }
                }
            };
        */
            Action delete_course = () =>
            {
                // Take user input
                String CourseID = prompt("Enter course ID");

                Console.WriteLine("Are you sure to delete? ") ;
                if("N" == Console.ReadLine() || "n" == Console.ReadLine()){
                    Console.WriteLine("Action cancelled.");
                    return;
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    String sql = "DELETE FROM Courses "
                        + "WHERE CourseID = @CourseID";

                    SqlCommand command = new SqlCommand(sql, connection);

                    SqlParameter param1 = new SqlParameter("@CourseID", SqlDbType.NChar, 10);
                    param1.Value = CourseID;
                    command.Parameters.Add(param1);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        Console.WriteLine("Course information deleted.");
                        connection.Close();
                        return;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Connection problem");
                    }
                }
            };


            Action show_course = () =>
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    String sql = "select * from Courses";
                    SqlCommand command = new SqlCommand(sql, connection);

                    try
                    {
                        connection.Open();
                        Console.WriteLine("Course list: ");
                        SqlDataReader reader = command.ExecuteReader();
                        Console.WriteLine("\n\tCourse ID\tCourse Name");
                        while (reader.Read())
                        {
                            Console.WriteLine("\t" + reader["CourseID"].ToString() + "\t" + reader["CourseName"].ToString());

                        }
                        connection.Close();
                        return;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Connection problem");
                    }
                }
            };

            // To the next level of menu
            int number = user_choice();
            switch (number)
            {
                case 0:
                    main_menu();
                    break;
                case 1:
                    new_course();
                    break;
            /*
                case 2:
                {
                    edit_course();
                    break;
                }
             */
                case 3:
                    delete_course();
                    break;
                case 4:
                    show_course();
                    break;
            }
        }

        private void registration_menu()
        {
            throw new NotImplementedException();
        }
    }
}
