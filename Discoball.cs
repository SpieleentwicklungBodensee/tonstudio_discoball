using System;
using Godot;

namespace TonstudioDiscoball;

public partial class Discoball : Node {

    private CanvasLayer _ui;
    private Sprite2D _circle;
    private DiscoShape _rect;
    private AnimationPlayer _animation;
    private ConfigWindow _configWindow;
    
    private bool _uiMode = true;
    private double _totalDelta;

    public override void _Ready() {
        GetViewport().TransparentBg = true;
        _ui = GetNode<CanvasLayer>("UI");
        _circle = GetNode<Sprite2D>("Circle");
        _rect = GetNode<DiscoShape>("Circle/Rect");
        _animation = GetNode<AnimationPlayer>("AnimationPlayer");
        _configWindow = GetNode<ConfigWindow>("ConfigWindow");
        _configWindow.Visible = true;
        _configWindow.MoveToForeground();
        _configWindow.GrabFocus();
    }

    public override void _PhysicsProcess(double delta) {
        if (_uiMode) return;
        _totalDelta += delta;
        var targetDelta = 60.0 / DiscoConfig.CurrentConfig.Bpm;
        if (_totalDelta < targetDelta) return;

        _totalDelta -= targetDelta;
        PlayAnimation();
    }

    private void PlayAnimation() {
        _rect.Position = Vector2.Zero;
        _rect.SetRandomColor();
        var circleHeight = _circle.Texture.GetHeight();
        var rectHeight = circleHeight / 100.0f * DiscoConfig.CurrentConfig.BarHeight;
        _rect.Scale = new Vector2(_rect.Scale.X, rectHeight);
        var rectOffset = circleHeight/2.0f + rectHeight/2.0f;
        _rect.Position = new Vector2(_rect.Position.X, -rectOffset);
        CreateTween().TweenProperty(_rect, "position:y", rectOffset, DiscoConfig.CurrentConfig.AnimationDuration);
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
                _configWindow.Show();
            } else {
                _circle.SelfModulate = Colors.Black;
            }
        }
    }

}
