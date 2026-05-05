using QuickPulse.Explains;

namespace GenericIdentity.B_TheSolution.Step1;

[DocFile]
[DocExample(typeof(CourseId))]
[DocExample(typeof(StudentId))]
[DocContent("En nu kunnen onze entities deze gebruiken.")]
[DocExample(typeof(Course))]
[DocExample(typeof(Student))]
[DocContent("En de service wordt duidelijker.")]
[DocExample(typeof(CoursesRegistry), nameof(CoursesRegistry.Enroll))]
[DocContent("**Gebruik:**")]
[DocExample(typeof(StepOneTypedIds), nameof(Example))]
[DocContent(
"""
Verwisselen van `Student.Id` en `Course.Id` is nu onmogelijk *at compile-time*.
Een ID is niet zomaar data. Een ID verwijst naar iets.

`Id<Course>` zegt:

> Dit is een verwijzing naar een Course

`Id<Student>` zegt:

> Dit is een verwijzing naar een Student

Dat is *domein informatie*.

We hebben ook de *run-time bug* omgezet naar een *compile-error*.
> Compile errors zijn goedkoper dan runtime bugs.
""")]
public class StepOneTypedIds
{
    [Fact]
    [CodeSnippet]
    public void Example()
    {
        var coursesRegistry = new CoursesRegistry();
        var course = coursesRegistry.GetCourseByTitle("Web Dev");
        var student = coursesRegistry.GetStudentByName("Ayende Rahien");
        coursesRegistry.Enroll(course.Id, student.Id);
    }
}