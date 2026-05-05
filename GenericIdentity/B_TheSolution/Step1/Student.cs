using QuickPulse.Explains;

namespace GenericIdentity.B_TheSolution.Step1;

[CodeExample]
public record StudentId(Guid Value);

[CodeExample]
public class Student
{
    public StudentId Id { get; }
    public string Name { get; }
    public Student(string name) => (Id, Name) = (new StudentId(Guid.NewGuid()), name);

    public override bool Equals(object? obj)
    {
        if (obj is null || obj.GetType() != GetType()) return false;

        var other = (Student)obj;
        return Id == other.Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}
