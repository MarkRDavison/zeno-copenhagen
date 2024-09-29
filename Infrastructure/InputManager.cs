namespace zeno_copenhagen.Infrastructure;

public class InputManager : IInputManager
{
    private KeyboardState _prevKeyboardState;
    private KeyboardState _keyboardState;

    private MouseState _prevMouseState;
    private MouseState _mouseState;

    public InputManager()
    {
        _keyboardState = Keyboard.GetState();
        _mouseState = Mouse.GetState();
    }

    public void CacheInputs()
    {
        _prevKeyboardState = _keyboardState;
        _keyboardState = Keyboard.GetState();
        _prevMouseState = _mouseState;
        _mouseState = Mouse.GetState();
    }

    public bool IsKeyDown(Keys key)
    {
        return _keyboardState.IsKeyDown(key);
    }

    public bool IsKeyPressed(Keys key)
    {
        return !_prevKeyboardState.IsKeyDown(key) && _keyboardState.IsKeyDown(key);
    }

    public void ListKeyState(Keys key)
    {
        var curr = _keyboardState.IsKeyDown(key);
        var prev = _prevKeyboardState.IsKeyDown(key);
        var force = Keyboard.GetState().IsKeyDown(key);

        Debug.WriteLine($"{key.ToString()} - Curr: {curr} - Prev: {prev} - Force: {force}");
    }

    public bool IsButtonPressed(MouseButton button)
    {
        return button switch
        {
            MouseButton.Left => _mouseState.LeftButton == ButtonState.Pressed && _prevMouseState.LeftButton != ButtonState.Pressed,
            MouseButton.Right => _mouseState.RightButton == ButtonState.Pressed && _prevMouseState.RightButton != ButtonState.Pressed,
            _ => false
        };
    }
    public bool IsButtonReleased(MouseButton button)
    {
        return button switch
        {
            MouseButton.Left => _mouseState.LeftButton == ButtonState.Released && _prevMouseState.LeftButton == ButtonState.Pressed,
            MouseButton.Right => _mouseState.RightButton == ButtonState.Released && _prevMouseState.RightButton == ButtonState.Pressed,
            _ => false
        };
    }

    public Vector2 GetMousePosition() => Mouse.GetState().Position.ToVector2();
    public Vector2 GetMousePosition(IGameCamera camera) => GetMousePosition() + camera.GetTargetPixel();
    public Vector2? GetTilePosition(IGameCamera camera)
    {
        var pos = GetMousePosition(camera);

        if (pos.Y < 0)
        {
            return null;
        }

        if (pos.X > 0)
        {
            return new Vector2((int)(pos.X / 64), (int)(pos.Y / 64));
        }

        return new Vector2((int)(pos.X / 64) - 1, (int)(pos.Y / 64));
    }
}