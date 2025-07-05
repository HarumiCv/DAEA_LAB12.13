using LAB14.Data;
using LAB14.Models.Requests;
using LAB14.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LAB14.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradeCustomController : ControllerBase
    {
        private readonly DemoContext _context;

        public GradeCustomController(DemoContext context)
        {
            _context = context;
        }

        [HttpPost("InsertStudentsByGrade")]
        public void InsertStudentsByGrade([FromBody] GradeRequestV1 request)
        {
            var grade = _context.Grades.FirstOrDefault(g => g.GradeId == request.GradeId && g.IsActive);
            if (grade == null)
            {
                return;
            }

            var students = request.Students.Select(s => new Student
            {
                GradeId = request.GradeId,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Phone = s.Phone,
                Email = s.Email,
                IsActive = true
            }).ToList();

            _context.Students.AddRange(students);
            _context.SaveChanges();
        }

    }
}
