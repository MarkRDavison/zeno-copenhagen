namespace zeno_copenhagen.Services;

public class GameInteractionService : IGameInteractionService
{
    private readonly IGameData _gameData;
    private readonly IInputActionManager _inputActionManager;
    private readonly IGameCamera _gameCamera;
    private readonly IGameCommandService _gameCommandService;
    private readonly IGameResourceService _gameResourceService;

    public GameInteractionService(
        IGameData gameData,
        IInputActionManager inputActionManager,
        IGameCamera gameCamera,
        IGameCommandService gameCommandService,
        IGameResourceService gameResourceService)
    {
        _gameData = gameData;
        _inputActionManager = inputActionManager;
        _gameCamera = gameCamera;
        _gameCommandService = gameCommandService;
        _gameResourceService = gameResourceService;
    }
    public void Update(TimeSpan delta)
    {
        if (_inputActionManager.IsActionInvoked("LCLICK"))
        {
            if (IsMouseOverDrill() && CanDrillLevel())
            {
                _gameCommandService.Execute<DigShaftCommand>(new(new()));
            }
        }
    }

    public bool IsMouseOverDrill()
    {
        return _inputActionManager.GetTilePosition(_gameCamera) == new Vector2(0, _gameData.Terrain.Rows.Count);
    }

    public bool CanDrillLevel()
    {
        return _gameCommandService.CanExecute<DigShaftCommand>(new(new()));
    }
}
