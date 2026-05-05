using QuickPulse.Explains;

namespace GenericIdentity.A_TheProblem;

[CodeExample]
public class CoursesRegistry
{
    // --------------------------------------------------------------------------------------
    // COURSES
    // --
    private readonly List<Course> courses = [new Course("Web Dev"), new Course("Pottery")];
    public IReadOnlyList<Course> Courses => courses;
    public Course GetCourseByTitle(string title) => courses.Single(a => a.Title == title);
    // --------------------------------------------------------------------------------------
    // STUDENTS
    // --
    private readonly List<Student> students = [new Student("Ayende Rahien"), new Student("Uncle Bob")];
    public IReadOnlyList<Student> Students => students;
    public Student GetStudentByName(string name) => students.Single(a => a.Name == name);
    // --------------------------------------------------------------------------------------
    // ENROLLMENTS
    // --
    public void Enroll(Guid courseId, Guid studentId)
    {
        var course = courses.SingleOrDefault(a => a.Id == courseId);
        if (course == null)
            throw new ArgumentException("Course not found", nameof(courseId));
        var student = students.SingleOrDefault(a => a.Id == studentId);
        if (course == null)
            throw new ArgumentException("Student not found", nameof(studentId));

        // Do stuff, a.k.a. enroll the student.
    }
}