#region Documentation Header
// File: SignUpView.xaml.cs
// Author: Mikayle Coetzee (ST10023767)
// Course: PROG6212 POE 2023
// Part: 2
// Description: This class represents the SignUp view user control.
#endregion

using _10023767_P2.Repositories;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace _10023767_P2.View
{
    /// <summary>
    /// Interaction logic for SignUpView.xaml.cs
    /// </summary>
    public partial class SignUpView : UserControl
    {
        /// <summary>
        /// Default constructor for SignUpView.
        /// </summary>
        public SignUpView()
        {
            InitializeComponent();
            if (this.iProfile.ImageSource == null)
            {
                this.iProfile.ImageSource = (ImageSource)this.FindResource("AnonymousImage");
            }
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Event handler for the Back button click event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Collapsed;
        }
    }
}//★---♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫---★・。。END OF FILE 。。・★---♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫---★//
