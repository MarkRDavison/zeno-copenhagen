namespace zeno_copenhagen.Prototypes;

public sealed class JobPrototype : IPrototype
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool Repeats { get; set; }
    public float Work { get; set; }
}
