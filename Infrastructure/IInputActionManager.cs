namespace zeno_copenhagen.Infrastructure;

public enum InputType
{
    Key,
    Mouse
}

public readonly struct InputAction
{
    public required InputType Type { get; init; }
    public Keys Key { get; init; }
    public MouseButton Button { get; init; }
}

public interface IInputActionManager : IInputManager
{
    void RegisterAction(string name, InputAction action);

    bool IsActionInvoked(string name);
}
