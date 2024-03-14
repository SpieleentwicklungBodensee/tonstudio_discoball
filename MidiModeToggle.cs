using System;
using Godot;

namespace TonstudioDiscoball;

public partial class MidiModeToggle : CheckButton {
    public override void _Ready() {
        ButtonPressed = DiscoConfig.CurrentConfig.MidiMode;
        Toggled += _OnToggled;
    }
    
    private void _OnToggled(bool toggled) {
        DiscoConfig.CurrentConfig.MidiMode = toggled;
        SaveSystem.Save();
    }

}


