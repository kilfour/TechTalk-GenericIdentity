using QuickPulse.Explains;

namespace GenericIdentity.A_TheProblem;

[CodeExample]
public class Student
{
    public Guid Id { get; }
    public string Name { get; }
    public Student(string name) => (Id, Name) = (Guid.NewGuid(), name);

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
