namespace LAB14.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public string Name { get; set; }
        public int Credit { get; set; }
        public bool IsActive { get; set; }

        public List<Enrollment> Enrollments { get; set; }
    }
}
