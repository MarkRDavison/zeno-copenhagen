namespace zeno_copenhagen.Commands.AddResource;

public class AddResourceCommand : IGameCommand
{
    public required string Name { get; set; }
    public required int Amount { get; set; }
}
