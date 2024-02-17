using System;
using Godot;

namespace TonstudioDiscoball;

public partial class Discoball : Node {

    private CanvasLayer _ui;

    public override void _Ready() {
        GetViewport().TransparentBg = true;
        _ui = GetNode<CanvasLayer>("UI");
    }

    public override void _Input(InputEvent ev) {
        if (ev.IsActionPressed("Quit")) {
            GetTree().Quit();
        }
        if (ev.IsActionPressed("UiMode")) {
            _ui.Visible = !_ui.Visible;
        }
    }

}
