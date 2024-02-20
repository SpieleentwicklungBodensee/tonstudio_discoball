using System;
using Godot;

namespace TonstudioDiscoball;

public partial class ResizeAndMoveControl : Node {

    [Export] private SaveSystem _saveSystem;

    public override void _Input(InputEvent ev) {
        if (ev.IsActionPressed("ResetPosition")) {
            _saveSystem.ResetWindowPosition();
        }
    }

    public override void _Process(double delta) {
        if (Input.IsActionPressed("xIncrease")) {
            var window = GetWindow();
            window.Size += new Vector2I(2, 0);
            window.Position -= new Vector2I(1, 0);
            _saveSystem.SaveWindowPosition();
        } 
        if (Input.IsActionPressed("xDecrease")) {
            var window = GetWindow();
            window.Size -= new Vector2I(2, 0);
            window.Position += new Vector2I(1, 0);
            _saveSystem.SaveWindowPosition();
        }
        if (Input.IsActionPressed("yIncrease")) {
            var window = GetWindow();
            window.Size += new Vector2I(0, 2);
            window.Position -= new Vector2I(0, 1);
            _saveSystem.SaveWindowPosition();
        }
        if (Input.IsActionPressed("yDecrease")) {
            var window = GetWindow();
            window.Size -= new Vector2I(0, 2);
            window.Position += new Vector2I(0, 1);
            _saveSystem.SaveWindowPosition();
        }
        if (Input.IsActionPressed("MoveUp")) {
            GetWindow().Position += new Vector2I(0, -1);
            _saveSystem.SaveWindowPosition();
        }
        if (Input.IsActionPressed("MoveDown")) {
            GetWindow().Position += new Vector2I(0, 1);
            _saveSystem.SaveWindowPosition();
        }
        if (Input.IsActionPressed("MoveLeft")) {
            GetWindow().Position += new Vector2I(-1, 0);
            _saveSystem.SaveWindowPosition();
        }
        if (Input.IsActionPressed("MoveRight")) {
            GetWindow().Position += new Vector2I(1, 0);
            _saveSystem.SaveWindowPosition();
        }
    }
}
