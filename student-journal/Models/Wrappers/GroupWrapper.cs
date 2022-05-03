using System.ComponentModel;

namespace Diary.Models.Wrappers
{
    public class GroupWrapper : IDataErrorInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(Id):
                        Error = Id == 0 ? "Group field is required!" : string.Empty;
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
