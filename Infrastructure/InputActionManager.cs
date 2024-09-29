namespace zeno_copenhagen.Infrastructure;

public sealed class InputActionManager : InputManager, IInputActionManager
{
    private readonly Dictionary<string, InputAction> _actions = [];


    public void RegisterAction(string name, InputAction action)
    {
        _actions.Add(name, action);
    }

    public bool IsActionInvoked(string name)
    {
        if (name == "LCLICK")
        {
            if (IsButtonPressed(MouseButton.Left))
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
