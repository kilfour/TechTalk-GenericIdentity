using QuickPulse.Explains;

namespace GenericIdentity.A_TheProblem;

[CodeExample]
public class Course
{
    public Guid Id { get; }
    public string Title { get; }
    public Course(string title) => (Id, Title) = (Guid.NewGuid(), title);

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
