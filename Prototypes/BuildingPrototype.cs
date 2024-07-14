namespace zeno_copenhagen.Prototypes;

public sealed class BuildingPrototype : IPrototype
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string TextureName { get; set; } = string.Empty;
    public Vector2 Size { get; set; }
}
