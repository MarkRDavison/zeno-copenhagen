namespace zeno_copenhagen.Commands.CreateShuttle;

public sealed class CreateShuttleCommandHandler : IGameCommandHandler<CreateShuttleCommand>
{
    private readonly IGameData _gameData;

    private readonly IPrototypeService<ShuttlePrototype, Shuttle> _shuttlePrototypeService;

    public CreateShuttleCommandHandler(
        IGameData gameData,
        IPrototypeService<ShuttlePrototype, Shuttle> shuttlePrototypeService)
    {
        _gameData = gameData;
        _shuttlePrototypeService = shuttlePrototypeService;
    }


    public bool Handle(CreateShuttleCommand command)
    {
        if (!_shuttlePrototypeService.IsPrototypeRegistered(command.PrototypeId))
        {
            return false;
        }

        var shuttle = _shuttlePrototypeService.CreateEntity(command.PrototypeId);

        _gameData.Shuttle.Shuttles.Add(shuttle);

        return true;
    }
}
