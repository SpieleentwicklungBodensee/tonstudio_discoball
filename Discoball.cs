using System;
using Godot;

namespace TonstudioDiscoball;

public partial class Discoball : Node {

    [Export] private Curve _curve;
    
    private CanvasLayer _ui;
    private Sprite2D _circle;
    private DiscoShape _rect;
    private ConfigWindow _configWindow;
    
    private bool _uiMode = true;
    private double _totalDelta;
    private float _rectOffset;
    private float _rectHeight;

    public override void _Ready() {
        GetViewport().TransparentBg = true;
        _ui = GetNode<CanvasLayer>("UI");
        _circle = GetNode<Sprite2D>("Circle");
        _rect = GetNode<DiscoShape>("Circle/Rect");
        _configWindow = GetNode<ConfigWindow>("ConfigWindow");
        _configWindow.Visible = true;
        _configWindow.MoveToForeground();
        _configWindow.GrabFocus();
    }
// 120   0.36    44
    public override void _PhysicsProcess(double delta) {
        if (_uiMode) return;
        _totalDelta += delta;
        var targetDelta = 60.0 / DiscoConfig.CurrentConfig.Bpm;
        if (_totalDelta < targetDelta) {
            var curvedDelta = _curve.Sample((float)_totalDelta);
            var currentRectPosition = Tween.InterpolateValue(
                -_rectOffset,
                _rectOffset*2 + _rectHeight,
                curvedDelta,
                // _totalDelta,
                DiscoConfig.CurrentConfig.AnimationDuration, 
                Tween.TransitionType.Linear, 
                Tween.EaseType.InOut);

            _rect.Position = new Vector2(_rect.Position.X, (float)currentRectPosition);
        } else {
            _totalDelta -= targetDelta;
            
            _rect.SetRandomColor();
            var circleHeight = _circle.Texture.GetHeight();
            _rectHeight = circleHeight / 100.0f * DiscoConfig.CurrentConfig.BarHeight;
            _rect.Scale = new Vector2(_rect.Scale.X, _rectHeight);
            _rectOffset = circleHeight/2.0f + _rectHeight/2.0f;
            _rect.Position = new Vector2(_rect.Position.X, -_rectOffset);
        }
    }

    public override void _Input(InputEvent ev) {
        if (ev.IsActionPressed("Quit")) {
            GetTree().Quit();
        }
        if (ev.IsActionPressed("UiMode")) {
            _uiMode = !_uiMode;
            _ui.Visible = _uiMode;
            if (_uiMode) {
                _circle.SelfModulate = Colors.White;
                _totalDelta = 0;
                _configWindow.Show();
            } else {
                _circle.SelfModulate = Colors.Black;
            }
        }
    }

}
