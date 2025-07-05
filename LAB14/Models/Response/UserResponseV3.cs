namespace LAB14.Models.Response
{
    public class UserResponseV3
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public List<CourseResponseV1> Courses { get; set; }
    }
}
