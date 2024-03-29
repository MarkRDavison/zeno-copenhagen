namespace zeno_copenhagen.Commands.CreateShuttle;

public class CreateShuttleCommand : IGameCommand
{
    public CreateShuttleCommand(string prototypeName) : this(StringHash.Hash(prototypeName))
    {

    }

    public CreateShuttleCommand(Guid prototypeId)
    {
        PrototypeId = prototypeId;
    }

    public Guid PrototypeId { get; }
}
