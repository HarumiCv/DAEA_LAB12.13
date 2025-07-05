namespace LAB14.Models.Requests
{
    public class EnrollmentRequestV2
    {
        public int StudentId { get; set; }
        public List<int> CourseIds { get; set; }
    }
}
