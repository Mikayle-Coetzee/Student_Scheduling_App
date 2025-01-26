#region Documentation Header
// File: BindablePasswordBox.xaml.cs
// Author: Mikayle Coetzee (ST10023767)
// Course: PROG6212 POE 2023
// Part: 1
// Description: This represents a custom WPF UserControl for binding a SecureString password.
#endregion

using System.Security;
using System.Windows;
using System.Windows.Controls;

namespace _10023767_P2.Controls
{
    /// <summary>
    /// This represents a custom WPF UserControl for binding a SecureString password. Part 2
    /// </summary>
    public partial class BindablePasswordBox : UserControl
    {
        /// <summary>
        /// The DependencyProperty for binding a SecureString password.
        /// </summary>
        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register("Password", typeof(SecureString), typeof(BindablePasswordBox));

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Gets or sets the SecureString password value.
        /// </summary>
        public SecureString Password
        {
            get { return (SecureString)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Initializes a new instance of the BindablePasswordBox class.
        /// </summary>
        public BindablePasswordBox()
        {
            InitializeComponent();
            txtPassword.PasswordChanged += OnPasswordChanged;
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Event handler for the PasswordChanged event of the TextBox control.
        /// Updates the Password property with the SecureString password.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="e">The RoutedEventArgs object.</param>
        private void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            Password = txtPassword.SecurePassword;
        }
    }
}//★---♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫---★・。。END OF FILE 。。・★---♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫---★//
