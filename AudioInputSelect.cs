using System;
using Godot;

namespace TonstudioDiscoball;

public partial class AudioInputSelect : OptionButton {
    
    private string[] _inputDevices;

    public override void _Ready() {
        _inputDevices = AudioServer.GetInputDeviceList();
        var current = AudioServer.InputDevice;
        for (var i = 0; i < _inputDevices.Length; i++) {
            var input = _inputDevices[i];
            AddItem(input, i);
            if (input.Equals(current)) {
                Selected = i;
            }
        }
    }

    private void _OnItemSelected(long index) {
        AudioServer.InputDevice = _inputDevices[index];
        // TODO save to config
    }

}
