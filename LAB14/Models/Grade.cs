namespace LAB14.Models
{
    public class Grade
    {
        public int GradeId { get; set; }
        public string Name { get; set; } 
        public string Description { get; set; }
        public bool IsActive { get; set; }

        public List<Student> Students { get; set; }
    }
}
