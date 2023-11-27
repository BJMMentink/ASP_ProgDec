namespace BJM.ProgDec.UI.ViewModels
{
    public class StudentVM
    {
        public Student Student { get; set; }
        public List<Advisor> Advisors { get; set; } = new List<Advisor>();
        public IEnumerable<int> AdvisorIds { get; set; } 

        public StudentVM()
        {
            Advisors = AdvisorManager.Load();
        }

        public StudentVM(int id)
        {
            Advisors = AdvisorManager.Load();
            Student = StudentManager.LoadById(id);
            AdvisorIds = Student.Advisors.Select(a => a.Id);
        }
    }
}
