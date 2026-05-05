using QuickPulse.Explains;

namespace GenericIdentity.B_TheSolution.Step3;

[CodeExample]
public abstract class DomainEntity<T>
{
    // Simplifying Id assignment for demo purposes.
    // In the wild, this is likely handled by a data store.
    public Id<T> Id { get; } = new Id<T>(Guid.NewGuid());

    public override bool Equals(object? obj)
    {
        if (obj is null)
            return false;
        if (obj.GetType() != GetType())
            return false;
        var other = (DomainEntity<T>)obj;
        return Id == other.Id;
    }

    public override int GetHashCode()
        => Id.GetHashCode();
}
