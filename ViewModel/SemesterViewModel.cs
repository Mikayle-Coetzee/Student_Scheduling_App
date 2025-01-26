#region Documentation Header
// File: SemesterViewModel.cs
// Author: Mikayle Coetzee (ST10023767)
// Course: PROG6212 POE 2023
// Part: 2
// Description: ...
#endregion

using _10023767_P2.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace _10023767_P2.ViewModel
{
    public class SemesterViewModel : ViewModelBase, INotifyPropertyChanged
    {
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

        // Create a private backing field for SemesterModel
        private SemesterModel semesterModel;

        public SemesterViewModel()
        {
            semesterModel = new SemesterModel();
            StartDate = DateTime.Now;
            Semesters = new ObservableCollection<Semester>();


            // Initialize your commands and properties here
            SaveCommand = new ViewModelCommand(ExecuteSaveCommand, CanExecuteSaveCommand);
            CancelCommand = new ViewModelCommand(ExecuteCancelCommand);
            HelpCommand = new ViewModelCommand(ExecuteHelpCommand);

            // Initialize the properties
            SemesterName = semesterModel.SemesterName;
            NumOfWeeks = semesterModel.NumOfWeeks;
            StartDate = semesterModel.StartDate;
        }

        // Public properties for data binding
        public string SemesterName
        {
            get { return semesterModel.SemesterName; }
            set
            {
                if (semesterModel.SemesterName != value)
                {
                    semesterModel.SemesterName = value;
                    OnPropertyChanged();
                }
            }
        }

        public int NumOfWeeks
        {
            get { return semesterModel.NumOfWeeks; }
            set
            {
                if (semesterModel.NumOfWeeks != value)
                {
                    semesterModel.NumOfWeeks = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime StartDate
        {
            get { return semesterModel.StartDate; }
            set
            {
                if (semesterModel.StartDate != value)
                {
                    semesterModel.StartDate = value;
                    OnPropertyChanged();
                }
            }
        }

        // Public commands for data binding
        public ICommand SaveCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }
        public ICommand HelpCommand { get; private set; }

        // Command methods
        private bool CanExecuteSaveCommand(object obj)
        {
            // Implement your validation logic for SaveCommand here
            // Return true if the command can be executed, false otherwise.
            return true;
        }

        private void ExecuteSaveCommand(object obj)
        {
            var isSaved = semesterModel.AddSemester(semesterModel.SemesterName,semesterModel.NumOfWeeks
                ,semesterModel.StartDate);

            if (isSaved)
            {
                // Clear user input 
                ClearUserInput();
                MessageBox.Show("Semester added successfully.");
            }
            else
            {
                MessageBox.Show("Failed to add semester. Please check your data and try again.");
            }
        }
        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Clears all user input fields 
        /// </summary>
        private void ClearUserInput()
        {
            // Clear all user input fields
            SemesterName = string.Empty;
            NumOfWeeks = 0;
            StartDate = DateTime.Now;
        }

        private void ExecuteCancelCommand(object obj)
        {
            ClearUserInput();
        }

        private void ExecuteHelpCommand(object obj)
        {
            string message = "How to Use the Semester Manager:\n\n" +
                         "1. Entering Semester Details:\n\n" +
                         "- In the window, you'll find three input fields:\n" +
                         "  - Semester Name: Enter the name of the semester.\n" +
                         "  - Semester Start Date: Select the starting date of the semester.\n" +
                         "  - Number of Weeks: Specify the total number of weeks in the semester.\n\n" +
                         "2. Taking Action:\n\n" +
                         "- After entering the details, you have two options:\n" +
                         "  - Clear Controls: Click this button to reset all entered data.\n" +
                         "  - Add Semester: Click this button to save the semester details.\n\n" +
                         "3. Viewing Added Semesters:\n\n" +
                         "- Successfully added semesters will be displayed in the list on the right side of the window.\n\n" +
                         "4. Display Semester Details:\n\n" +
                         "- To view the details of a specific semester:\n" +
                         "  - Click on the semester's name in the list.\n" +
                         "  - The semester's information will be shown in the data grid.";

            MessageBox.Show(message);
        }

        private ObservableCollection<Semester> _semesters;
        public ObservableCollection<Semester> Semesters
        {
            get { return _semesters; }
            set
            {
                _semesters = value;
                OnPropertyChanged(nameof(Semesters));
            }
        }


      
    }
}

