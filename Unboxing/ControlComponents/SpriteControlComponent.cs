using System;
using System.Diagnostics;
using SharpDX;
using SharpDX.Direct2D1;

namespace Unboxing.ControlComponents;
internal class SpriteControlComponent : ControlComponent
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
	private Bitmap? _bitmap;
	private string? _bitmapResourceName;

	public string? BitmapResourceName
	{
		get => _bitmapResourceName;
		set
		{
			if (_bitmapResourceName == value)
			{
				return;
			}

			_bitmapResourceName = value;
			RecreateBitmap();
		}
	}

	private void RecreateBitmap()
	{
		if (string.IsNullOrEmpty(_bitmapResourceName))
		{
			_bitmap = null;
		}
		else
		{
			_bitmap = ResourceRepository.Get<Bitmap>(_bitmapResourceName);
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
		if (!string.IsNullOrEmpty(_bitmapResourceName))
		{
			_bitmap = ResourceRepository.Get<Bitmap>(_bitmapResourceName);
		}
	}

	public override void Render()
	{
		Debug.Assert(_brush is not null);

		var globalPosition = Control.GlobalPosition;
		var size = Control.Size;
		var rect = new RectangleF(globalPosition.X, globalPosition.Y, size.Width, size.Height);
		if (_bitmap is null)
		{
			Graphics.RenderTarget.FillRectangle(
				rect,
				_brush
			);
		}
		else
		{
			Graphics.RenderTarget.DrawBitmap(_bitmap, rect, 
				Color.A / 255.0f, // TODO: Вообще сделать, чтобы можно было использовать цвет как фильтр
				BitmapInterpolationMode.Linear);
		}
	}

	public override void Dispose()
	{
		_brush?.Dispose();
	}
}
