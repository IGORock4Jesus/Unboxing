using System;
using System.Diagnostics;
using SharpDX;

namespace Unboxing.NodeComponents;
internal class RacquetControllerNodeComponent : NodeComponent
{
	private float _speed;
	private const float FORCE = 200.0f;
	private const float SPEEDUP = 5.0f;

	protected override void OnInitialize()
	{
		WindowInputs.KeyDown += WindowInputs_KeyPress;
		WindowInputs.KeyPress += WindowInputs_KeyPress;
	}

	public override void Dispose()
	{
		WindowInputs.KeyDown -= WindowInputs_KeyPress;
		WindowInputs.KeyPress -= WindowInputs_KeyPress;
	}

	public override void Update(float deltaTime)
	{
		if (MathF.Abs(_speed) < 0.01f)
		{
			_speed = 0.0f;
		}

		if (_speed != 0.0f)
		{
			Node.Position += new Vector2(_speed * deltaTime, 0.0f);
			var halfSize = Node.Size.Width / 2.0f;
			if (Node.Position.X < halfSize)
			{
				Node.Position = new(halfSize, Node.Position.Y);
				_speed = -_speed;
			}

			Debug.Assert(Node.Parent is not null);
			var end = Node.Parent.Size.Width - halfSize;
			if (Node.Position.X > end)
			{
				Node.Position = new(end, Node.Position.Y);
				_speed = -_speed;
			}

			var speedup = _speed * deltaTime * SPEEDUP;
			_speed -= speedup;
		}
	}

	private void WindowInputs_KeyPress(KeyboardEventArgs keyboard)
	{
		if (keyboard.Key == 'd')
		{
			_speed += FORCE;
		}

		if (keyboard.Key == 'a')
		{
			_speed -= FORCE;
		}
	}
}
