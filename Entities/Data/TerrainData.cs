namespace zeno_copenhagen.Entities.Data;

public class TerrainData
{
    public List<TerrainRow> Rows { get; } = new();
    public int Level { get; private set; }
}
