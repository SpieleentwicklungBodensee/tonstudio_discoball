using System;
using Godot;

namespace TonstudioDiscoball;

public partial class SaveSystem : Node {

    private const string SaveFile = "user://disco_config.tres";

    public override void _Ready() {
        Load();
    }

    public void Save() {
        var config = new DiscoConfig();
        config.WindowPosition = GetWindow().Position;
        config.WindowSize = GetWindow().Size;
        ResourceSaver.Save(config, SaveFile);
    }

    private void Load() {
        var config = ResourceLoader.Load<DiscoConfig>(SaveFile);
        if (config == null) return;

        GetWindow().Position = config.WindowPosition;
        GetWindow().Size = config.WindowSize;
    }
}
