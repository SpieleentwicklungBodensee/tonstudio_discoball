using System;
using Godot;

namespace TonstudioDiscoball;

public partial class SaveSystem : Node {

    private const string SaveFile = "user://dicso_cdonfig.tres";

    public override void _Input(InputEvent ev) {
        if (ev.IsActionPressed("Save")) {
            Save();
        } else if (ev.IsActionPressed("Load")) {
            Load();
        }
    }

    private void Save() {
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
