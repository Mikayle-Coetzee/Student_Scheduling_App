#region Documentation Header
// File: SignUpModel.cs
// Author: Mikayle Coetzee (ST10023767)
// Course: PROG6212 POE 2023
// Part: 2
// Description: This file contains the SignUpModel class, which is responsible for user registration and validation. 
#endregion

using _10023767_P2.Repositories;
using System;
using System.IO;
using System.Security;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Drawing;
using System.Windows.Forms;

namespace _10023767_P2.Model
{
    /// <summary>
    /// Class representing the SignUpModel, handling user registration and validation.
    /// </summary>
    public class SignUpModel
    {
        /// <summary>
        /// Repository to interact with user data.
        /// </summary>
        private UserRepository userRepository = new UserRepository();

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Validation helper class.
        /// </summary>
        private CustomClassLibrary.ValidationClass validate = new CustomClassLibrary.ValidationClass();

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Initializes a new instance of the SignUpModel class.
        /// </summary>
        public SignUpModel() { }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Converts an ImageSource to a Bitmap object.
        /// </summary>
        /// <param name="imageSource"></param>
        /// <returns></returns>
        private System.Drawing.Bitmap ConvertImageBrushToBitmap(ImageSource imageSource)
        {
            if (imageSource is BitmapSource bitmapSource)
            {
                using (var stream = new MemoryStream())
                {
                    var encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
                    encoder.Save(stream);
                    var bitmap = new System.Drawing.Bitmap(stream);
                    return new System.Drawing.Bitmap(bitmap);
                }
            }
            return null;
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Private field to hold a user profile image.
        /// </summary>
        private Bitmap Profile;

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Validates user input for registration.
        /// </summary>
        /// <param name="username">The username to validate</param>
        /// <param name="passwordString">The password to validate</param>
        /// <param name="name">The name to validate</param>
        /// <param name="surname">The surname to validate</param>
        /// <param name="studentNumber">The student number to validate</param>
        /// <param name="email">The email to validate</param>
        /// <param name="uploadedImage">The uploaded user image to validate</param>
        /// <param name="confirmPasswordString">The password confirmation to validate.</param>
        /// <param name="confirmEmail">The email confirmation to validate</param>
        /// <returns>True if input is valid, otherwise false</returns>
        private bool ValidateStudent(string username, string passwordString,
            string name, string surname, string studentNumber,
            string email, ImageSource uploadedImage, string confirmPasswordString,
            string confirmEmail)
        {
            var validationRules = new[]
            {
                new { ControlType="TextBox", Control = "username",ErrorMessage = "Please enter a valid username." },
                new { ControlType="TextBox",Control = "name",ErrorMessage = "Please enter a valid name." },
                new { ControlType="TextBox",Control = "surname",ErrorMessage = "Please enter a valid surname." },
                new { ControlType="TextBox",Control = "studentNumber",ErrorMessage = "Please enter a valid student number." },
                new { ControlType="TextBox",Control = "email",ErrorMessage = "Please enter a valid email address." },
                new { ControlType="TextBox",Control = "confirmEmail",ErrorMessage = "Please make sure that the email and confirm email is the same." },
                new { ControlType="PasswordBox",Control = "password",ErrorMessage = "Please enter a password (min 8 characters, uppercase, lowercase, number, special character)." },
                new { ControlType="PasswordBox",Control = "confirmPassword",ErrorMessage = "Please make sure that the password and confirm password is the same." }
            };

            bool valid = true;
            foreach (var rule in validationRules)
            {
                switch (rule.ControlType)
                {
                    case "PasswordBox" when rule.Control == "confirmPassword":
                        if (confirmPasswordString != passwordString)
                        {
                            MessageBox.Show(rule.ErrorMessage);
                            valid = false;
                        }
                        break;

                    case "TextBox" when rule.Control == "confirmEmail":
                        if (confirmEmail != email)
                        {
                            MessageBox.Show(rule.ErrorMessage);
                            valid = false;
                        }
                        break;

                    case "PasswordBox":
                        if (!validate.Validate_Password(passwordString))
                        {
                            MessageBox.Show(rule.ErrorMessage);
                            valid = false;
                        }
                        break;

                    case "TextBox":
                        if (!validate.Validate_String(username) &&
                            !validate.Validate_String(surname) &&
                            !validate.Validate_String(name) &&
                            !validate.Validate_String(email))
                        {
                            MessageBox.Show(rule.ErrorMessage);
                            valid = false;
                        }
                        break;
                }
            }
            return valid;
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Registers a student with the provided information.
        /// </summary>
        /// <param name="username">The username of the student</param>
        /// <param name="password">The password of the student</param>
        /// <param name="name">The name of the student</param>
        /// <param name="surname">The surname of the student</param>
        /// <param name="studentNumber">The student number of the student</param>
        /// <param name="email">The email of the student</param>
        /// <param name="uploadedImage">The uploaded profile image of the student</param>
        /// <param name="confirmPassword">The confirmed password of the student</param>
        /// <param name="confirmEmail">The confirmed email of the student</param>
        /// <returns>True if the registration is successful, otherwise false</returns>
        public bool RegisterStudent(string username, SecureString password,
            string name, string surname, string studentNumber,
            string email, ImageSource uploadedImage, SecureString confirmPassword,
            string confirmEmail)
        {
            // Convert the SecureString to a string for storing in the database
            string passwordString = ConvertSecureStringToString(password);
            string confirmPasswordString = ConvertSecureStringToString(confirmPassword);

            bool saved = false;
            if (ValidateStudent(username, passwordString, name, surname, studentNumber, email, uploadedImage, 
                confirmPasswordString,confirmEmail))
            {
                Profile = ConvertImageBrushToBitmap(uploadedImage);
                saved = userRepository.SaveStudent(username, name, surname, studentNumber, email, passwordString,
                    Profile);
            }

            return saved;
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Converts a SecureString to a plain string.
        /// </summary>
        /// <param name="secureString">The SecureString to convert</param>
        /// <returns>The plain string representation of the SecureString</returns>
        private string ConvertSecureStringToString(SecureString secureString)
        {
            if (secureString != null)
            {
                IntPtr ptr = System.Runtime.InteropServices.Marshal.SecureStringToBSTR(secureString);
                try
                {

                    return System.Runtime.InteropServices.Marshal.PtrToStringBSTR(ptr);
                }
                finally
                {
                    System.Runtime.InteropServices.Marshal.ZeroFreeBSTR(ptr);
                }
            }

            return "";
        }
    }
}//★---♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫---★・。。END OF FILE 。。・★---♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫---★//
