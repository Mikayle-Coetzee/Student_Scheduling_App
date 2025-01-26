#region Documentation Header
// File: ViewModelBase.cs
// Author: Mikayle Coetzee (ST10023767)
// Course: PROG6212 POE 2023
// Part: 2
// Description: ...
#endregion

using System.ComponentModel;

namespace _10023767_P2.ViewModel
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
