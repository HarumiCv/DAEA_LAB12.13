namespace LAB14.Models.Response
{
    public class UserResponseV2
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public GradeResponseV1 Grade { get; set; }
    }
}
