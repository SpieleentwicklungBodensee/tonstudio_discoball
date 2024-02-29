using Godot;

namespace TonstudioDiscoball;

public partial class BarHeightSelector : SpinBox {
    public override void _Ready() {
        Value = DiscoConfig.CurrentConfig.BarHeight;
    }
    
    private void _OnValueChanged(double value) {
        DiscoConfig.CurrentConfig.BarHeight = (int)value;
        SaveSystem.Save();
    }
}


