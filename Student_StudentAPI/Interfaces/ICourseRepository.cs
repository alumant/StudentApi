using Microsoft.AspNetCore.JsonPatch;
using Student_StudentAPI.Models;
using Student_StudentAPI.Models.Requests;

namespace Student_StudentAPI.Interfaces
{
    public interface ICourseRepository
    {
        Task<string> CreateCourseAsync(CourseRequest newCourse);
        Task<Course> GetCourseByIdAsync(int id);
        Task<Course> PatchAsync(int id, JsonPatchDocument<Course> patch);
        Task<IEnumerable<Course>> GetAllCoursesAsync();
        Task<Course> DeleteCourseAsync(int id);
    }
}
