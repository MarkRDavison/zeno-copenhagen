namespace zeno_copenhagen.Views;

public sealed class JobView : BaseView
{
    private readonly SpriteBatch _spriteBatch;
    private readonly IGameData _gameData;
    private readonly IPrototypeService<JobPrototype, Job> _jobPrototypeService;

    public JobView(
        IGameData gameData,
        IResourceService resourceService,
        ISpriteSheetService spriteSheetService,
        IPrototypeService<JobPrototype, Job> jobPrototypeService
    ) : base(
        resourceService,
        spriteSheetService)
    {
        _gameData = gameData;
        _spriteBatch = _resourceService.CreateSpriteBatch();
        _jobPrototypeService = jobPrototypeService;
    }

    public override void Update(TimeSpan delta)
    {

    }

    public override void Draw(TimeSpan delta, Matrix camera)
    {
        _spriteBatch.Begin(transformMatrix: camera);

        foreach (var job in _gameData.Job.Jobs)
        {
            DrawTileAlignedSpriteCell(
                _spriteBatch,
                "JOB_CIRCLE",
                job.TileCoords + job.Offset,
                job.AllocatedWorkerId is null
                    ? Color.Yellow
                    : Color.Blue);
        }

        _spriteBatch.End();
    }
}
