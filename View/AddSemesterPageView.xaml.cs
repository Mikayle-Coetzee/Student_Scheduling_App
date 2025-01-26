#region Documentation Header
// File: AddSemesterPageView.xaml.cs
// Author: Mikayle Coetzee (ST10023767)
// Course: PROG6212 POE 2023
// Part: 1
// Description: This class represents the AddSemesterPageView user control, which allows users to add semesters.
#endregion

using _10023767_P2;
using _10023767_P2.Model;
using _10023767_P2.Repositories;
using _10023767_P2.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace _10023767_P2.View
{
    /// <summary>
    /// Interaction logic for AddSemesterPageView.xaml
    /// </summary>
    public partial class AddSemesterPageView : UserControl
    {

        /// <summary>
        /// Instance of the ValidationClass for input validation.
        /// </summary>
        private CustomClassLibrary.ValidationClass validate = new CustomClassLibrary.ValidationClass();

        private SemesterModel semesterModel = new SemesterModel();
        private SemesterRepository semesterRepository = new SemesterRepository();
        /// <summary>
        /// Instance of the WorkerClass for adding semesters.
        /// </summary>
        //private WorkerClass workerClass = new WorkerClass();

        /// <summary>
        /// Original border brush used for error message clearing.
        /// </summary>
        private SolidColorBrush originalBrush = new SolidColorBrush();

        //private DBHelper dbHelper = new DBHelper();
       // private DBHelper DBHelper = new DBHelper();

        //private string Username;
        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Constructor for the AddSemesterPageView user control.
        /// Initializes the control and sets up initial UI elements.
        /// </summary>
        public AddSemesterPageView()
        {
            InitializeComponent();
            DataContext = new SemesterViewModel();
            UpdateList();
            UpdateGrid();

            dpSemesterDate.SelectedDate = DateTime.Today;
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Updates the list of semesters displayed in the ListBox.
        /// </summary>
        private void UpdateList()
        {
            lstSemester.ItemsSource = null; // Clear the ListBox

            UserModel currentStudent = semesterModel.GetStudentDetails();

            if (currentStudent != null)
            {
                List<Semester> semesters = semesterRepository.GetSemestersForCurrentStudent(currentStudent.Username);
                int count = semesters.Count;

                if (semesters.Count > 0)
                {
                    lstSemester.ItemsSource = semesters;
                    int lastIndex = semesters.Count - 1;
                    lstSemester.SelectedIndex = lastIndex;
                    ShowParentButtons(currentStudent, count);
                }
            }
        }


        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Displays the parent buttons in the main window.
        /// </summary>
        public void ShowParentButtons(UserModel currentStudent, int count)
        {
            MainWindowView mainWindow = Window.GetWindow(this) as MainWindowView;
            mainWindow?.ShowButtons(currentStudent,count);
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstSemester_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Updates the DataGrid with the list of semesters.
        /// </summary>
        private void UpdateGrid()
        {
            dataGridSemesters.ItemsSource = null; // Clear the DataGrid

            UserModel currentStudent = semesterModel.GetStudentDetails();

            if (currentStudent != null)
            {
                List<Semester> semesters = semesterRepository.GetSemestersForCurrentStudent(currentStudent.Username);

                if (semesters.Count > 0)
                {
                    dataGridSemesters.ItemsSource = semesters;
                }
            }
        }


        private void btnSave_Click_1(object sender, RoutedEventArgs e)
        {
            UpdateList();
            UpdateGrid();

            tbxNumOfWeeks.Text = "0";
            tbxSemesterName.Clear();
            dpSemesterDate.SelectedDate = DateTime.Now;

        }
        //・♫-------------------------------------------------------------------------------------------------♫・//
    }
}//★---♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫---★・。。END OF FILE 。。・★---♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫---★//
