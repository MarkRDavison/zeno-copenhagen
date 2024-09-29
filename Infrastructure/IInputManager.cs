namespace zeno_copenhagen.Infrastructure;

public enum MouseButton
{
    Left,
    Right
}

public interface IInputManager
{
    void CacheInputs();

    bool IsKeyDown(Keys key);
    bool IsKeyPressed(Keys key);

    void ListKeyState(Keys key);

    bool IsButtonPressed(MouseButton button);
    bool IsButtonReleased(MouseButton button);
    Vector2 GetMousePosition();
    Vector2 GetMousePosition(IGameCamera camera);
    Vector2? GetTilePosition(IGameCamera camera);
}
