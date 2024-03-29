namespace zeno_copenhagen.Entities.Data;

public class TerrainData
{
    public TerrainData()
    {
        Rows.Add(new TerrainRow());
    }
    public List<TerrainRow> Rows { get; } = new();
    public int Level { get; private set; }
    public void IncrementLevel()
    {
        Rows.Add(new TerrainRow());
        Level++;
    }
}
