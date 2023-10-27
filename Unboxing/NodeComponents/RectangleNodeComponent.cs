using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;
using SharpDX.Direct2D1;

namespace Unboxing.NodeComponents;
internal class RectangleNodeComponent : NodeComponent
{
	private SolidColorBrush? _brush;
	private Color _background = Color.Black;
	public Color Color
	{
		get => _background;
		set
		{
			if (_background == value)
			{
				return;
			}

			_background = value;
			RecreateBackgroundBrush();
		}
	}

	private void RecreateBackgroundBrush()
	{
		_brush?.Dispose();
		_brush = new(Graphics.RenderTarget, _background);
	}

	protected override void OnInitialize()
	{
		_brush ??= new(Graphics.RenderTarget, _background);
	}

	public override void Render()
	{
		Debug.Assert(_brush is not null);

		Graphics.RenderTarget.FillRectangle(new RectangleF(
			Node.GlobalPosition.X - Node.Size.Width / 2.0f,
			Node.GlobalPosition.Y - Node.Size.Width / 2.0f,
			Node.Size.Width, Node.Size.Height),
			_brush);
	}

	public override void Dispose()
	{
		_brush?.Dispose();
	}
}
