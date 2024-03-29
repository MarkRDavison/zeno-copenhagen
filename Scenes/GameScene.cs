namespace zeno_copenhagen.Scenes;

public class GameScene : IScene
{
    private readonly TerrainView _terrainView;
    private readonly ShuttleView _shuttleView;
    private readonly IShuttleScheduleService _shuttleScheduleService;

    public GameScene(
        TerrainView terrainView,
        ShuttleView shuttleView,
        IShuttleScheduleService shuttleScheduleService)
    {
        _terrainView = terrainView;
        _shuttleView = shuttleView;
        _shuttleScheduleService = shuttleScheduleService;
    }

    public void Initialise()
    {
        _terrainView.Initialise();
        _shuttleView.Initialise();
    }

    public void Update(TimeSpan delta)
    {
        _shuttleScheduleService.Update(delta);



        _terrainView.Update(delta);
        _shuttleView.Update(delta);
    }

    public void Draw(TimeSpan delta, Matrix camera)
    {
        _terrainView.Draw(delta, camera);
        _shuttleView.Draw(delta, camera);
    }

}
