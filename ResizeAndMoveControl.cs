using System;
using Godot;

namespace TonstudioDiscoball;

public partial class ResizeAndMoveControl : Node {


    public override void _Process(double delta) {
        if (Input.IsActionPressed("xIncrease")) {
            var window = GetWindow();
            window.Size += new Vector2I(2, 0);
            window.Position -= new Vector2I(1, 0);
        }
        if (Input.IsActionPressed("xDecrease")) {
            var window = GetWindow();
            window.Size -= new Vector2I(2, 0);
            window.Position += new Vector2I(1, 0);
        }
        if (Input.IsActionPressed("yIncrease")) {
            var window = GetWindow();
            window.Size += new Vector2I(0, 2);
            window.Position -= new Vector2I(0, 1);
        }
        if (Input.IsActionPressed("yDecrease")) {
            var window = GetWindow();
            window.Size -= new Vector2I(0, 2);
            window.Position += new Vector2I(0, 1);
        }
        if (Input.IsActionPressed("MoveUp")) {
            GetWindow().Position += new Vector2I(0, -1);
        }
        if (Input.IsActionPressed("MoveDown")) {
            GetWindow().Position += new Vector2I(0, 1);
        }
        if (Input.IsActionPressed("MoveLeft")) {
            GetWindow().Position += new Vector2I(-1, 0);
        }
        if (Input.IsActionPressed("MoveRight")) {
            GetWindow().Position += new Vector2I(1, 0);
        }
    }
}
