namespace zeno_copenhagen.Commands.DigTile;

public class DigTileCommand : IGameCommand
{
    public required int Level { get; set; }
    public required int Column { get; set; }
}
