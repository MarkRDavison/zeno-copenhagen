namespace zeno_copenhagen.Prototypes;

public sealed class WorkerPrototype : IPrototype
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string TextureName { get; set; } = string.Empty;
    public HashSet<string> Jobs { get; set; } = [];
    public float Speed { get; set; }
}
