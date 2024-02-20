using System;
using Godot;

namespace TonstudioDiscoball;

[GlobalClass]
public partial class DiscoConfig : Resource {
    
    [Export] public Vector2I WindowPosition;
    [Export] public Vector2I WindowSize = new (353, 353);
    [Export] public int Screen;

    public override string ToString() {
        return $"DiscoConfig: Window {WindowSize.X}x{WindowSize.Y} @{WindowPosition} on Screen {Screen}";
    }
}
