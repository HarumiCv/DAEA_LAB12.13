using LAB14.Data;
using LAB14.Models;
using LAB14.Models.Requests;
using LAB14.Models.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LAB14.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CoursesCustomController : ControllerBase
    {
        private readonly DemoContext _context;

        public CoursesCustomController(DemoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<CourseResponseV1> Get()
        {
            var courses = _context.Courses
                .Where(c => c.IsActive)
                .ToList();

            var response = courses.Select(c => new CourseResponseV1
            {
                CourseId = c.CourseId,
                Name = c.Name,
                Credit = c.Credit
            }).ToList();

            return response;
        }

        [HttpGet]
        public List<CourseResponseV1> GetByName(string name)
        {
            var courses = _context.Courses
                .Where(c => c.IsActive && c.Name.Contains(name))
                .ToList();

            var response = courses.Select(c => new CourseResponseV1
            {
                CourseId = c.CourseId,
                Name = c.Name,
                Credit = c.Credit
            }).ToList();

            return response;
        }

        [HttpPost]
        public void Insert(CourseRequestV1 request)
        {
            var course = new Course
            {
                Name = request.Name,
                Credit = request.Credit,
                IsActive = true
            };

            _context.Courses.Add(course);
            _context.SaveChanges();
        }

    }
}
