namespace zeno_copenhagen.Infrastructure;

public struct InputAction
{
}

public interface IInputActionManager : IInputManager
{
    void RegisterAction(string name, InputAction action);

    bool IsActionInvoked(string name);
}
