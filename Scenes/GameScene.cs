namespace zeno_copenhagen.Scenes;

public sealed class GameScene : IScene
{
    private readonly TerrainView _terrainView;
    private readonly ShuttleView _shuttleView;
    private readonly BuildingView _buildingView;
    private readonly JobView _jobView;
    private readonly WorkerView _workerView;
    private readonly UiView _uiView;
    private readonly IShuttleScheduleService _shuttleScheduleService;
    private readonly IJobAllocationService _jobAllocationService;
    private readonly IWorkerMovementService _workerMovementService;

    public GameScene(
        TerrainView terrainView,
        ShuttleView shuttleView,
        BuildingView buildingView,
        JobView jobView,
        WorkerView workerView,
        UiView uiView,
        IShuttleScheduleService shuttleScheduleService,
        IJobAllocationService jobAllocationService,
        IWorkerMovementService workerMovementService)
    {
        _terrainView = terrainView;
        _shuttleView = shuttleView;
        _buildingView = buildingView;
        _jobView = jobView;
        _workerView = workerView;
        _uiView = uiView;
        _shuttleScheduleService = shuttleScheduleService;
        _jobAllocationService = jobAllocationService;
        _workerMovementService = workerMovementService;
    }

    public void Initialise()
    {
        _terrainView.Initialise();
        _shuttleView.Initialise();
        _buildingView.Initialise();
        _jobView.Initialise();
        _workerView.Initialise();
        _uiView.Initialise();
    }

    public void Update(TimeSpan delta)
    {
        _shuttleScheduleService.Update(delta);
        _jobAllocationService.AllocateJobs(10);
        _workerMovementService.Update(delta);

        _uiView.Update(delta);

        _terrainView.Update(delta);
        _shuttleView.Update(delta);
        _buildingView.Update(delta);
        _jobView.Update(delta);
        _workerView.Update(delta);
    }

    public void Draw(TimeSpan delta, Matrix camera)
    {
        _terrainView.Draw(delta, camera);
        _shuttleView.Draw(delta, camera);
        _buildingView.Draw(delta, camera);
        _jobView.Draw(delta, camera);
        _workerView.Draw(delta, camera);
        _uiView.Draw(delta, Matrix.Identity);
    }

}
