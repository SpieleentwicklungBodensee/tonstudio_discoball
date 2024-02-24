using System;
using Godot;

namespace TonstudioDiscoball;

public partial class DragAnchor : Control {
    
    [Export] private SaveSystem _saveSystem;

    private bool _hasMouseFocus;
    private bool _dragging;
    private Vector2 _dragPoint;

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
        else if (_dragging && ev is InputEventMouseMotion) {
            UpdateWindowPosition();
        }
    }
    
    private void StartDrag() {
        _dragPoint = GetGlobalMousePosition();
        _dragging = true;
    }
    
    private void StopDrag() {
        _dragging = false;
        _dragPoint = default;
    }
    
    private void UpdateWindowPosition() {
        var positionChange = GetGlobalMousePosition() - _dragPoint;
        var window = GetWindow();
        var newPosition = window.Position + new Vector2I((int)positionChange.X, (int)positionChange.Y);
        window.Position = newPosition;
        _saveSystem.SaveWindowPosition();
    }
}
