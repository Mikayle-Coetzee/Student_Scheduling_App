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
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;

namespace _10023767_P2.View
{
    /// <summary>
    /// Interaction logic for MainWindowView.xaml
    /// </summary>
    public partial class MainWindowView : Window
    {
        
        public MainWindowView()
        {
            InitializeComponent();
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }

        /// <summary>
        /// This method shows or hides specific buttons based on the current student's data.
        /// </summary>
        public void ShowButtons(UserModel currentStudent, int count)
        {
            bool studentHasSemesters = currentStudent != null && count > 0;

            rbAddModule.Visibility = studentHasSemesters ? Visibility.Visible : Visibility.Collapsed;
            rbEditModule.Visibility = studentHasSemesters ? Visibility.Visible : Visibility.Collapsed;
            rbProgress.Visibility = studentHasSemesters ? Visibility.Visible : Visibility.Collapsed;
            rbCalendar.Visibility = studentHasSemesters ? Visibility.Visible : Visibility.Collapsed;
        }

        /// <summary>
        /// This method unchecks all radio buttons in the sidebar.
        /// </summary>
        private void UncheckAllRadioButtons()
        {
            StackPanel radioButtonContainer = FindName("spSideBar") as StackPanel;

            if (radioButtonContainer != null)
            {
                radioButtonContainer
                    .Children
                    .OfType<RadioButton>()
                    .ToList()
                    .ForEach(radioButton => radioButton.IsChecked = false);
            }

        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Import Windows user32.dll for window dragging.
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="wMsg"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Event handler for mouse left button down on the control bar for window dragging.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pnlControlBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowInteropHelper helper = new WindowInteropHelper(this);
            SendMessage(helper.Handle, 161, 2, 0);
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Event handler for the "Sign Out" button click.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSignOut_Click(object sender, RoutedEventArgs e)
        {
            //// Stop the music playback and cancel the time tracking thread.
            //MusicPageView.Instance.StopMusicAndReset();

            //// Redirect to the login or main window
            //MainWindow loginWindow = new MainWindow();
            //loginWindow.Show();

            // Close the current window (MainWindowView)
            this.Close();
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Event handler for the "Notification" button click.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNotification_Click(object sender, RoutedEventArgs e)
        {
            // Toggle the notification bell icon.
            this.iBell.Icon = (this.iBell.Icon == FontAwesome.Sharp.IconChar.Bell)
                ? FontAwesome.Sharp.IconChar.BellSlash
                : FontAwesome.Sharp.IconChar.Bell;

        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Event handler for the "Refresh" button click.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            // Create a new instance of MainWindowView.
            MainWindowView newWindow = new MainWindowView();

            // Show buttons based on the current student's data.
            //newWindow.ShowButtons();


            //StudentClass currentStudent = ServiceLocator.MainViewModel.CurrentStudent;

            //if (currentStudent != null)
            //{
            //    // Set the username text.
            //    newWindow.tbUsername.Text = currentStudent.Username;

            //    // Create an ImageBrush from the Profile property of currentStudent.
            //    ImageSource imageSource = Imaging.CreateBitmapSourceFromHBitmap(
            //    currentStudent.Profile.GetHbitmap(),
            //        IntPtr.Zero,
            //    System.Windows.Int32Rect.Empty,
            //     BitmapSizeOptions.FromEmptyOptions());

            //    // Set the user profile image.
            //    newWindow.iUserProfile.ImageSource = imageSource;

            //}

            // Show the new window
            newWindow.Show();


            // Close the current window
            this.Close();

        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Event handler for the "Close Sidebar" button click.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCloseSideBar_Click(object sender, RoutedEventArgs e)
        {
            //if (isSidebarOpen)
            //{
            //    CloseSidebar();
            //}
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// This method opens the sidebar with animation.
        /// </summary>
        private void OpenSidebar()
        {
            //DoubleAnimation animation = new DoubleAnimation(250, TimeSpan.FromSeconds(0.3));
            //sidebarGrid.BeginAnimation(WidthProperty, animation);
            //isSidebarOpen = true;
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// This method closes the sidebar with animation.
        /// </summary>
        private void CloseSidebar()
        {
            //DoubleAnimation animation = new DoubleAnimation(10, TimeSpan.FromSeconds(0.3));
            //sidebarGrid.BeginAnimation(WidthProperty, animation);
            //isSidebarOpen = false;
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Event handler for mouse enter the sidebar grid to open it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sidebarGrid_MouseEnter(object sender, MouseEventArgs e)
        {
            //if (!isSidebarOpen)
            //{
            //    OpenSidebar();
            //}
        }
        //・♫-------------------------------------------------------------------------------------------------♫・//
    }
}//★---♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫---★・。。END OF FILE 。。・★---♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫---★//
