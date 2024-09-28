namespace zeno_copenhagen.Infrastructure;

public class GameCamera : IGameCamera
{
    private readonly GraphicsDeviceManager _graphics;

    public GameCamera(GraphicsDeviceManager graphics)
    {
        _graphics = graphics;
        Translation = new Vector2(0, -128);
    }

    public void Move(Vector2 offset)
    {
        Translation += offset;
    }

    public Vector2 Translation { get; private set; }

    public Vector2 GetTargetPixel() => -Translation - new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight) / 2;
}
