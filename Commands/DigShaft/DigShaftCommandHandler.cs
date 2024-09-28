namespace zeno_copenhagen.Commands.DigShaft;

public sealed class DigShaftCommandHandler : IGameCommandHandler<DigShaftCommand>
{
    const int ResourceCost = 100;
    const string ResourceName = "Resource_Gold";

    private readonly IGameData _gameData;
    private readonly IGameResourceService _gameResourceService;

    public DigShaftCommandHandler(
        IGameData gameData,
        IGameResourceService gameResourceService)
    {
        _gameData = gameData;
        _gameResourceService = gameResourceService;
    }

    public bool Handle(DigShaftCommand command)
    {
        if (CanHandle(command))
        {
            _gameResourceService.ReduceResource(ResourceName, ResourceCost);
            _gameData.Terrain.IncrementLevel();
            return true;
        }

        return false;
    }


    public bool CanHandle(DigShaftCommand command)
    {
        return _gameResourceService.GetResource(ResourceName) >= ResourceCost;
    }
}
