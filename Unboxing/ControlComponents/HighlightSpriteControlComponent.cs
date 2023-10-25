using SharpDX;

namespace Unboxing.ControlComponents;
internal class HighlightSpriteControlComponent : ControlComponent
{
	private Color _previousBackground;

	protected override void OnInitialize()
	{
		var mouseZone = Control.GetComponent<MouseZoneControlComponent>();
		mouseZone.MouseEnter += MouseZone_MouseEnter;
		mouseZone.MouseLeave += MouseZone_MouseLeave;
	}

	private void MouseZone_MouseLeave(Control control)
	{
		var sprite = Control.GetComponent<SpriteControlComponent>();
		sprite.Color = _previousBackground;
	}

	private void MouseZone_MouseEnter(Control control)
	{
		var sprite = Control.GetComponent<SpriteControlComponent>();
		_previousBackground = sprite.Color;
		sprite.Color = _previousBackground * 1.5f;
	}
}
