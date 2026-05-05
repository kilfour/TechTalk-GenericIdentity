using QuickPulse.Explains;

namespace GenericIdentity.B_TheSolution.Step3;

[CodeExample]
public class Course(string title) : DomainEntity<Course>
{
    public string Title { get; } = title;
}
