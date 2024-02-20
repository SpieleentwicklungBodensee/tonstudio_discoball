using System;
using Godot;

namespace TonstudioDiscoball;

public partial class SaveSystem : Node {

    private const string SaveFile = "user://disco_config.tres";

    public override void _Ready() {
        Load();
    }

    public void Save() {
        var window = GetWindow();
        var config = new DiscoConfig();
        config.WindowPosition = window.Position;
        config.WindowSize = window.Size;
        config.Screen = window.CurrentScreen;
        
        ResourceSaver.Save(config, SaveFile);
    }

    private void Load() {
        var config = ResourceLoader.Load<DiscoConfig>(SaveFile);
        if (config == null) return;

        var window = GetWindow();
        window.CurrentScreen = config.Screen;
        window.Position = config.WindowPosition;
        window.Size = config.WindowSize;
    }
}
