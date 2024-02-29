using Godot;

namespace TonstudioDiscoball;

public partial class Warning : Label {

    public override void _Process(double delta) {
        var config = DiscoConfig.CurrentConfig;
        Visible = config.AnimationDuration > 60.0 / config.Bpm;
    }
}
