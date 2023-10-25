using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;
using SharpDX.Direct2D1;

namespace Unboxing.NodeComponents;
internal class EllipseNodeComponent : NodeComponent
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
		Debug.Assert(_brush is null);

		_brush = new(Graphics.RenderTarget, _background);
	}

	public override void Render()
	{
		Debug.Assert(_brush is not null);

		Graphics.RenderTarget.FillEllipse(new Ellipse(Node.GlobalPosition, 
			Node.Size.Width / 2.0f, 
			Node.Size.Height / 2.0f), 
			_brush);
	}

	public override void Dispose()
	{
		_brush?.Dispose();
	}
}
