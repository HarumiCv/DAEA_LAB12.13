namespace LAB14.Models.Requests
{
    public class GradeRequestV1
    {
        public int GradeId { get; set; } 

        public List<UserRequestV4> Students { get; set; }
    }
}
