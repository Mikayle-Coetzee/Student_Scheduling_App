#region Documentation Header
// File: SignUpViewModel.cs
// Author: Mikayle Coetzee (ST10023767)
// Course: PROG6212 POE 2023
// Part: 2
// Description: This file contains the SignUpViewModel class, which is a ViewModel for signing up users in a WPF application.
// It handles user registration, input validation, and image uploading.
#endregion

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace _10023767_P2.Model
{
    public class UserModel
    {
        /// <summary>
        /// Gets or sets the username of the student.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the first name of the student.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the last name of the student.
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// Gets or sets the student number.
        /// </summary>
        public string StudentNumber { get; set; }

        /// <summary>
        /// Gets or sets the email address of the student.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password of the student.
        /// </summary>
        public SecureString Password { get; set; }

        /// <summary>
        /// Gets or sets the profile of the student
        /// </summary>
        public byte[] Profile { get; set; } //was Bitmap

        /// <summary>
        /// Gets or sets the collection of semesters associated with the student.
        /// </summary>
        public ObservableCollection<Semester> Semesters { get; set; } = new ObservableCollection<Semester>();
    }
}
