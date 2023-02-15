using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Student_StudentAPI.Data;
using Student_StudentAPI.Models;
using Student_StudentAPI.Models.Requests;

namespace Student_StudentAPI.Interfaces
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _db;

        public StudentRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<string> CreateStudentAsync(StudentRequest newStudent)
        {
            var student = new Student
            {
                Name = newStudent.Name,
                Email= newStudent.Email,
                CourseId=newStudent.CourseId
            };

            _db.students.Add(student);
            await _db.SaveChangesAsync();

            return "Student created successfully";
        }

        
        public async Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            //Entity Framework Eager loading course object
            return await _db.students.Include(s => s.Course).ToListAsync();
        }


        public async Task<Student> GetStudentByIdAsync(int id)
        {
            //Entity Framework Eager loading course object
            return await _db.students.Include(s => s.Course).FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<string> DeleteStudentAsync(int id)
        {
            var student = await _db.students.FirstOrDefaultAsync(s => s.Id == id);
            if (student == null)
            {
                return null;
            }
            _db.students.Remove(student);
            await _db.SaveChangesAsync();

            return "Student successfully deleted";
        }
        public async Task<Student> PatchAsync(int id, JsonPatchDocument<Student> patch)
        {
            var student = await _db.students.FirstOrDefaultAsync(s => s.Id == id);
            if (student == null)
            {
                throw new ArgumentException("Invalid student Id.");
            }

            patch.ApplyTo(student);
            await _db.SaveChangesAsync();

            return student;
        }

    }
}
