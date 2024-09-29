namespace zeno_copenhagen.Commands.DigTile;

public sealed class DigTileCommandHandler : IGameCommandHandler<DigTileCommand>
{
    private readonly IGameData _gameData;
    private readonly ITerrainModificationService _terrainModificationService;

    public DigTileCommandHandler(
        IGameData gameData,
        ITerrainModificationService terrainModificationService)
    {
        _gameData = gameData;
        _terrainModificationService = terrainModificationService;
    }

    public bool Handle(DigTileCommand command)
    {
        if (_terrainModificationService.EnsureTilesExistIncluding(new Vector2(command.Column, command.Level)) &&
            _gameData.Terrain.GetTile(command.Level, command.Column) is { } tile)
        {
            tile.DugOut = true;
            tile.TileName = "EMPTY";/*TODO Constant for default*/
            tile.JobReserved = false;

            return true;
        }

        return false;
    }
}
