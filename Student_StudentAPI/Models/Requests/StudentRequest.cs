namespace Student_StudentAPI.Models.Requests
{
    public class StudentRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int CourseId { get; set; }
    }
}
