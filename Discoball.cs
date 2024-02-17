using System;
using Godot;

namespace TonstudioDiscoball;

public partial class Discoball : Node {

    private readonly Random _random = new();
    private CanvasLayer _ui;
    private Sprite2D _circle;
    private bool _uiMode = true;
    private double _totalDelta;
    private const double TickRateInSeconds = 1;

    public override void _Ready() {
        GetViewport().TransparentBg = true;
        _ui = GetNode<CanvasLayer>("UI");
        _circle = GetNode<Sprite2D>("Circle");
    }

    public override void _PhysicsProcess(double delta) {
        if (_uiMode) return;
        _totalDelta += delta;
        if (_totalDelta < TickRateInSeconds) return;

        _totalDelta = 0;
        _circle.Modulate = new Color(RandomBrightColor(), RandomBrightColor(), RandomBrightColor());
    }

    private float RandomBrightColor() {
        return _random.NextSingle() / 2 + .5f;
    }

    public override void _Input(InputEvent ev) {
        if (ev.IsActionPressed("Quit")) {
            GetTree().Quit();
        }
        if (ev.IsActionPressed("UiMode")) {
            _uiMode = !_uiMode;
            _ui.Visible = _uiMode;
            if (_uiMode) {
                _circle.Modulate = Colors.White;
            }
        }
    }

}
