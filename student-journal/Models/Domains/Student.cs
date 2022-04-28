namespace Diary.Models.Domains
{
    internal class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Comments { get; set; }
        public string Activities { get; set; }
        public int GroupId { get; set; }
    }
}
