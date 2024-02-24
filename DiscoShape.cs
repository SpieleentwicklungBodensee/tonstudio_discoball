using System;
using Godot;

namespace TonstudioDiscoball;

public partial class DiscoShape : Sprite2D {
    
    private readonly Random _random = new();

    public void SetRandomColor() {
        var randomAbovePoint5 = _random.NextSingle() / 2 + .5f;
        var color = Color.FromHsv(_random.NextSingle(), randomAbovePoint5, 1);
        SelfModulate = color;
    }
}
