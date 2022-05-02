
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

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(FirstName):
                        Error = string.IsNullOrWhiteSpace(FirstName) ? "First name is required!" : string.Empty;
                        break;
                    case nameof(LastName):
                        Error = string.IsNullOrWhiteSpace(LastName) ? "Last name is required!" : string.Empty;
                        break;
                    default:
                        break;
                }

                return Error;
            }
        }

        public string Error { get; set; }
    }
}
