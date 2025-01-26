#region Documentation Header
// File: SignUpViewModel.cs
// Author: Mikayle Coetzee (ST10023767)
// Course: PROG6212 POE 2023
// Part: 2
// Description: This file contains the SignUpViewModel class, which is a ViewModel for signing up users in a WPF application.
// It handles user registration, input validation, and image uploading.
#endregion

#region Documentation Header
// File: AddModulePageView.xaml.cs
// Author: Mikayle Coetzee (ST10023767)
// Course: PROG6212 POE 2023
// Part: 1
// Description: This class represents the AddModulePageView user control, which allows users to add modules to a selected semester.
#endregion

using _10023767_P2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace _10023767_P2.View
{
    /// <summary>
    /// Interaction logic for AddModulePageView.xaml
    /// </summary>
    public partial class AddModulePageView : UserControl
    {
        /// <summary>
        /// Instance of the ValidationClass for input validation.
        /// </summary>
        //private CustomClassLibrary.ValidationClass validate = new CustomClassLibrary.ValidationClass();

        ///// <summary>
        ///// Instance of the WorkerClass for adding modules.
        ///// </summary>
        //private WorkerClass workerClass = new WorkerClass();

        ///// <summary>
        ///// Instance of the MessageHandlingClass for displaying messages.
        ///// </summary>
        //private MessageHandlingClass MessageHandling = new MessageHandlingClass();

        /// <summary>
        /// Original border brush used for error message clearing.
        /// </summary>
        private SolidColorBrush originalBrush = new SolidColorBrush();

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Constructor for the AddModulePageView user control.
        /// Initializes the control and sets up initial UI elements.
        /// </summary>
        public AddModulePageView()
        {
            InitializeComponent();

            //StudentClass currentStudent = ServiceLocator.MainViewModel.CurrentStudent;
            //if (currentStudent != null && currentStudent.Semesters.Count > 0)
            //{
            //    List<string> semesterNames = currentStudent.Semesters.Select(semester => semester.SemesterName).ToList();

            //    cbxSemesterName.ItemsSource = semesterNames;
            //    cbxSemesterName.SelectedIndex = 0;

            //    cbxMinutes.ItemsSource = Enumerable.Range(0, 60).Select(i => i.ToString()).ToList();
            //    cbxMinutes.SelectedIndex = 0;

            //    updateList();
            //    updateGrid();
            //}

        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Clears all input controls on the form.
        /// </summary>
        private void ClearControls()
        {
            originalBrush = tbxModuleCode.BorderBrush as SolidColorBrush;

            //ClearClass.ClearControls<TextBox>(this);
            //ClearClass.ClearControls<ComboBox>(this);
            //ClearClass.ClearControls<CheckBox>(this);
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Handles the click event for the cancel button.
        /// Clears all input controls.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            ClearControls();
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Handles the click event for the save button.
        /// Validates user input and adds a module to the selected semester.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string semesterName = cbxSemesterName.SelectedItem as string;
            string name = tbxModuleName.Text;
            string classHrsPerWeek = tbxClassHrsPerWeek.Text;
            string code = tbxModuleCode.Text;
            string numOfCredits = tbxCredits.Text;
            string classMinPerWeek = cbxMinutes.Text;


            var validationRules = new[]
            {
                new { Control = tbxModuleName as Control, ErrorMessage = "Please enter a valid module name." },
                new { Control = tbxClassHrsPerWeek as Control, ErrorMessage = "Please enter the valid class hours per week." },
                new { Control = tbxModuleCode as Control, ErrorMessage = "Please enter a valid module code." },
                new { Control = tbxCredits as Control, ErrorMessage = "Please enter a valid number of credits." }
            };

            bool valid = true;


            //foreach (var rule in validationRules)
            //{
            //    switch (rule.Control)
            //    {
            //        case TextBox tbx when tbx == tbxClassHrsPerWeek:
            //            if (!validate.Validate_Float(tbx.Text))
            //            {
            //                MessageHandling.DisplayError(tbx, rule.ErrorMessage);
            //                valid = false;
            //            }
            //            else
            //            {
            //                MessageHandling.ClearError(tbx, originalBrush);
            //            }
            //            break;

            //        case TextBox tbx when tbx == tbxCredits:
            //            if (!validate.Validate_Integer(tbx.Text))
            //            {
            //                MessageHandling.DisplayError(tbx, rule.ErrorMessage);
            //                valid = false;
            //            }
            //            else
            //            {
            //                MessageHandling.ClearError(tbx, originalBrush);
            //            }
            //            break;

            //        case TextBox textBox:
            //            if (!validate.Validate_String(textBox.Text))
            //            {
            //                MessageHandling.DisplayError(textBox, rule.ErrorMessage);
            //                valid = false;
            //            }
            //            else
            //            {
            //                MessageHandling.ClearError(textBox, originalBrush);
            //            }
            //            break;
            //    }

            //}

            if (valid)
            {
                int numOfCreditsConverted = Convert.ToInt32(numOfCredits);

                //TimeEntryClass timePerWeek = new TimeEntryClass
                //{ Hours = Convert.ToInt32(classHrsPerWeek), Minutes = Convert.ToInt32(classMinPerWeek) };

                //workerClass.AddModule(name, timePerWeek, code, numOfCreditsConverted, semesterName);


                ClearControls();

                updateGrid();
            }
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Handles the selection changed event for the semester name combo box.
        /// Updates the module list and grid based on the selected semester.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Selection changed event arguments.</param>
        private void cbxSemesterName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updateList();
            updateGrid();
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// This method updates the DataGrid to display modules for the selected semester.
        /// </summary>
        private void updateGrid()
        {
            //string semesterName = cbxSemesterName.SelectedItem as string;
            //StudentClass currentStudent = ServiceLocator.MainViewModel.CurrentStudent;

            //if (currentStudent != null)
            //{
            //    SemesterClass selectedSemester = currentStudent.Semesters.FirstOrDefault(semester => semester.SemesterName == semesterName);

            //    dataGridModules.ItemsSource = selectedSemester?.ModuleList;
            //}
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// This method displays a help message to guide users on adding modules to a semester.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {
            string message = "How to Add a Module to a Semester:\n\n" +
                          "1. Selecting a Semester:\n" +
                          "- Choose the semester to which you want to add a module from the available options.\n\n" +
                          "2. Module Details:\n" +
                          "- After selecting a semester, you'll need to provide the following information for the module:\n" +
                          "  - Module Code: Enter the unique code for the module.\n" +
                          "  - Module Name: Input the name of the module.\n" +
                          "  - Number of Credits: Specify the credit value for the module.\n" +
                          "  - Class Hours per Week: Enter the number of hours dedicated to this module per week.\n\n" +
                          "3. Adding Minutes (Optional):\n" +
                          "- If you want to add minutes to the hours, check the 'Add Minutes' checkbox.\n" +
                          "  - Once checked, you can select the number of minutes to be added to the class hours.\n\n" +
                          "4. Taking Action:\n" +
                          "- After entering the module details, you have two options:\n" +
                          "  - Clear All Data: Click this button to reset all entered module data.\n" +
                          "  - Add Module: Click this button to save the module details.\n\n" +
                          "5. Viewing Added Modules:\n" +
                          "- Successfully added modules will be displayed in the listbox on the right side of the window and will change according to the semester name selected.\n" +
                          "- The module details will also be populated in a grid depending on the semester name selected.";



            //MessageHandling.DisplayHelpMessage(message);
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Handles the Checked event for the "Add Minutes" checkbox.
        /// Shows the minutes selection ComboBox and label when checked.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        private void cbAddMinutes_Checked(object sender, RoutedEventArgs e)
        {
            cbxMinutes.Visibility = Visibility.Visible;
            cbxMinutes.SelectedIndex = 0;
            lblMinutes.Visibility = Visibility.Visible;
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Handles the Unchecked event for the "Add Minutes" checkbox.
        /// Hides the minutes selection ComboBox and label when unchecked.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        private void cbAddMinutes_Unchecked(object sender, RoutedEventArgs e)
        {
            cbxMinutes.Visibility = Visibility.Collapsed;
            cbxMinutes.SelectedIndex = 0;
            lblMinutes.Visibility = Visibility.Collapsed;
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Handles the SelectionChanged event for the ListBox displaying semesters.
        /// Updates the DataGrid with modules for the selected semester.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        private void lstSemester_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updateGridOutOfListBox();
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Updates the ListBox to display all semesters of the current student.
        /// </summary>
        private void updateList()
        {
            lstSemester.ItemsSource = null; // Clear the ListBox

            //StudentClass currentStudent = ServiceLocator.MainViewModel.CurrentStudent;
            //if (currentStudent != null && currentStudent.Semesters.Count > 0)
            //{
            //    lstSemester.ItemsSource = currentStudent.Semesters; // Re-bind the ListBox

            //    int lastIndex = cbxSemesterName.SelectedIndex;
            //    lstSemester.SelectedIndex = lastIndex;
            //}

        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Updates the DataGrid with modules for the selected semester when a semester is selected from the ListBox.
        /// </summary>
        private void updateGridOutOfListBox()
        {

            //SemesterClass selectedSemester = lstSemester.SelectedItem as SemesterClass;
            //if (selectedSemester != null)
            //{
            //    dataGridModules.ItemsSource = selectedSemester?.ModuleList;
            //}

        }
        //・♫-------------------------------------------------------------------------------------------------♫・//
    }
}//★---♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫---★・。。END OF FILE 。。・★---♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫---★//
