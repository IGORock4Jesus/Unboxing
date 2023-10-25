using System;
using SharpDX;

namespace Unboxing.ControlComponents;
internal class MouseZoneControlComponent : ControlComponent
{
	bool _isMouseOver;

	public event Action<Control>? MouseEnter, MouseLeave;	

	protected override void OnInitialize()
	{
		HUD.MousePositionChanged += HUD_MousePositionChanged;
	}

	private void HUD_MousePositionChanged(Vector2 mousePosition)
	{
		var isMouseOver =
			mousePosition.X >= Control.GlobalPosition.X &&
			mousePosition.Y >= Control.GlobalPosition.Y &&
			mousePosition.X <= Control.GlobalPosition.X + Control.Size.Width &&
			mousePosition.Y <= Control.GlobalPosition.Y + Control.Size.Height;

		if (isMouseOver != _isMouseOver)
		{
			_isMouseOver = isMouseOver;

			if (_isMouseOver)
			{
				RaiseEnter();
			}
			else
			{
				RaiseLeave();
			}
		}
	}

	private void RaiseLeave()
	{
		MouseLeave?.Invoke(Control);
	}

	private void RaiseEnter()
	{
		MouseEnter?.Invoke(Control);
	}
}
