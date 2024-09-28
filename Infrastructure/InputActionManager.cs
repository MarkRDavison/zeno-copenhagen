namespace zeno_copenhagen.Infrastructure;

public sealed class InputActionManager : InputManager, IInputActionManager
{
    private readonly IInputManager _inputManager;
    private readonly Dictionary<string, InputAction> _actions = [];

    public InputActionManager(IInputManager inputManager)
    {
        _inputManager = inputManager;
    }

    public void RegisterAction(string name, InputAction action)
    {
        _actions.Add(name, action);
    }

    public bool IsActionInvoked(string name)
    {
        if (name == "LCLICK")
        {
            if (_inputManager.IsButtonPressed(MouseButton.Left))
            {
                return true;
            }
        }

        if (!_actions.TryGetValue(name, out var action))
        {
            return false;
        }

        return false;
    }
}
