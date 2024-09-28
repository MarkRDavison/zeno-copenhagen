namespace zeno_copenhagen.Views;

public sealed class WorkerView : BaseView
{
    private SpriteBatch _spriteBatch;
    private readonly IGameData _gameData;
    private readonly IPrototypeService<WorkerPrototype, Worker> _workerPrototypeService;

    public WorkerView(
        IGameData gameData,
        IResourceService resourceService,
        ISpriteSheetService spriteSheetService,
        IPrototypeService<WorkerPrototype, Worker> workerPrototypeService
    ) : base(
        resourceService,
        spriteSheetService)
    {
        _gameData = gameData;
        _spriteBatch = _resourceService.CreateSpriteBatch();
        _workerPrototypeService = workerPrototypeService;
    }

    public override void Update(TimeSpan delta)
    {

    }

    public override void Draw(TimeSpan delta, Matrix camera)
    {
        _spriteBatch.Begin(transformMatrix: camera);

        foreach (var worker in _gameData.Worker.Workers)
        {
            var prototype = _workerPrototypeService.GetPrototype(worker.PrototypeId);

            DrawSpriteCell(_spriteBatch, prototype.TextureName, worker.Position);
        }

        _spriteBatch.End();
    }
}
