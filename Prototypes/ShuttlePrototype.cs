namespace zeno_copenhagen.Prototypes;

public class ShuttlePrototype : IPrototype
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string TextureName { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public double LoadingTime { get; set; }
    public double IdleTime { get; set; }
    public double Speed { get; set; }
}
