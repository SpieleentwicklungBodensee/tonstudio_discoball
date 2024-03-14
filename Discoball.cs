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
    private bool _animationPlaying;
    private double _animationDelta;
    private double _animationTimingDelta;
    private float _rectOffset;
    private float _rectHeight;
    private double _animationDuration;
    private int _midiClocks;
    private bool _midiAnimationTriggered;

    public override void _Ready() {
        GetViewport().TransparentBg = true;
        _ui = GetNode<CanvasLayer>("UI");
        _circle = GetNode<Sprite2D>("Circle");
        _rect = GetNode<DiscoShape>("Circle/Rect");
        _configWindow = GetNode<ConfigWindow>("ConfigWindow");
        _configWindow.Visible = true;
        _configWindow.MoveToForeground();
        _configWindow.GrabFocus();
        OS.OpenMidiInputs();
        Console.WriteLine("midi:");
        foreach (var connectedMidiInput in OS.GetConnectedMidiInputs()) {
            Console.WriteLine(connectedMidiInput);
        }
    }

    public override void _Process(double delta) {
        if (_uiMode) return;


        if (_animationPlaying) {
            Animate(delta, _animationDuration);
        } else {
            if (DiscoConfig.CurrentConfig.MidiMode) return;

            _animationTimingDelta += delta;
            var targetDelta = 60.0 / DiscoConfig.CurrentConfig.Bpm;
            if (_animationTimingDelta >= targetDelta) {
                _animationTimingDelta %= targetDelta;
                InitAnimation();
            }
        }
    }

    private ulong _lastMidiTs;

    public override void _Input(InputEvent ev) {
        if (ev.IsActionPressed("Quit")) {
            GetTree().Quit();
        }
        if (DiscoConfig.CurrentConfig.MidiMode && ev is InputEventMidi { Message: MidiMessage.Start }) {
            _midiClocks = 0;
            _midiAnimationTriggered = false;
        }
        if (DiscoConfig.CurrentConfig.MidiMode && ev is InputEventMidi { Message: MidiMessage.TimingClock }) {
            _midiClocks++;
            if (_midiClocks >= 24) {
                _midiClocks -= 24;
                _midiAnimationTriggered = false;
            }
            if (_midiClocks >= (24 - DiscoConfig.CurrentConfig.AnimationOffset) % 24 && !_midiAnimationTriggered) {
                _midiAnimationTriggered = true;
                InitAnimation();
            }
        }
        if (ev.IsActionPressed("TestButton")) {
            if (_uiMode && !DiscoConfig.CurrentConfig.MidiMode) return;
            InitAnimation();
        }
        if (ev.IsActionPressed("UiMode")) {
            _uiMode = !_uiMode;
            _ui.Visible = _uiMode;
            if (_uiMode) {
                _circle.SelfModulate = Colors.White;
                _animationTimingDelta = 0;
                _configWindow.Show();
            } else {
                _circle.SelfModulate = Colors.Black;
            }
        }
    }

    private void Animate(double delta, double animationDuration) {
        _animationDelta += delta;
        var progress = _animationDelta / animationDuration;
        var adjustedProgress = _curve.Sample((float)progress);
        var currentRectPosition = Tween.InterpolateValue(
            -_rectOffset,
            _rectOffset * 2 + _rectHeight,
            adjustedProgress * animationDuration,
            animationDuration,
            Tween.TransitionType.Linear,
            Tween.EaseType.InOut);

        _rect.Position = new Vector2(_rect.Position.X, (float)currentRectPosition);

        if (_animationDelta > animationDuration) {
            _animationPlaying = false;
            _animationDelta = 0;
        }
    }

    private void InitAnimation() {
        _rect.SetRandomColor();
        var circleHeight = _circle.Texture.GetHeight();
        _rectHeight = circleHeight / 100.0f * DiscoConfig.CurrentConfig.BarHeight;
        _rect.Scale = new Vector2(_rect.Scale.X, _rectHeight);
        _rectOffset = circleHeight / 2.0f + _rectHeight / 2.0f;
        _rect.Position = new Vector2(_rect.Position.X, -_rectOffset);
        _animationDuration = DiscoConfig.CurrentConfig.AnimationDuration;
        _animationPlaying = true;
        _animationDelta = 0;
    }

}
