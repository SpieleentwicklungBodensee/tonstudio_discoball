using System;
using Godot;

namespace TonstudioDiscoball;

public partial class BottomResize : Control {

    private bool _hasMouseFocus;
    private bool _dragging;

    public override void _Ready() {
        MouseEntered += () => {
            if (Visible) {
                _hasMouseFocus = true;
            }
        };
        MouseExited += () => { _hasMouseFocus = false; };
    }

    public override void _Input(InputEvent ev) {
        if (ev.IsActionPressed("LeftClick") && _hasMouseFocus)
            StartDrag();
        else if (ev.IsActionReleased("LeftClick"))
            StopDrag();
        else if (_dragging && ev is InputEventMouseMotion mouseMotion) 
            UpdateWindowSize(mouseMotion);
    }

    private void StartDrag() {
        _dragging = true;
    }

    private void StopDrag() {
        _dragging = false;
    }

    private void UpdateWindowSize(InputEventMouseMotion mouseMotion) {
        var window = GetViewport().GetWindow();
        var windowScale = window.Size.Y / (float)window.ContentScaleSize.Y;
        var positionChange = mouseMotion.Relative * windowScale;
        var newSize = new Vector2I(window.Size.X, Math.Max(DiagResize.MinHeight, (int)positionChange.Y + window.Size.Y));
        DisplayServer.WindowSetSize(newSize);
    }
}
