using System;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.DirectWrite;

namespace Unboxing.ControlComponents;
internal class LabelControlComponent : ControlComponent
{
	private SolidColorBrush? _brush;
	private Color _color = Color.White;
	private TextFormat? _textFormat;
	private string _fontFamily = "Consolas";
	private float _fontSize = 30.0f;

	public string Text { get; set; } = string.Empty;
	public Color Color
	{
		get => _color;
		set
		{
			if (_color == value)
			{
				return;
			}

			_color = value;
			RecreateForegroundBrush();
		}
	}
	public string FontFamily
	{
		get => _fontFamily;
		set
		{
			if (_fontFamily == value)
			{
				return;
			}

			_fontFamily = value;
			RecreateTextFormat();
		}
	}
	public float FontSize
	{
		get => _fontSize;
		set
		{
			if (_fontSize != value)
			{
				return;
			}

			_fontSize = value;
			RecreateTextFormat();
		}
	}

	private void RecreateForegroundBrush()
	{
		_brush?.Dispose();
		_brush = new(Graphics.RenderTarget, _color);
	}

	private void RecreateTextFormat()
	{
		_textFormat?.Dispose();
		_textFormat = new(Graphics.WriteFactory, _fontFamily, _fontSize);
	}

	protected override void OnInitialize()
	{
		_brush = new(Graphics.RenderTarget, _color);
		_textFormat = new(Graphics.WriteFactory, _fontFamily, 30.0f);
	}

	public override void Render()
	{
		if (string.IsNullOrEmpty(Text))
		{
			return;
		}

		var globalPosition = Control.GlobalPosition;
		var size = Control.Size;
		var rect = new RectangleF(globalPosition.X, globalPosition.Y, size.Width, size.Height);
		Graphics.RenderTarget.DrawText(Text, _textFormat, rect, _brush);

	}

	public override void Dispose()
	{
		_brush?.Dispose();
		_textFormat?.Dispose();
	}
}
