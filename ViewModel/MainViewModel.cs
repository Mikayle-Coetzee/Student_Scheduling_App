#region Documentation Header
// File: MainViewModel.cs
// Author: Mikayle Coetzee (ST10023767)
// Course: PROG6212 POE 2023
// Part: 2
// Description: ...
#endregion

using _10023767_P2.Model;
using _10023767_P2.Repositories;
using FontAwesome.Sharp;
using System.Threading;
using System.Windows.Input;

namespace _10023767_P2.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private UserAccountModel _currentUserAccount;
        private IUserRepository userRepository;
        private ViewModelBase _currentChildView;
        private string _caption;
        private IconChar _icon;

        public UserAccountModel CurrentUserAccount
        {
            get
            {
                return _currentUserAccount;
            }
            set
            {
                _currentUserAccount = value;
                OnPropertyChanged(nameof(CurrentUserAccount));
            }
        }
        public ViewModelBase CurrentChildView
        {
            get
            {
                return _currentChildView;
            }
            set
            {
                _currentChildView = value;
                OnPropertyChanged(nameof(CurrentChildView));
            }
        }
        public string Caption
        {
            get
            {
                return _caption;
            }
            set
            {
                _caption = value;
                OnPropertyChanged(nameof(Caption));
            }
        }
        public IconChar Icon
        {
            get
            {
                return _icon;
            }
            set
            {
                _icon = value;
                OnPropertyChanged(nameof(Icon));
            }
        }



        //public ICommand ShowSignUpViewCommand { get; }
        public ICommand ShowSemesterViewCommand { get; }
        public ICommand ShowModuleViewCommand { get; }

        public MainViewModel()
        {
            userRepository = new UserRepository();
            CurrentUserAccount = new UserAccountModel();
            //Initialize commands
            ShowSemesterViewCommand = new ViewModelCommand(ExecuteShowSemesterViewCommand);
            ShowModuleViewCommand = new ViewModelCommand(ExecuteShowModuleViewCommand);
            //Default view
            ExecuteShowSemesterViewCommand(null);
            LoadCurrentUserData();
        }

        private void ExecuteShowModuleViewCommand(object obj)
        {
            CurrentChildView = new ModuleViewModel();
            Caption = "Add Module";
            Icon = IconChar.Book;
        }


        private void ExecuteShowSemesterViewCommand(object obj)
        {
            CurrentChildView = new SemesterViewModel();
            Caption = "Add Semester";
            Icon = IconChar.PlusCircle;
        }
        private void LoadCurrentUserData()
        {
            var user = userRepository.GetByUsername(Thread.CurrentPrincipal.Identity.Name);
            if (user != null)
            {
                CurrentUserAccount.Username = user.Username;
                CurrentUserAccount.DisplayName = $"{user.Username}";
                CurrentUserAccount.ProfilePicture = user.Profile;
            }
            else
            {
                CurrentUserAccount.DisplayName = "Invalid user, please make sure that you are logged in";
                //hide child views 
            }
        }


    }
}
