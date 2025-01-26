#region Documentation Header
// File: SignUpViewModel.cs
// Author: Mikayle Coetzee (ST10023767)
// Course: PROG6212 POE 2023
// Part: 2
// Description: This file contains the SignUpViewModel class, which is a ViewModel for signing up users in a WPF application.
// It handles user registration, input validation, and image uploading.
#endregion

using _10023767_P2.Model;
using System;
using System.Windows.Input;
using System.Windows.Media;
using System.ComponentModel;
using _10023767_P2.Repositories;
using System.Windows;
using System.Security;
using System.Runtime.CompilerServices;
using Microsoft.Win32;
using System.Windows.Media.Imaging;

namespace _10023767_P2.ViewModel
{
    /// <summary>
    /// ViewModel for user registration.
    /// </summary>
    public class SignUpViewModel : ViewModelBase, INotifyPropertyChanged
    {
        /// <summary>
        /// Holds the SignUpModel instance.
        /// </summary>
        private SignUpModel signUpModel;

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Holds the UserRepository instance.
        /// </summary>
        private UserRepository userRepository;

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Holds the user's password as a SecureString.
        /// </summary>
        private SecureString securePassword;

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Tracks the visibility of the view.
        /// </summary>
        private bool _isViewVisible = true;

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Gets or sets the visibility status of the view.
        /// </summary>
        public bool IsViewVisible
        {
            get
            {
                return _isViewVisible;
            }
            set
            {
                _isViewVisible = value;
                OnPropertyChanged(nameof(IsViewVisible));
            }
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// This initializes a new instance of the SignUpViewModel class. It initializes the model and necessary
        /// commands for user sign-up.
        /// </summary>
        public SignUpViewModel()
        {
            signUpModel = new SignUpModel();
            userRepository = new UserRepository();
            UserModel = new UserModel();
            SignUpCommand = new ViewModelCommand(ExecuteSignUpCommand, CanExecuteSignUpCommand);
            UploadImageCommand = new ViewModelCommand(ExecuteUploadImageCommand);
            ClearCommand = new ViewModelCommand(ExecuteClearCommand);
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// New event handler for property changed notifications.
        /// </summary>
        public new event PropertyChangedEventHandler PropertyChanged;

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Invokes property change notifications for the view.
        /// </summary>
        /// <param name="propertyName">The name of the property that changed.</param>
        protected new void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Gets or sets the user model associated with the sign-up process.
        /// </summary>
        public UserModel UserModel { get; set; }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Gets or sets the user's username. When set, it triggers property change notification.
        /// </summary>
        public string Username
        {
            get { return UserModel.Username; }
            set
            {
                if (UserModel.Username != value)
                {
                    UserModel.Username = value;
                    OnPropertyChanged();
                }
            }
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Gets or sets the user's password. When set, it triggers property change notification.
        /// </summary>
        public SecureString Password
        {
            get { return UserModel.Password; }
            set
            {
                if (UserModel.Password != value)
                {
                    UserModel.Password = value;
                    OnPropertyChanged();
                }
            }
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Holds the user's confirm password as a SecureString.
        /// </summary>
        private SecureString confirmPassword;

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Gets or sets the user's confirm password. When set, it triggers property change notification.
        /// </summary>
        public SecureString ConfirmPassword
        {
            get { return confirmPassword; }
            set
            {
                if (confirmPassword != value)
                {
                    confirmPassword = value;
                    OnPropertyChanged();
                }
            }
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Gets or sets the user's name. When set, it triggers property change notification.
        /// </summary>
        public string Name
        {
            get { return UserModel.Name; }
            set
            {
                if (UserModel.Name != value)
                {
                    UserModel.Name = value;
                    OnPropertyChanged();
                }
            }
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Gets or sets the user's surname. When set, it triggers property change notification.
        /// </summary>
        public string Surname
        {
            get { return UserModel.Surname; }
            set
            {
                if (UserModel.Surname != value)
                {
                    UserModel.Surname = value;
                    OnPropertyChanged();
                }
            }
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Gets or sets the user's student number. When set, it triggers property change notification.
        /// </summary>
        public string StudentNumber
        {
            get { return UserModel.StudentNumber; }
            set
            {
                if (UserModel.StudentNumber != value)
                {
                    UserModel.StudentNumber = value;
                    OnPropertyChanged();
                }
            }
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Gets or sets the user's email. When set, it triggers property change notification.
        /// </summary>
        public string Email
        {
            get { return UserModel.Email; }
            set
            {
                if (UserModel.Email != value)
                {
                    UserModel.Email = value;
                    OnPropertyChanged();
                }
            }
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Holds the user's confirm email.
        /// </summary>
        private string confirmEmail;

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Gets or sets the user's confirm email. When set, it triggers property change notification.
        /// </summary>
        public string ConfirmEmail
        {
            get { return confirmEmail; }
            set
            {
                if (confirmEmail != value)
                {
                    confirmEmail = value;
                    OnPropertyChanged();
                }
            }
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Holds the user's uploaded image source.
        /// </summary>
        private ImageSource uploadedImageSource;

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Gets or sets the user's uploaded image source. When set, it triggers property change notification.
        /// </summary>
        public ImageSource UploadedImageSource
        {
            get { return uploadedImageSource; }
            set
            {
                if (uploadedImageSource != value)
                {
                    uploadedImageSource = value;
                    OnPropertyChanged();
                }
            }
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Gets the command for signing up the user. It's used to execute the registration process.
        /// </summary>
        public ICommand SignUpCommand { get; private set; }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Gets the command for uploading an image for the user's profile. It opens a file dialog for image
        /// selection.
        /// </summary>
        public ICommand UploadImageCommand { get; set; }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Gets the command for clearing all user input fields. It resets the form.
        /// </summary>
        public ICommand ClearCommand { get; private set; }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Determines whether the ClearCommand can be executed. Always returns true.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool CanExecuteClearCommand(object obj)
        {
            return true;
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Executes the ClearCommand, clearing all user input fields.
        /// </summary>
        /// <param name="obj"></param>
        private void ExecuteClearCommand(object obj)
        {
            ClearUserInput();
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Determines whether the SignUpCommand can be executed. Always returns true.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool CanExecuteSignUpCommand(object obj)
        {
            return true;
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Converts a plain text string to a SecureString.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
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

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Executes the SignUpCommand, attempting to register the user with the provided information. 
        /// It checks if registration is successful and provides feedback to the user.
        /// </summary>
        /// <param name="obj"></param>
        private void ExecuteSignUpCommand(object obj)
        {
            var isRegistered = signUpModel.RegisterStudent(
                UserModel.Username, UserModel.Password, UserModel.Name,
                UserModel.Surname, UserModel.StudentNumber, UserModel.Email,
                UploadedImageSource, confirmPassword, confirmEmail);

            if (isRegistered)
            {
                // Clear user input and hide the view
                ClearUserInput();
                HideView();
                MessageBox.Show("User registered successfully.");
            }
            else
            {
                MessageBox.Show("Failed to register. Please check your data and try again.");
            }
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Executes the UploadImageCommand, allowing the user to select and display an image for their profile.
        /// </summary>
        /// <param name="obj"></param>
        private void ExecuteUploadImageCommand(object obj)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files | *.jpg; *.jpeg; *.png; *.bmp";

            if (openFileDialog.ShowDialog() == true)
            {
                string imagePath = openFileDialog.FileName;

                try
                {
                    Uri imageUri = new Uri(imagePath, UriKind.RelativeOrAbsolute);
                    BitmapImage image = new BitmapImage(imageUri);
                    UploadedImageSource = image;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading the selected image: " + ex.Message);
                }
            }
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Clears all user input fields 
        /// </summary>
        private void ClearUserInput()
        {
            // Clear all user input fields
            Username = string.Empty;
            Password = new SecureString();
            ConfirmPassword = new SecureString();
            Name = string.Empty;
            Surname = string.Empty;
            StudentNumber = string.Empty;
            Email = string.Empty;
            ConfirmEmail = string.Empty;
            UploadedImageSource = null;
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Hides the view by setting IsViewVisible to false.
        /// </summary>
        private void HideView()
        {
            IsViewVisible = false;
        }
    }
}//★---♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫---★・。。END OF FILE 。。・★---♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫---★//
