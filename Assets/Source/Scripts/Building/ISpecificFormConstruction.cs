
public interface ISpecificFormConstruction : IConstruction
{
    public SidesPack SidesPack { get; }
    public ISideIdentifier GetWorldSideIdentifier(SideName sideName);
    public void SetSides(SidesPack sidesPack);
}
