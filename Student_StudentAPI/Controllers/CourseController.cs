using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Student_StudentAPI.Interfaces;
using Student_StudentAPI.Models;
using Student_StudentAPI.Models.Requests;

namespace Student_StudentAPI.Controllers
{
    public class CourseController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;

        public CourseController(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        [HttpGet("Courses")]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourses()
        {
            var courses = await _courseRepository.GetAllCoursesAsync();
            return Ok(courses);
        }
        [HttpGet("Course/id")]
        public async Task<ActionResult<Student>> GetCourse(int id)
        {
            var course = await _courseRepository.GetCourseByIdAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            return Ok(course);
        }
        [HttpPost("course")]
        public async Task<ActionResult<Course>> CreateCourse([FromBody] CourseRequest newCourse)
        {
            var result = await _courseRepository.CreateCourseAsync(newCourse);
            return Ok(result);
        }
        [HttpDelete("Course")]
        public async Task<ActionResult> DeleteCourseAsync(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var deletedCourse = await _courseRepository.DeleteCourseAsync(id);
            if (deletedCourse == null)
            {
                return NotFound();
            }

            return NoContent();
        }
        [HttpPatch]
        public async Task<IActionResult> UpdateCourse(int id, [FromBody] JsonPatchDocument<Course> patchDoc)
        {
            try
            {
                var updatedCourse = await _courseRepository.PatchAsync(id, patchDoc);
                return Ok(updatedCourse);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
