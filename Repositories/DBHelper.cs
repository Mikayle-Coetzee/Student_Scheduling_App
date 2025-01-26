using _10023767_P2.Properties;
using System;
using System.Data.SqlClient;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;

namespace _10023767_P2.Repositories
{
    public class DBHelper
    {
        /// <summary>
        /// connection string
        /// </summary>
        private readonly string ConnectionString = Settings.Default.MyAppDatabaseConnString;
        //private string ConnectionString;

        /// <summary>
        /// SQL Command
        /// </summary>
        private SqlCommand Command;

        /// <summary>
        /// SQL Connection 
        /// </summary>
        private SqlConnection Connection;

        private readonly Entities context; // Share a single context

       // public Student currentStudent ;
        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Default constructor 
        /// </summary>

        public DBHelper()
        {
            context = new Entities(); // Initialize the context in the constructor
            context.Configuration.AutoDetectChangesEnabled = true;
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Method uses to make DB connection  
        /// </summary>
        private void ConnectDb()
        {
            this.Connection = new SqlConnection();
            this.Command = new SqlCommand();
            this.Connection.ConnectionString = this.ConnectionString;
            this.Command.Connection = this.Connection;
            this.Command.CommandType = CommandType.Text;
            this.Connection.Open();

        }
        private void UpdateEntityDataModel(Entities context, string username)
        {
            // Assuming you have the student ID
            int studentID = GetStudentIDByUsername(username); // Pass the context

            try
            {
                context.Database.Initialize(true); // Reinitialize the context
                RefreshContext(context); // Refresh the entire context

                // Manually refresh the context to include the new student
                context = new Entities();

                var student = context.Students.Find(studentID);
                if (student != null)
                {
                    context.Entry(student).Reload(); // Refresh the entity
                }


                // Introduce a delay here to allow the entity to refresh
                System.Threading.Thread.Sleep(1000); // Adjust the delay time as needed
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void RefreshContext(Entities context)
        {
            var objectContext = ((IObjectContextAdapter)context).ObjectContext;
            var refreshableObjects = context.ChangeTracker.Entries().Select(c => c.Entity).ToList();
            objectContext.Refresh(RefreshMode.StoreWins, refreshableObjects);
        }


        public Student GetStudentDetails(string username)
        {
            try
            {
                // Update the Entity Framework data model first
                UpdateEntityDataModel(context, username);

                // Find the student with the provided username
                var student = context.Students.FirstOrDefault(s => s.Username == username);

                if (student != null)
                {                    
                   // this.currentStudent = student;
                    return student;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }

            return null;
        }



        public bool DoesStudentUsernameExist(string username)
        {
            try
            {
                using (var context = new Entities())
                {
                    // Find the student with the provided username
                    var student = context.Students.FirstOrDefault(s => s.Username == username);

                    if (student != null)
                    {
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }

            return false;
        }
        public bool DoesStudentPasswordExist(string password)
        {
            try
            {
                using (var context = new Entities())
                {
                    // Find the student with the provided password
                    var student = context.Students.FirstOrDefault(s => s.Password == password);

                    if (student != null)
                    {
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }

            return false;
        }
        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myListItem"></param>
        /// <returns></returns>
        //public void SaveStudent(string username, string name, string surname, string studentNum,
        //               string email, string password, Bitmap profile)
        //{
        //    ConnectDb();

        //    try
        //    {
        //        using (var sqlCmd = new SqlCommand())
        //        {
        //            sqlCmd.Connection = Connection;
        //            sqlCmd.CommandType = CommandType.Text;
        //            sqlCmd.CommandText = "INSERT INTO Students (Username, Name, Surname, StudentNumber, Email, Password, ProfileImage) " +
        //                                 "VALUES (@Username, @Name, @Surname, @StudentNumber, @Email, @Password, @ProfileImage)";


        //            sqlCmd.Parameters.AddWithValue("@Username", username);
        //            sqlCmd.Parameters.AddWithValue("@Name", name);
        //            sqlCmd.Parameters.AddWithValue("@Surname", surname);
        //            sqlCmd.Parameters.AddWithValue("@StudentNumber", studentNum);
        //            sqlCmd.Parameters.AddWithValue("@Email", email);
        //            sqlCmd.Parameters.AddWithValue("@Password", password);

        //            byte[] profileBytes;

        //            // Convert Bitmap to byte array to store in VARBINARY column
        //            using (MemoryStream stream = new MemoryStream())
        //            {
        //                profile.Save(stream, ImageFormat.Png); // Save the profile image as a PNG
        //                profileBytes = stream.ToArray();
        //            }

        //            sqlCmd.Parameters.AddWithValue("@ProfileImage", profileBytes);

        //            sqlCmd.ExecuteNonQuery();

        //            // Manually attach the new student to the context
        //            var newStudent = new Student
        //            {
        //                Username = username,
        //                Name = name,
        //                Surname = surname,
        //                StudentNumber = studentNum,
        //                Email = email,
        //                Password = password,
        //                ProfileImage = profileBytes
        //            };

        //            using (var context = new MyAppDatabasePROGEntities())
        //            {
        //                // Add the new student to the context and set its state to Added
        //                context.Students.Add(newStudent);

        //                // Save changes to the database
        //                context.SaveChanges();
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        MessageBox.Show(@"Error inserting data. Check with the database manager", @"Data Insert", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        Debug.WriteLine($"Error inserting data: {e}");
        //    }
        //    finally
        //    {
        //        Connection.Close();
        //    }
        //}


        public int GetStudentIDByUsername(string username)
        {
            int studentID = 0;

            ConnectDb();

            try
            {
                using (var sqlCmd = new SqlCommand())
                {
                    sqlCmd.Connection = Connection;
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.CommandText = "SELECT StudentID FROM Students WHERE Username = @Username";

                    sqlCmd.Parameters.AddWithValue("@Username", username);

                    var result = sqlCmd.ExecuteScalar();

                    if (result != null)
                    {
                        studentID = (int)result;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(@"Error getting data. Check with the database manager", @"Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Debug.WriteLine($"Error getting data: {e}");
            }
            finally
            {
                Connection.Close();
            }

            return studentID;
        }
        public int GetStudentIDByStudentNumber(string studentNumber)
        {
            int studentID = 0;

            ConnectDb();

            try
            {
                using (var sqlCmd = new SqlCommand())
                {
                    sqlCmd.Connection = Connection;
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.CommandText = "SELECT StudentID FROM Students WHERE StudentNumber = @StudentNumber";

                    sqlCmd.Parameters.AddWithValue("@StudentNumber", studentNumber);

                    var result = sqlCmd.ExecuteScalar();

                    if (result != null)
                    {
                        studentID = (int)result;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(@"Error getting data. Check with the database manager", @"Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Debug.WriteLine($"Error getting data: {e}");
            }
            finally
            {
                Connection.Close();
            }

            return studentID;
        }


        //・♫-------------------------------------------------------------------------------------------------♫・//

        public void SaveSemester(string semesterName, int numOfWeeks, DateTime startDate, string username)
        {
            int studentID = GetStudentIDByUsername(username);

            if (studentID != 0)
            {
                ConnectDb();

                try
                {
                    using (var sqlCmd = new SqlCommand())
                    {
                        sqlCmd.Connection = Connection;
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.CommandText = "INSERT INTO Semesters (SemesterName, NumOfWeeks, StartDate, StudentID) " +
                                             "VALUES (@SemesterName, @NumOfWeeks, @StartDate, @StudentID)";

                        sqlCmd.Parameters.AddWithValue("@SemesterName", semesterName);
                        sqlCmd.Parameters.AddWithValue("@NumOfWeeks", numOfWeeks);
                        sqlCmd.Parameters.AddWithValue("@StartDate", startDate);
                        sqlCmd.Parameters.AddWithValue("@StudentID", studentID);

                        sqlCmd.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(@"Error inserting data. Check with the database manager", @"Data Insert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Debug.WriteLine($"Error inserting data: {e}");
                }
                finally
                {
                    Connection.Close();
                }
            }
            else
            {
                MessageBox.Show("Student not found. Cannot add a semester.");
            }
        }

        //public void SaveModule(string name, TimeEntryClass classHrsPerWeek, string code, int numOfCredits, int semesterID)
        //{
        //    ConnectDb();

        //    try
        //    {
        //        using (var sqlCmd = new SqlCommand())
        //        {
        //            sqlCmd.Connection = Connection;
        //            sqlCmd.CommandType = CommandType.Text;
        //            sqlCmd.CommandText = "INSERT INTO Modules (Name, ClassHrsPerWeek_Hours, ClassHrsPerWeek_Minutes, Code, NumOfCredits, SemesterID) " +
        //                                 "VALUES (@Name, @ClassHrsPerWeek_Hours, @ClassHrsPerWeek_Minutes, @Code, @NumOfCredits, @SemesterID)";

        //            sqlCmd.Parameters.AddWithValue("@Name", name);
        //            sqlCmd.Parameters.AddWithValue("@ClassHrsPerWeek_Hours", classHrsPerWeek.Hours);
        //            sqlCmd.Parameters.AddWithValue("@ClassHrsPerWeek_Minutes", classHrsPerWeek.Minutes);
        //            sqlCmd.Parameters.AddWithValue("@Code", code);
        //            sqlCmd.Parameters.AddWithValue("@NumOfCredits", numOfCredits);
        //            sqlCmd.Parameters.AddWithValue("@SemesterID", semesterID);

        //            sqlCmd.ExecuteNonQuery();
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        MessageBox.Show(@"Error inserting data. Check with the database manager", @"Data Insert", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        Debug.WriteLine($"Error inserting data: {e}");
        //    }
        //    finally
        //    {
        //        Connection.Close();
        //    }
        //}
        public int GetModuleIDByCodeAndSemester(string moduleCode, int semesterID)
        {
            int moduleID = 0;

            ConnectDb();

            try
            {
                using (var sqlCmd = new SqlCommand())
                {
                    sqlCmd.Connection = Connection;
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.CommandText = "SELECT ModuleID FROM Modules WHERE Code = @ModuleCode AND SemesterID = @SemesterID";


                    sqlCmd.Parameters.AddWithValue("@ModuleCode", moduleCode);
                    sqlCmd.Parameters.AddWithValue("@SemesterID", semesterID);

                    var result = sqlCmd.ExecuteScalar();

                    if (result != null)
                    {
                        moduleID = (int)result;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(@"Error getting data. Check with the database manager", @"Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Debug.WriteLine($"Error getting data: {e}");
            }
            finally
            {
                Connection.Close();
            }

            return moduleID;
        }

        public void SaveStudyData(int moduleID, DateTime dayWorked, int weekWorked, int hoursWorked, int minutesWorked)
        {
            ConnectDb();

            try
            {
                using (var sqlCmd = new SqlCommand())
                {
                    sqlCmd.Connection = Connection;
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.CommandText = "INSERT INTO Study (DayWorked, WeekWorked, TimeWorked_Hours, TimeWorked_Minutes, ModuleID) " +
                                         "VALUES (@DayWorked, @WeekWorked, @HoursWorked, @MinutesWorked, @ModuleID)";

                    sqlCmd.Parameters.AddWithValue("@DayWorked", dayWorked);
                    sqlCmd.Parameters.AddWithValue("@WeekWorked", weekWorked);
                    sqlCmd.Parameters.AddWithValue("@HoursWorked", hoursWorked);
                    sqlCmd.Parameters.AddWithValue("@MinutesWorked", minutesWorked);
                    sqlCmd.Parameters.AddWithValue("@ModuleID", moduleID);

                    sqlCmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(@"Error inserting data. Check with the database manager", @"Data Insert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Debug.WriteLine($"Error inserting data: {e}");
            }
            finally
            {
                Connection.Close();
            }
        }

        public int GetSemesterIDByNameAndStudent(string semesterName, int studentID)
        {
            int semesterID = 0;

            ConnectDb();

            try
            {
                using (var sqlCmd = new SqlCommand())
                {
                    sqlCmd.Connection = Connection;
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.CommandText = "SELECT SemesterID FROM Semesters WHERE SemesterName = @SemesterName AND StudentID = @StudentID";

                    sqlCmd.Parameters.AddWithValue("@SemesterName", semesterName);
                    sqlCmd.Parameters.AddWithValue("@StudentID", studentID);

                    var result = sqlCmd.ExecuteScalar();

                    if (result != null)
                    {
                        semesterID = (int)result;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(@"Error getting data. Check with the database manager", @"Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Debug.WriteLine($"Error getting data: {e}");
            }
            finally
            {
                Connection.Close();
            }

            return semesterID;
        }

    }
}//★---♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫---★・。。END OF FILE 。。・★---♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫---★//