using Godot;

namespace TonstudioDiscoball;

public partial class AnimationDurationSelector : SpinBox {
    public override void _Ready() {
        Value = DiscoConfig.CurrentConfig.AnimationDuration;
    }
    
    private void _OnValueChanged(double value) {
        DiscoConfig.CurrentConfig.AnimationDuration = value;
        SaveSystem.Save();
    }
}


