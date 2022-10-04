
public interface ISideIdentifier
{
    public string Identifier { get; }
    public bool Compare(ISideIdentifier sideIdentifier);
}
