#region Documentation Header
// File: StartUpWindow.cs
// Author: Mikayle Coetzee (ST10023767)
// Course: PROG6212 POE 2023
// Part: 2
// Description: ...
#endregion

using System.Windows;

namespace _10023767_P2.View
{
    /// <summary>
    /// Interaction logic for StartUpWindow.xaml
    /// </summary>
    public partial class StartUpWindow : Window
    {
        /// <summary>
        /// 
        /// </summary>
        public StartUpWindow()
        {
            InitializeComponent();
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Method to update user data in the login form.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public void UpdateUserData(string username, string password)
        {
            tbxUser.Text = username;
            //pbxPassword.Password = password;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSignUp_Click_1(object sender, RoutedEventArgs e)
        {
            // Navigate to the SignUpView view
            SignUpView signUpView = new SignUpView();

            // Set the visibility of the SignUpView section to visible
            signUpView.Visibility = Visibility.Visible;

            // Navigate to the SignUpView using the MainFrame
            SignUpFrame.Navigate(content: signUpView);
        }
    }
}//★---♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫---★・。。END OF FILE 。。・★---♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫---★//
