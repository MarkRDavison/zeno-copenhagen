namespace zeno_copenhagen.Services.Terrain;

public class TerrainModificationService : ITerrainModificationService
{
    private readonly IGameData _gameData;

    public TerrainModificationService(IGameData gameData)
    {
        _gameData = gameData;
    }

    public bool EnsureTilesExistIncluding(Vector2 tilePosition)
    {
        if (_gameData.Terrain.Level < tilePosition.Y)
        {
            return false;
        }

        if (tilePosition.X == 0)
        {
            return false;
        }

        var row = _gameData.Terrain.Rows[(int)tilePosition.Y];

        var x = Math.Abs(tilePosition.X) - 1;

        List<Tile> tiles;
        if (tilePosition.X > 0)
        {
            tiles = row.RightTiles;
        }
        else
        {
            tiles = row.LeftTiles;
        }

        while (tiles.Count <= x)
        {
            tiles.Add(new Tile { TileName = "DIRT" /*TODO Constant for default*/ });
        }

        return true;
    }
}
