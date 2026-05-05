using GenericIdentity.A_TheProblem;
using QuickPulse.Explains;

namespace GenericIdentity;

[DocFile]
[DocContent("> Gegeven het volgende (speelgoed) domein:")]
[DocContent("**Entities:**")]
[DocExample(typeof(Course))]
[DocExample(typeof(Student))]
[DocContent("**Service:**")]
[DocExample(typeof(CoursesRegistry))]
public class TheGenericIdentity
{
    [Fact]
    public void Doc() => Explain.This<TheGenericIdentity>("README.md");
}

