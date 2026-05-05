using QuickPulse.Explains;

namespace GenericIdentity.B_TheSolution.Step3;

[CodeExample]
public class Student(string name) : DomainEntity<Student>
{
    public string Name { get; } = name;
}