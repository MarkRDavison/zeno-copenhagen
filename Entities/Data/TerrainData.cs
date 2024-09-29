namespace zeno_copenhagen.Entities.Data;

public sealed class TerrainData
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

    public bool CanReachPosition(Vector2 position)
    {
        var column = (int)position.X;
        var level = (int)position.Y;

        if ((column) == 0)
        {
            return true;
        }

        if (Rows.Count > level)
        {
            var row = Rows[level];
            if (column < 0)
            {
                column = Math.Abs(column + 1);

                if (row.LeftTiles.Count > column &&
                    row.LeftTiles.Take(column).All(_ => _.DugOut))
                {
                    return true;
                }
            }
            else
            {
                column = column - 1;
                if (row.RightTiles.Count > column &&
                    row.RightTiles.Take(column).All(_ => _.DugOut))
                {
                    return true;
                }
            }
        }

        return false;
    }

    public Tile? GetTile(int level, int column)
    {
        if (column == 0)
        {
            return null;
        }

        if (Rows.Count > level)
        {
            var row = Rows[level];
            if (column < 0)
            {
                column = Math.Abs(column + 1);
                if (row.LeftTiles.Count > column)
                {
                    return row.LeftTiles[column];
                }
            }
            else
            {
                column = column - 1;
                if (row.RightTiles.Count > column)
                {
                    return row.RightTiles[column];
                }
            }
        }

        return null;
    }
}
