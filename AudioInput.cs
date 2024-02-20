using System;
using Godot;

namespace TonstudioDiscoball;

public partial class AudioInput : AudioStreamPlayer {

    private const string SpectrumBus = "Spectrum";
    private const string AudioInBus = "Audio_In";

    private AudioEffectSpectrumAnalyzerInstance _spectrum;

    public override void _Ready() {
        _spectrum =
            AudioServer.GetBusEffectInstance(AudioServer.GetBusIndex(SpectrumBus), 0) as
                AudioEffectSpectrumAnalyzerInstance;
        var audioIn = AudioServer.GetBusEffect(AudioServer.GetBusIndex(AudioInBus), 0) as AudioEffectRecord;
        audioIn!.SetRecordingActive(true);
    }



    public override void _Process(double delta) {
        var magnitude = _spectrum.GetMagnitudeForFrequencyRange(0, 11000).Length();
        // if (magnitude > 1e-5) {
        //     Console.WriteLine("on");
        // } else {
        //     Console.WriteLine("off");
        // }

        // Console.WriteLine(magnitude);
    }
}
