using System;
using System.Windows.Forms;
using SharpDX;

namespace Unboxing;
internal static class WindowInputs
{
	public static event Action<KeyboardEventArgs>? KeyDown, KeyUp, KeyPress;

	public static event Action<MouseEventContext>? MouseDown, MouseUp, MouseMove, MouseWheel;

	internal static void RaiseKeyDown(Keys keyCode)
	{
		KeyDown?.Invoke(new(keyCode, char.MinValue));
	}

	internal static void RaiseKeyUp(Keys keyCode)
	{
		KeyUp?.Invoke(new(keyCode, char.MinValue));
	}

	internal static void RaiseKeyPress(char keyChar)
	{
		KeyPress?.Invoke(new(Keys.None, keyChar));
	}

	internal static void RaiseMouseDown(int x, int y, System.Windows.Forms.MouseButtons mouseButtons, int wheel)
	{
		MouseDown?.Invoke(new(x, y, mouseButtons, wheel));
	}

	internal static void RaiseMouseUp(int x, int y, System.Windows.Forms.MouseButtons mouseButtons, int wheel)
	{
		MouseUp?.Invoke(new(x, y, mouseButtons, wheel));
	}

	internal static void RaiseMouseMove(int x, int y, System.Windows.Forms.MouseButtons mouseButtons, int wheel)
	{
		MouseMove?.Invoke(new(x, y, mouseButtons, wheel));
	}

	internal static void RaiseMouseWheel(int x, int y, System.Windows.Forms.MouseButtons mouseButtons, int wheel)
	{
		MouseWheel?.Invoke(new(x, y, mouseButtons, wheel));
	}
}
