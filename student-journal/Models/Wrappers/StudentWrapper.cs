
using System.ComponentModel;

namespace Diary.Models.Wrappers
{
    public class StudentWrapper : IDataErrorInfo
    {
        public StudentWrapper()
        {
            Group = new GroupWrapper();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Comments { get; set; }
        public string Math { get; set; }
        public string Technology { get; set; }
        public string Physics { get; set; }
        public string PolishLang { get; set; }
        public string ForeignLang { get; set; }
        public string Activities { get; set; }

        public GroupWrapper Group { get; set; }

        private bool _isFirstNameValid;
        private bool _isLastNameValid;

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(FirstName):
                        if (string.IsNullOrWhiteSpace(FirstName))
                        {
                            Error = "First name is required!";
                            _isFirstNameValid = false;
                        }
                        else
                        {
                            Error = string.Empty;
                            _isFirstNameValid = true;
                        }
                        break;

                    case nameof(LastName):
                        if (string.IsNullOrWhiteSpace(LastName))
                        {
                            Error = "Last name is required!";
                            _isLastNameValid = false;
                        }
                        else
                        {
                            Error = string.Empty;
                            _isLastNameValid = true;
                        }
                        break;

                    default:
                        break;
                }

                return Error;
            }
        }

        public string Error { get; set; }

        public bool IsValid => _isFirstNameValid && _isLastNameValid && Group.IsValid;
    }
}
