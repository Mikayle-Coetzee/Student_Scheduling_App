#region Documentation Header
// File: LoginViewModel.cs
// Author: Mikayle Coetzee (ST10023767)
// Course: PROG6212 POE 2023
// Part: 2
// Description: This file contains the LoginViewModel class, responsible for user login and authentication in a
// WPF application.
#endregion

using System;
using System.Net;
using System.Security.Principal;
using System.Security;
using System.Threading;
using System.Windows.Input;
using _10023767_P2.Model;
using _10023767_P2.Repositories;

namespace _10023767_P2.ViewModel
{
    /// <summary>
    /// ViewModel for user login and authentication.
    /// </summary>
    public class LoginViewModel : ViewModelBase
    {
        /// <summary>
        /// The username for login.
        /// </summary>
        private string _username;

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// The password for login.
        /// </summary>
        private SecureString _password;

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Error message for login.
        /// </summary>
        private string _errorMessage;

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Indicates whether the view is visible.
        /// </summary>
        private bool _isViewVisible = true;

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Repository for user data.
        /// </summary>
        private IUserRepository userRepository;

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Gets or sets the username for login.
        /// </summary>
        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Gets or sets the password for login.
        /// </summary>
        public SecureString Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Gets or sets the error message for login.
        /// </summary>
        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Gets or sets a value indicating whether the view is visible.
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
        /// Command to show the password.
        /// </summary>
        public ICommand ShowPasswordCommand { get; }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Command to remember the password.
        /// </summary>
        public ICommand RememberPasswordCommand { get; }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Command to initiate the login.
        /// </summary>
        public ICommand LoginCommand { get; private set; }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Command to recover the password.
        /// </summary>
        public ICommand RecoverPasswordCommand { get; private set; }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Initializes a new instance of the LoginViewModel class.
        /// </summary>
        public LoginViewModel()
        {
            userRepository = new UserRepository();
            LoginCommand = new ViewModelCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
            RecoverPasswordCommand = new ViewModelCommand(p => ExecuteRecoverPassCommand("", ""));
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Determines whether the login command can be executed.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool CanExecuteLoginCommand(object obj)
        {
            bool validData;
            if (string.IsNullOrWhiteSpace(Username) || Username.Length < 3 ||
                Password == null || Password.Length < 3)
                validData = false;
            else
                validData = true;
            return validData;
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Executes the login command.
        /// </summary>
        /// <param name="obj"></param>
        private void ExecuteLoginCommand(object obj)
        {
            var isValidUser = userRepository.AuthenticateUser(new NetworkCredential(Username, Password));
            if (isValidUser)
            {
                Thread.CurrentPrincipal = new GenericPrincipal(
                    new GenericIdentity(Username), null);
                IsViewVisible = false;
            }
            else
            {
                ErrorMessage = "* Invalid username or password";
            }
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Executes the password recovery command.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="email"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void ExecuteRecoverPassCommand(string username, string email)
        {
            throw new NotImplementedException();
        }
    }
}//★---♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫---★・。。END OF FILE 。。・★---♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫---★//
