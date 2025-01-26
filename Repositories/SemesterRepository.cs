using _10023767_P2.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;

namespace _10023767_P2.Repositories
{
    public class SemesterRepository : RepositoryBase
    {

        /// <summary>
        /// SQL Command
        /// </summary>
        private SqlCommand command;

        /// <summary>
        /// SQL Connection 
        /// </summary>
        private SqlConnection connection;

        private readonly Entities context;
        public SemesterRepository()
        {
            context = new Entities(); // Initialize the context in the constructor
            context.Configuration.AutoDetectChangesEnabled = true;
        }

        private void ConnectDb()
        {
            this.connection = GetSqlConnection();
            this.command = new SqlCommand();
            this.connection.Open();
            this.command.Connection = this.connection;
            this.command.CommandType = CommandType.Text;
        }
        public int GetStudentIDByUsername(string username)
        {
            int studentID = 0;

            ConnectDb();

            try
            {
                using (var sqlCmd = new SqlCommand())
                {
                    sqlCmd.Connection = connection;
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
                System.Windows.MessageBox.Show(@"Error getting data. Check with the database manager", @"Data Error", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Error);
                Debug.WriteLine($"Error getting data: {e}");
            }
            finally
            {
                connection.Close();
            }

            return studentID;
        }
        public bool SaveSemester(string semesterName, int numOfWeeks, DateTime startDate, string username)
        {
            bool saved = false;
            int studentID = GetStudentIDByUsername(username);

            if (studentID != 0)
            {
                ConnectDb();

                try
                {
                    using (var sqlCmd = new SqlCommand())
                    {
                        sqlCmd.Connection = connection;
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
                    System.Windows.MessageBox.Show(
                        @"Error inserting data. Check with the database manager", @"Data Insert");
                    Debug.WriteLine($"Error inserting data: {e}");
                    saved = false;
                }
                finally
                {
                    connection.Close();
                }
                saved = true;
            }
            else
            {
                System.Windows.MessageBox.Show("Student not found. Cannot add a semester.");
                saved = false;
            }

            return saved;
        }

        public int GetSemesterIDByNameAndStudent(string semesterName, int studentID)
        {
            int semesterID = 0;

            ConnectDb();

            try
            {
                using (var sqlCmd = new SqlCommand())
                {
                    sqlCmd.Connection = connection;
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
                System.Windows.MessageBox.Show(@"Error getting data. Check with the database manager", @"Data Error");
                Debug.WriteLine($"Error getting data: {e}");
            }
            finally
            {
                connection.Close();
            }

            return semesterID;
        }

        public List<Semester> GetSemestersForCurrentStudent(string username)
        {
            int studentID = GetStudentIDByUsername(username);

            if (studentID == 0)
            {
                return new List<Semester>();
            }

            ConnectDb();

            try
            {
                using (var sqlCmd = new SqlCommand())
                {
                    sqlCmd.Connection = connection;
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.CommandText = "SELECT * FROM Semesters WHERE StudentID = @StudentID";

                    sqlCmd.Parameters.AddWithValue("@StudentID", studentID);

                    var semesters = new List<Semester>();
                    using (var reader = sqlCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var semester = new Semester
                            {
                                SemesterName = reader["SemesterName"].ToString(),
                                NumOfWeeks = (int)reader["NumOfWeeks"],
                                StartDate = (DateTime)reader["StartDate"]
                                // Add other properties as needed
                            };
                            semesters.Add(semester);
                        }
                    }

                    return semesters;
                }
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("Error getting data. Check with the database manager", "Data Error");
                Debug.WriteLine($"Error getting data: {e}");
            }
            finally
            {
                connection.Close();
            }

            return new List<Semester>();
        }

    }
}
