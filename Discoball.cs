using System;
using Godot;

namespace TonstudioDiscoball;

public partial class Discoball : Node {

    private CanvasLayer _ui;
    private Sprite2D _circle;
    private AnimationPlayer _animation;
    private bool _uiMode = true;
    private double _totalDelta;

    public override void _Ready() {
        GetViewport().TransparentBg = true;
        _ui = GetNode<CanvasLayer>("UI");
        _circle = GetNode<Sprite2D>("Circle");
        _animation = GetNode<AnimationPlayer>("AnimationPlayer");
    }

    public override void _PhysicsProcess(double delta) {
        if (_uiMode) return;
        _totalDelta += delta;
        var targetDelta = 60.0 / DiscoConfig.CurrentConfig.Bpm;
        if (_totalDelta < targetDelta) return;

        _totalDelta -= targetDelta;
        // _animation.Play("ColorChange");
        _animation.Play("PulseDown");
    }

    public override void _Input(InputEvent ev) {
        if (ev.IsActionPressed("Quit")) {
            GetTree().Quit();
        }
        if (ev.IsActionPressed("UiMode")) {
            _uiMode = !_uiMode;
            _ui.Visible = _uiMode;
            if (_uiMode) {
                _animation.Stop();
                _circle.SelfModulate = Colors.White;
                _totalDelta = 0;
            } else {
                _circle.SelfModulate = Colors.Black;
            }
        }
    }

}
