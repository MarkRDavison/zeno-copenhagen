namespace zeno_copenhagen.Prototypes;

public sealed class JobPrototype : IPrototype
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool Repeats { get; set; }
    public Func<Vector2, float> Work { get; set; } = _ => 1.0f;
    public Action<IServiceProvider, Job>? OnWorkComplete { get; set; }
}
