namespace zeno_copenhagen.Entities;

public sealed class Tile
{
    public required string TileName { get; set; }
    public bool DugOut { get; set; }
    public bool JobReserved { get; set; }
    public bool HasBuilding { get; set; }
}
