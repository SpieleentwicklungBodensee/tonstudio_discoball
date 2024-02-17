using System;
using Godot;

namespace TonstudioDiscoball;

public partial class DragAnchor : TextureRect {

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
        if (ev.IsActionPressed("LeftClick") && _hasMouseFocus) {
            _dragPoint = GetGlobalMousePosition();
            _dragging = true;
        }
        else if (ev.IsActionReleased("LeftClick") && _hasMouseFocus)
            _dragging = false;
        else if (_dragging && ev is InputEventMouseMotion) {
            var positionChange = GetGlobalMousePosition() - _dragPoint;
            var newPosition = GetViewport().GetWindow().Position +
                              new Vector2I((int)positionChange.X, (int)positionChange.Y);
            DisplayServer.WindowSetPosition(newPosition);
        }
    }

    public override void _Notification(int notificationType) {
        if (notificationType != NotificationDragEnd) return;

        Console.WriteLine("drag end");
        _dragging = false;
        _dragPoint = default;
    }
}
