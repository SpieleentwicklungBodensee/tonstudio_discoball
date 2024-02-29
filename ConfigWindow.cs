using Godot;

namespace TonstudioDiscoball;

public partial class ConfigWindow : Window {

    public override void _Ready() {
        MoveToForeground();
    }

    private void _OnCloseRequested() {
        Hide();
    }
}
