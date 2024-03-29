namespace zeno_copenhagen.Views;

public class TerrainView : BaseView
{
    private SpriteBatch _spriteBatch;
    private readonly IGameData _gameData;

    public TerrainView(
        IGameData gameData,
        IResourceService resourceService,
        ISpriteSheetService spriteSheetService) :
    base(
        resourceService,
        spriteSheetService)
    {
        _gameData = gameData;
        _spriteBatch = _resourceService.CreateSpriteBatch();
    }


    public override void Draw(TimeSpan delta, Matrix camera)
    {
        _spriteBatch.Begin(transformMatrix: camera);

        int level;
        for (level = 0; level <= _gameData.Terrain.Level; level++)
        {
            DrawTileAlignedSpriteCell(_spriteBatch, "LADDER", new Vector2(0, level));
            var currentRow = _gameData.Terrain.Rows[level];
            for (int rightX = 0; rightX < currentRow.RightTiles.Count; ++rightX)
            {
                var tile = currentRow.RightTiles[rightX];
                DrawTileAlignedSpriteCell(_spriteBatch, tile.TileName, new Vector2(rightX + 1, level));
            }
            for (int leftX = 0; leftX < currentRow.LeftTiles.Count; ++leftX)
            {
                var tile = currentRow.RightTiles[leftX];
                DrawTileAlignedSpriteCell(_spriteBatch, tile.TileName, new Vector2(-leftX - 1, level));
            }
        }

        DrawTileAlignedSpriteCell(_spriteBatch, "LADDER_DRILL", new Vector2(0, level));

        _spriteBatch.End();
    }

    public override void Update(TimeSpan delta)
    {
    }

}
