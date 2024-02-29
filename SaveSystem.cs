using System;
using System.Diagnostics;
using System.Linq;
using Godot;

namespace TonstudioDiscoball;

public partial class SaveSystem : Node {

    private const string SaveFile = "user://disco_config.tres";

    public override void _Ready() {
        Load();
    }

    public void SaveWindowPosition() {
        var window = GetWindow();
        var config = DiscoConfig.CurrentConfig;
        
        config.WindowPosition = window.Position;
        config.WindowSize = window.Size;
        config.Screen = window.CurrentScreen;

        Save();
    }

    public void ResetWindowPosition() {
        DiscoConfig.CurrentConfig.ResetWindowPosition();
        ApplyCurrentConfig();
        Save();
    }

    public static void Save() {
        Trace.TraceInformation($"Saving config: {DiscoConfig.CurrentConfig}");
        ResourceSaver.Save(DiscoConfig.CurrentConfig, SaveFile);
    }

    private void Load() {
        var config = ResourceLoader.Load<DiscoConfig>(SaveFile);
        if (config != null) {
            DiscoConfig.CurrentConfig = config;
            ApplyCurrentConfig();
        } else {
            DiscoConfig.CurrentConfig = new DiscoConfig();
            ApplyCurrentConfig();
        }
    }

    private void ApplyCurrentConfig() {
        var config = DiscoConfig.CurrentConfig;
        var window = GetWindow();
        window.CurrentScreen = config.Screen;
        window.Size = config.WindowSize;
        window.Position = config.WindowPosition;
    }

}
