using QuickPulse.Explains;

namespace GenericIdentity.B_TheSolution.Step1;

[CodeExample]
public record CourseId(Guid Value);

[CodeExample]
public class Course
{
    public CourseId Id { get; }
    public string Title { get; }
    public Course(string title) => (Id, Title) = (new CourseId(Guid.NewGuid()), title);

    public override bool Equals(object? obj)
    {
        if (obj is null || obj.GetType() != GetType()) return false;

        var other = (Course)obj;
        return Id == other.Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}
