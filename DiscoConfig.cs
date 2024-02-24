using System;
using Godot;

namespace TonstudioDiscoball;

[GlobalClass]
public partial class DiscoConfig : Resource {

    public static DiscoConfig CurrentConfig;

    [Export] public Vector2I WindowPosition = DisplayServer.ScreenGetSize() / 2;
    [Export] public Vector2I WindowSize = new(353, 353);
    [Export] public int Screen;

    public override string ToString() {
        return
            $"DiscoConfig: Window {WindowSize.X}x{WindowSize.Y} @{WindowPosition} on Screen {Screen}";
    }

    public void ResetWindowPosition() {
        var defaults = new DiscoConfig();
        WindowPosition = defaults.WindowPosition;
        WindowSize = defaults.WindowSize;
        Screen = defaults.Screen;
    }
}
