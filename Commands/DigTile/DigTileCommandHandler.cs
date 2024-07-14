namespace zeno_copenhagen.Commands.DigTile;

public sealed class DigTileCommandHandler : IGameCommandHandler<DigTileCommand>
{
    private readonly IGameData _gameData;

    public DigTileCommandHandler(IGameData gameData)
    {
        _gameData = gameData;
    }

    public bool Handle(DigTileCommand command)
    {
        if (_gameData.Terrain.Level < command.Level)
        {
            return false;
        }

        if (command.Column == 0)
        {
            return false;
        }

        var row = _gameData.Terrain.Rows[command.Level];

        var x = Math.Abs(command.Column) - 1;

        List<Tile> tiles;
        if (command.Column > 0)
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

        tiles[x].DugOut = true;
        tiles[x].TileName = "EMPTY";/*TODO Constant for default*/

        return true;
    }
}
