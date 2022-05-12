using System.ComponentModel;

namespace Diary.Models
{
    public class DatabaseSettings : IDataErrorInfo
    {
        public string ServerAddress { get; set; }
        public string ServerName { get; set; }
        public string DatabaseName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        private bool _isServerAddressValid;
        private bool _isServerNameValid;
        private bool _isDatabaseNameValid;
        private bool _isUsernameValid;
        private bool _isPasswordValid;

        public string this[string columnName]
        {
            get
            {
                switch(columnName)
                {
                    case nameof(ServerAddress):
                        _isServerAddressValid = IsPropertyValid(ServerAddress);
                        break;

                    case nameof(ServerName):
                        _isServerNameValid = IsPropertyValid(ServerName);
                        break;

                    case nameof(DatabaseName):
                        _isDatabaseNameValid = IsPropertyValid(DatabaseName);
                        break;

                    case nameof(Username):
                        _isUsernameValid = IsPropertyValid(Username);
                        break;

                    case nameof(Password):
                        _isPasswordValid = IsPropertyValid(Password);
                        break;
                }

                return Error;
            }
        }

        private bool IsPropertyValid(string prop)
        {
            if (string.IsNullOrWhiteSpace(prop))
            {
                Error = "Field is required!";
                return false;
            }
            
            Error = string.Empty;
            return true;
        }

        public string Error { get; set; }

        public bool IsValid => _isServerAddressValid 
            && _isServerNameValid 
            && _isDatabaseNameValid 
            && _isUsernameValid 
            && _isPasswordValid;
    }
}
