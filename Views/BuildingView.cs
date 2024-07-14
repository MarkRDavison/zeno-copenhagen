namespace zeno_copenhagen.Views;

public sealed class BuildingView : BaseView
{
    private SpriteBatch _spriteBatch;
    private readonly IGameData _gameData;
    private readonly IPrototypeService<BuildingPrototype, Building> _buildingPrototypeService;

    public BuildingView(
        IGameData gameData,
        IResourceService resourceService,
        ISpriteSheetService spriteSheetService,
        IPrototypeService<BuildingPrototype, Building> buildingPrototypeService
    ) : base(
        resourceService,
        spriteSheetService)
    {
        _gameData = gameData;
        _spriteBatch = _resourceService.CreateSpriteBatch();
        _buildingPrototypeService = buildingPrototypeService;
    }

    public override void Update(TimeSpan delta)
    {
    }

    public override void Draw(TimeSpan delta, Matrix camera)
    {
        _spriteBatch.Begin(transformMatrix: camera);

        foreach (var building in _gameData.Building.Buildings)
        {
            var prototype = _buildingPrototypeService.GetPrototype(building.PrototypeId);

            DrawTileAlignedSpriteCell(_spriteBatch, prototype.TextureName, building.Position);
        }

        _spriteBatch.End();
    }
}
