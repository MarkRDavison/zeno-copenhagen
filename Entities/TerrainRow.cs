namespace zeno_copenhagen.Entities;

public sealed class TerrainRow
{
    public List<Tile> LeftTiles { get; } = new();
    public List<Tile> RightTiles { get; } = new();
}
