namespace LAB14.Models.Response
{
    public class EnrollmentResponseV1
    {
        public int EnrollmentId { get; set; }
        public DateTime Date { get; set; }

        public UserResponseV1 Student { get; set; }

        public CourseResponseV1 Course { get; set; }
    }
}
