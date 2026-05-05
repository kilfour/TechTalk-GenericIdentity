using QuickPulse.Explains;

namespace GenericIdentity.A_TheProblem;

[DocFile]
[DocExample(typeof(TheProblem), nameof(Example))]
public class TheProblem
{
    [Fact(Skip = "This test fails, demonstrating The Problem.")]
    [CodeSnippet]
    [DocContent(
"""
**Primitive obsession**  
We gebruiken een primitief type voor iets dat eigenlijk domeinbetekenis heeft.  
Guid is technisch correct, maar domeinmatig te vaag.  

> `Course.Id` en `Student.Id` zijn niet hetzelfde concept.
""")]
    public void Example()
    {
        var coursesRegistry = new CoursesRegistry();
        var course = coursesRegistry.GetCourseByTitle("Web Dev");
        var student = coursesRegistry.GetStudentByName("Ayende Rahien");
        coursesRegistry.Enroll(student.Id, course.Id);
    }
}

