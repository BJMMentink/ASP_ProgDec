using System.ComponentModel;

namespace BJM.ProgDec.BL.Models
{
    public class Student
    {
        public int Id { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; } = string.Empty;
        [DisplayName("Last Name")]
        public string LastName { get; set; } = string.Empty;
        [DisplayName("Student ID")]
        public string StudentId { get; set; } = string.Empty;
        [DisplayName("Full Name")]
        public string FullName { get { return FirstName + " " + LastName; } }

        public List<Advisor> Advisors { get; set; } = new List<Advisor>();

    }
}