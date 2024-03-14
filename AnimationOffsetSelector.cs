using System;
using Godot;

namespace TonstudioDiscoball;

public partial class AnimationOffsetSelector : Control {

    private Slider _slider;
    private Label _display;

    public override void _Ready() {
        _slider = GetNode<HSlider>("HBoxContainer/V/H_Slider/HSlider");
        _display = GetNode<Label>("HBoxContainer/OffsetDisplay");

        _slider.ValueChanged += value => {
            DiscoConfig.CurrentConfig.AnimationOffset = (int)value;
            SaveSystem.Save();
            DisplayValue();
        };
        _slider.Value = DiscoConfig.CurrentConfig.AnimationOffset;
    }

    private void DisplayValue() {
        _display.Text = (DiscoConfig.CurrentConfig.AnimationOffset / 24.0).ToString("0.###");
    }

}
