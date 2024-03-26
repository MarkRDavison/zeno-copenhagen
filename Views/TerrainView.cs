namespace zeno_copenhagen.Views;

public class TerrainView : BaseView
{
    private readonly IGameData _gameData;

    public TerrainView(IGameData gameData)
    {
        _gameData = gameData;
    }

    public override void Draw(TimeSpan delta)
    {
        throw new NotImplementedException();
    }

    public override void Update(TimeSpan delta)
    {
        throw new NotImplementedException();
    }
}
