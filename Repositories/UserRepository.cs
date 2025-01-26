#region Documentation Header
// File: SignUpViewModel.cs
// Author: Mikayle Coetzee (ST10023767)
// Course: PROG6212 POE 2023
// Part: 2
// Description: This file contains the SignUpViewModel class, which is a ViewModel for signing up users in a WPF application.
// It handles user registration, input validation, and image uploading.
#endregion

using System.Data.SqlClient;
using System.Data;
using System.Net;
using _10023767_P2.Model;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Windows;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Security;

namespace _10023767_P2.Repositories
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public bool Add(UserModel userModel)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// SQL Command
        /// </summary>
        private SqlCommand command;

        /// <summary>
        /// SQL Connection 
        /// </summary>
        private SqlConnection connection;

        private readonly Entities context;
        public UserRepository()
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


        public bool AuthenticateUser(NetworkCredential credential)
        {
            return (DoesStudentUsernameExist(credential.UserName) &&
                    DoesStudentPasswordExist(credential.Password));
        }

        public bool Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public bool Edit(UserModel userModel)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<UserModel> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public UserModel GetById(int id)
        {
            throw new System.NotImplementedException();
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
        private void RefreshContext(Entities context)
        {
            var objectContext = ((IObjectContextAdapter)context).ObjectContext;
            var refreshableObjects = context.ChangeTracker.Entries().Select(c => c.Entity).ToList();
            objectContext.Refresh(RefreshMode.StoreWins, refreshableObjects);
        }
        private SecureString ConvertToSecureString(string input)
        {
            SecureString secureString = new SecureString();
            if (!string.IsNullOrEmpty(input))
            {
                foreach (char c in input)
                {
                    secureString.AppendChar(c);
                }
            }
            return secureString;
        }

        public UserModel GetByUsername(string username)
        {
            UserModel user = null;

            try
            {
                // Update the Entity Framework data model first
                UpdateEntityDataModel(context, username);

                // Find the student with the provided username
                var student = context.Students.FirstOrDefault(s => s.Username == username);

                if (student != null)
                {
                    // this.currentStudent = student;
                    SecureString pass = ConvertToSecureString(student.Password);
                     user = new UserModel()
                     {
                         Username = student.Username,
                         Name = student.Name,
                         Surname = student.Surname,
                         StudentNumber = student.StudentNumber,
                         Email = student.Email,
                         Password = pass,
                         Profile = student.ProfileImage
                     };
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }

            return user;
        }
        public bool SaveStudent(string username, string name, string surname, string studentNum,
                       string email, string password, Bitmap profile)
        {
            bool saved = false;
            ConnectDb();

            try
            {
                using (var sqlCmd = new SqlCommand())
                {
                    sqlCmd.Connection = connection;
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.CommandText = "INSERT INTO Students (Username, Name, Surname, StudentNumber, Email, Password, ProfileImage) " +
                                         "VALUES (@Username, @Name, @Surname, @StudentNumber, @Email, @Password, @ProfileImage)";


                    sqlCmd.Parameters.AddWithValue("@Username", username);
                    sqlCmd.Parameters.AddWithValue("@Name", name);
                    sqlCmd.Parameters.AddWithValue("@Surname", surname);
                    sqlCmd.Parameters.AddWithValue("@StudentNumber", studentNum);
                    sqlCmd.Parameters.AddWithValue("@Email", email);
                    sqlCmd.Parameters.AddWithValue("@Password", password);

                    byte[] profileBytes;

                    // Convert Bitmap to byte array to store in VARBINARY column
                    using (MemoryStream stream = new MemoryStream())
                    {
                        profile.Save(stream, ImageFormat.Png); // Save the profile image as a PNG
                        profileBytes = stream.ToArray();
                    }

                    sqlCmd.Parameters.AddWithValue("@ProfileImage", profileBytes);

                    //string pass = password.ToString();

                    sqlCmd.ExecuteNonQuery();

                    // Manually attach the new student to the context
                    var newStudent = new Student
                    {
                        Username = username,
                        Name = name,
                        Surname = surname,
                        StudentNumber = studentNum,
                        Email = email,
                        Password = password,
                        ProfileImage = profileBytes
                    };

                    using (var context = new Entities())
                    {
                        // Add the new student to the context and set its state to Added
                        context.Students.Add(newStudent);

                        // Save changes to the database
                        context.SaveChanges();
                    }
                }
                saved = true;
            }
            catch (Exception e)
            {
                MessageBox.Show(@"Error inserting data. Check with the database manager", @"Data Insert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Debug.WriteLine($"Error inserting data: {e}");
                saved = false;
            }
            finally
            {
                connection.Close();
                
            }
            return saved;
        }


    }
}