using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class StudentsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public StudentsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
    {
        return await _context.Students.ToListAsync();
    }


    
    [HttpGet("{id}")]
    public async Task<ActionResult<Student>> GetStudent(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student == null) return NotFound();
        return student;
    }



[HttpGet("filter/{studentId}")]
public async Task<ActionResult<object>> GetFilteredStudentData(int studentId)
{
    var studentX = await _context.Students
        .Where(s => s.Id == studentId)
        .Include(s => s.StudentCourses)
        .ThenInclude(sc => sc.Course)
        .FirstOrDefaultAsync();

    if (studentX == null) return NotFound("Student not found.");


    var relatedStudents = await _context.Students
        .Where(s => s.Id != studentX.Id && s.StudentCourses
            .Any(sc => studentX.StudentCourses
                .Select(xc => xc.CourseId)
                .Contains(sc.CourseId)))
        .Include(s => s.StudentCourses)
        .ThenInclude(sc => sc.Course)
        .ToListAsync();


    var studentY = relatedStudents.FirstOrDefault();
    if (studentY == null) return NotFound("No related students found.");

    return Ok(new
    {
        StudentX = new
        {
            studentX.Id,
            studentX.Name,
            Courses = studentX.StudentCourses.Select(sc => new 
            {
                sc.Course.Id,
              //  sc.Course.Title
            })
        },
        StudentY = new
        {
            studentY.Id,
            studentY.Name,
            Courses = studentY.StudentCourses.Select(sc => new
            {
                sc.Course.Id,
               // sc.Course.Title
            })
        },
        SharedCourses = studentX.StudentCourses
            .Where(sc => studentY.StudentCourses
                .Select(yc => yc.CourseId)
                .Contains(sc.CourseId))
            .Select(sc => new
            {
                sc.Course.Id,
              //  sc.Course.Title
            })
    });
}





    [HttpPost]
    public async Task<ActionResult<Student>> CreateStudent(Student student)
    {
        _context.Students.Add(student);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, student);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateStudent(int id, Student student)
    {
        if (id != student.Id) return BadRequest();

        _context.Entry(student).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStudent(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student == null) return NotFound();

        _context.Students.Remove(student);
        await _context.SaveChangesAsync();
        return NoContent();
    }




}
