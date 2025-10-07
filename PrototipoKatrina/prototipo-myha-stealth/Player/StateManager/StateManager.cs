using Godot;

namespace PrototipoMyha.Player.StateManager;

public partial class StateManager : Node
{
    private static SignalManager _instance;
    public override void _Ready()
    {
        _instance = SignalManager.Instance;
    }
}
