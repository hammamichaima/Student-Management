public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }

    public ICollection<StudentCourse>? StudentCourses { get; set; }
}
