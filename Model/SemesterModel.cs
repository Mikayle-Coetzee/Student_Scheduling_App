#region Documentation Header
// File: SemesterModel.cs
// Author: Mikayle Coetzee (ST10023767)
// Course: PROG6212 POE 2023
// Part: 2
// Description: ...
#endregion

using _10023767_P2.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Xml.Linq;
using System.ComponentModel.DataAnnotations;
using System.Windows.Media;
using System.Windows;
using System.Threading;
using _10023767_P2.ViewModel;
using FontAwesome.Sharp;
using System.Runtime.Remoting.Contexts;

namespace _10023767_P2.Model
{
    public class SemesterModel
    {
        /// <summary>
        /// Gets or sets the name of the semester.
        /// </summary>
        public string SemesterName { get; set; }

        /// <summary>
        /// Gets or sets the number of weeks in the semester.
        /// </summary>
        public int NumOfWeeks { get; set; }

        /// <summary>
        /// Gets or sets the start date of the semester.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the list of modules associated with the semester.
        /// </summary>
        public List<Module> ModuleList { get; set; }

        /// <summary>
        /// Gets the number of modules associated with the semester.
        /// </summary>
        public int NumberOfModules
        {
            get
            {
                return ModuleList?.Count ?? 0;
            }
        }

        private UserRepository userRepository = new UserRepository();
        private SemesterRepository semesterRepository = new SemesterRepository();
        private CustomClassLibrary.ValidationClass validate = new CustomClassLibrary.ValidationClass();
        public bool AddSemester(string SemesterName, int NumOfWeeks
                , DateTime StartDate)
        {
            // Convert the SecureString to a string for storing in the database

            bool saved = false;
            if (ValidateSemester(SemesterName, NumOfWeeks, StartDate))
            {
                LoadCurrentUserData();
                saved = semesterRepository.SaveSemester(SemesterName, NumOfWeeks, StartDate, username);
            }
            return saved;
        }
        private string username = string.Empty;

        private void LoadCurrentUserData()
        {
            var user = userRepository.GetByUsername(Thread.CurrentPrincipal.Identity.Name);
            if (user != null)
            {
                username = user.Username;
            }

        }

        public UserModel GetStudentDetails()
        {
            LoadCurrentUserData ();
            return userRepository.GetByUsername(username);

        }

        private bool ValidateSemester(string semesterName, int numOfWeeks
                , DateTime startDate)
        {
            var validationRules = new[]
            {
                new { ControlType="TextBox", Control = "semesterName",ErrorMessage = "Please enter a valid semester name." },
                new { ControlType="TextBox", Control = "numOfWeeks",ErrorMessage = "Please enter the valid number of weeks." },
                new { ControlType="DateTimePicker",Control = "startDate",ErrorMessage = "Please select a valid date." }
            };
            string numOfWeeksString = Convert.ToString(numOfWeeks);
            string startDateString = Convert.ToString(startDate);

            bool valid = true;
            foreach (var rule in validationRules)
            {
                switch (rule.ControlType)
                {
                    case "TextBox" when rule.Control == "numOfWeeks":
                        if (!validate.Validate_Integer(numOfWeeksString))
                        {
                            MessageBox.Show(rule.ErrorMessage);
                            valid = false;
                        }
                        break;

                    case "DateTimePicker":
                        if (!validate.Validate_Date(startDateString))
                        {
                            MessageBox.Show(rule.ErrorMessage);
                            valid = false;
                        }
                        break;

                    case "TextBox":
                        if (!validate.Validate_String(numOfWeeksString) &&
                            !validate.Validate_String(semesterName) )
                        {
                            MessageBox.Show(rule.ErrorMessage);
                            valid = false;
                        }
                        break;
                }
            }
            return valid;
        }
    }
}
