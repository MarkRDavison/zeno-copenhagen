namespace zeno_copenhagen.Commands.DigShaft;

public sealed class DigShaftCommandHandler : IGameCommandHandler<DigShaftCommand>
{
    private readonly IGameData _gameData;

    public DigShaftCommandHandler(IGameData gameData)
    {
        _gameData = gameData;
    }

    public bool Handle(DigShaftCommand command)
    {
        _gameData.Terrain.IncrementLevel();
        return true;
    }
}
