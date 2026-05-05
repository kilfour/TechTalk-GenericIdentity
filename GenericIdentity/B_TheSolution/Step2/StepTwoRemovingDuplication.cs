using QuickPulse.Explains;

namespace GenericIdentity.B_TheSolution.Step2;

[DocFile]
[DocContent(
"""
Cool, maar nu moeten we een extra klasse maken voor elke entiteit, ... less cool.  

Deze types vertegenwoordigen allen hetzelfde idee.  
> Een `Id` voor een specifiek domeintype.

Alleen het domeintype verandert.

*Generics* to the rescue !
""")]
[DocHeader("Generic Identity")]
[DocExample(typeof(Id<>))]
[DocContent("**Gebruik:**")]
[DocExample(typeof(StepTwoRemovingDuplication), nameof(UsageExample))]
[DocContent(
"""
**Belangrijk:** `courseId` en `studentId` zijn ook hier van een verschillent type.
""")]
[DocHeader("Refactoring the Entities")]
[DocExample(typeof(Course))]
[DocExample(typeof(Student))]
[DocHeader("And the Service")]
[DocContent("> Hier passen we enkel de *function signature* aan.")]
[DocExample(typeof(CoursesRegistry), nameof(CoursesRegistry.Enroll))]
public class StepTwoRemovingDuplication
{
    [CodeSnippet]
    private static void UsageExample()
    {
        Id<Course> courseId = new(Guid.NewGuid());
        Id<Student> studentId = new(Guid.NewGuid());
    }

    [Fact]
    public void Example()
    {
        var coursesRegistry = new CoursesRegistry();
        var course = coursesRegistry.GetCourseByTitle("Web Dev");
        var student = coursesRegistry.GetStudentByName("Ayende Rahien");
        coursesRegistry.Enroll(course.Id, student.Id);
    }
}
