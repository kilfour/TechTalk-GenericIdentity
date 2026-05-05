using QuickPulse.Explains;

namespace GenericIdentity.B_TheSolution.Step2;

[CodeExample]
public record Id<T>(Guid Value)
{
    public override string ToString()
    {
        return $"{typeof(T).Name}: {Value}";
    }
}