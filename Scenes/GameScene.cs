namespace zeno_copenhagen.Scenes;

public sealed class GameScene : IScene
{
    private readonly TerrainView _terrainView;
    private readonly ShuttleView _shuttleView;
    private readonly BuildingView _buildingView;
    private readonly UiView _uiView;
    private readonly IShuttleScheduleService _shuttleScheduleService;

    public GameScene(
        TerrainView terrainView,
        ShuttleView shuttleView,
        BuildingView buildingView,
        UiView uiView,
        IShuttleScheduleService shuttleScheduleService)
    {
        _terrainView = terrainView;
        _shuttleView = shuttleView;
        _buildingView = buildingView;
        _uiView = uiView;
        _shuttleScheduleService = shuttleScheduleService;
    }

    public void Initialise()
    {
        _terrainView.Initialise();
        _shuttleView.Initialise();
        _buildingView.Initialise();
        _uiView.Initialise();
    }

    public void Update(TimeSpan delta)
    {
        _shuttleScheduleService.Update(delta);

        _uiView.Update(delta);

        _terrainView.Update(delta);
        _shuttleView.Update(delta);
        _buildingView.Update(delta);
    }

    public void Draw(TimeSpan delta, Matrix camera)
    {
        _terrainView.Draw(delta, camera);
        _shuttleView.Draw(delta, camera);
        _buildingView.Draw(delta, camera);
        _uiView.Draw(delta, Matrix.Identity);
    }

}
