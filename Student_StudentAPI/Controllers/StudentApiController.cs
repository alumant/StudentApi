using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student_StudentAPI.Data;
using Student_StudentAPI.Interfaces;
using Student_StudentAPI.Models;
using Student_StudentAPI.Models.Requests;
using System;

namespace Student_StudentAPI.Controllers
{
    [Route("api/StudentApi")]
    [ApiController]
    public class StudentApiController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;

        public StudentApiController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            var students = await _studentRepository.GetAllStudentsAsync();
            return Ok(students);
        }

        [HttpGet("Student/id")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {

            var student = await _studentRepository.GetStudentByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }

        [HttpPost]
        public async Task<ActionResult<Student>> CreateStudent([FromBody] StudentRequest newStudent)
        {
            var result = await _studentRepository.CreateStudentAsync(newStudent);

            return Ok(result);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteStudent(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var message = await _studentRepository.DeleteStudentAsync(id);
            if (message == null)
            {
                return NotFound();
            }
            return Ok(message);
        }
        [HttpPatch]
        public async Task<IActionResult> UpdateStudent(int id, [FromBody] JsonPatchDocument<Student> patchDoc)
        {
            try
            {
                var updatedStudent = await _studentRepository.PatchAsync(id, patchDoc);
                return Ok(updatedStudent);
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
