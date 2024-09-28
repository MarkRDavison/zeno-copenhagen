namespace zeno_copenhagen.Prototypes;

public sealed class BuildingPrototype : IPrototype
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string TextureName { get; set; } = string.Empty;
    public Vector2 Size { get; set; }
    public List<ProvidedJob> ProvidedJobs { get; } = [];
    public List<RequiredWorker> RequiredWorkers { get; } = [];

}

public record ProvidedJob(string Name, Vector2 Offset);
public record RequiredWorker(string Name, int Count);
