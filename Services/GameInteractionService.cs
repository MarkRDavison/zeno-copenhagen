namespace zeno_copenhagen.Services;

public class GameInteractionService : IGameInteractionService
{
    private readonly IGameData _gameData;
    private readonly IInputActionManager _inputActionManager;
    private readonly IGameCamera _gameCamera;
    private readonly IGameCommandService _gameCommandService;
    private readonly IGameResourceService _gameResourceService;
    private readonly IJobCreationService _jobCreationService;
    private readonly ITerrainModificationService _terrainModificationService;

    public GameInteractionService(
        IGameData gameData,
        IInputActionManager inputActionManager,
        IGameCamera gameCamera,
        IGameCommandService gameCommandService,
        IGameResourceService gameResourceService,
        IJobCreationService jobCreationService,
        ITerrainModificationService terrainModificationService)
    {
        _gameData = gameData;
        _inputActionManager = inputActionManager;
        _gameCamera = gameCamera;
        _gameCommandService = gameCommandService;
        _gameResourceService = gameResourceService;
        _jobCreationService = jobCreationService;
        _terrainModificationService = terrainModificationService;
    }
    public void Update(TimeSpan delta)
    {
        if (_inputActionManager.IsActionInvoked("LCLICK"))
        {
            if (IsMouseOverDrill() && CanDrillLevel())
            {
                _gameCommandService.Execute<DigShaftCommand>(new(new()));
            }
            else if (State == UiState.Dig)
            {
                if (_inputActionManager.GetTilePosition(_gameCamera) is { } tilePosition &&
                    tilePosition.Y <= _gameData.Terrain.Level)
                {
                    var tile = _gameData.Terrain.GetTile((int)tilePosition.Y, (int)tilePosition.X);

                    if (tile is null || !tile.DugOut)
                    {
                        var digAll =
                            _inputActionManager.IsKeyDown(Keys.LeftShift) ||
                            _inputActionManager.IsKeyDown(Keys.RightShift);

                        if (_terrainModificationService.EnsureTilesExistIncluding(tilePosition))
                        {
                            var sign = Math.Sign(tilePosition.X);

                            for (int column = (int)tilePosition.X; column != 0; column -= sign)
                            {
                                var currentTile = _gameData.Terrain.GetTile((int)tilePosition.Y, column);
                                if (currentTile is null || currentTile.DugOut)
                                {
                                    break;
                                }

                                if (currentTile.JobReserved)
                                {
                                    continue;
                                }

                                _jobCreationService.CreateJob(
                                    StringHash.Hash("Job_Dig"),
                                    new(),
                                    new Vector2(column, tilePosition.Y));

                                if (!digAll)
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
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

    public UiState State { get; set; }
}
