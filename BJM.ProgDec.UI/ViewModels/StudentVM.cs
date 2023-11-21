namespace BJM.ProgDec.UI.ViewModels
{
    public class StudentVM
    {
        public Student Student { get; set; }
        public List<Advisor> Advisors { get; set; } = new List<Advisor>();
        public IEnumerable<int> AdvisorId { get; set; } // new advisors for the students
        public StudentVM() 
        {
            Advisors = AdvisorManager.Load();
        }
        public StudentVM(int id)
        {
            Advisors = AdvisorManager.Load();
            Student = StudentManager.LoadById(id);
            AdvisorId = Student.Advisors.Select(a => a.Id);
        }
    }
}
