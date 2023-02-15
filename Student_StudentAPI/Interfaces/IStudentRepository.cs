using Microsoft.AspNetCore.JsonPatch;
using Student_StudentAPI.Models;
using Student_StudentAPI.Models.Requests;

namespace Student_StudentAPI.Interfaces
{
    public interface IStudentRepository
    {
        
        Task<string> CreateStudentAsync(StudentRequest newStudent);
        Task<Student> PatchAsync(int id, JsonPatchDocument<Student> patch);
        Task<Student> GetStudentByIdAsync(int id);
        Task<IEnumerable<Student>> GetAllStudentsAsync();
        Task<string> DeleteStudentAsync(int id);
        

    }
}
