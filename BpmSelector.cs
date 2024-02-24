using Godot;

namespace TonstudioDiscoball;

public partial class BpmSelector : SpinBox {
    public override void _Ready() {
        Value = DiscoConfig.CurrentConfig.Bpm;
    }
    
    private void _OnValueChanged(double value) {
        DiscoConfig.CurrentConfig.Bpm = (int)value;
        SaveSystem.Save();
    }
}


