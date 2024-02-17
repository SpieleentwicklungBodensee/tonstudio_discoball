using System;
using Godot;

namespace TonstudioDiscoball;

public partial class DiagResize  : Control {
	public const int MinHeight = 80;
	public const int MinWidth = 80;

	private bool _hasMouseFocus;
	private bool _dragging;

	public override void _Ready() {
		MouseEntered += () => {
			if (Visible) {
				_hasMouseFocus = true;
			}
		};
		MouseExited += () => { _hasMouseFocus = false; };
	}

	public override void _Input(InputEvent ev) {
		if (ev.IsActionPressed("LeftClick") && _hasMouseFocus)
			StartDrag();
		else if (ev.IsActionReleased("LeftClick"))
			StopDrag();
		else if (_dragging && ev is InputEventMouseMotion mouseMotion) 
			UpdateWindowSize(mouseMotion);
	}

	private void StartDrag() {
		_dragging = true;
	}

	private void StopDrag() {
		_dragging = false;
	}

	private void UpdateWindowSize(InputEventMouseMotion mouseMotion) {
		var window = GetViewport().GetWindow();
		var positionChange = new Vector2(
			mouseMotion.Relative.X * window.Size.X /window.ContentScaleSize.X,
			mouseMotion.Relative.Y * window.Size.Y /window.ContentScaleSize.Y
		);
		var newSize = new Vector2I(
			Math.Max(MinWidth, (int)positionChange.X + window.Size.X), 
			Math.Max(MinHeight, (int)positionChange.Y + window.Size.Y));
		DisplayServer.WindowSetSize(newSize);
	}
}
