using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student_StudentAPI.Data;
using Student_StudentAPI.Models;
using Student_StudentAPI.Models.Requests;

namespace Student_StudentAPI.Interfaces
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ApplicationDbContext _db;

        public CourseRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<string> CreateCourseAsync(CourseRequest newCourse)
        {
            if (newCourse.Id > 0)
            {
                throw new ArgumentException("BadRequest!!");
            }
            var course = new Models.Course
            {
                CourseCode = newCourse.CourseCode,
                CourseDescription = newCourse.CourseDescription
            };

            _db.Courses.Add(course);
            await _db.SaveChangesAsync();

            return "Course created successfully";
        }
        public async Task<IEnumerable<Models.Course>> GetAllCoursesAsync()
        {
            return await _db.Courses.ToListAsync();
        }
        
        public async Task<Models.Course> GetCourseByIdAsync(int id)
        {
            return await _db.Courses.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Models.Course> DeleteCourseAsync(int id)
        {
            var course = await _db.Courses.FirstOrDefaultAsync(c => c.Id == id);
            if (course == null)
            {
                return null;
            }

            _db.Courses.Remove(course);
            await _db.SaveChangesAsync();

            return course;
        }
        public async Task<Course> PatchAsync(int id, JsonPatchDocument<Course> patch)
        {
            var course = await _db.Courses.FirstOrDefaultAsync(c => c.Id == id);
            if (course == null)
            {
                throw new ArgumentException("Invalid student Id.");
            }

            patch.ApplyTo(course);
            await _db.SaveChangesAsync();

            return course;
        }
    }
}
