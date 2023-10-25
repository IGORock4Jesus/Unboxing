using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;
using SharpDX.DirectInput;

namespace Unboxing;
internal static class Inputs
{
	private static DirectInput? DirectInput;
	static Mouse? Mouse;
	static Keyboard? Keyboard;
	static KeyboardState PreviousKeyboardState = new();
	static KeyboardState CurrentKeyboardState = new();
	static MouseState PreviousMouseState = new();
	static MouseState CurrentMouseState = new();

	public static void Initialize(nint windowHandle)
	{
		DirectInput = new DirectInput();
		Keyboard = new(DirectInput);
		Mouse = new(DirectInput);

		Keyboard.SetCooperativeLevel(windowHandle, CooperativeLevel.Background | CooperativeLevel.NonExclusive);
		Mouse.SetCooperativeLevel(windowHandle, CooperativeLevel.Background | CooperativeLevel.NonExclusive);

		Keyboard.Acquire();
		Mouse.Acquire();
	}

	public static void Release()
	{
		Mouse?.Unacquire();
		Keyboard?.Unacquire();

		Mouse?.Dispose();
		Keyboard?.Dispose();
		DirectInput?.Dispose();
	}

	internal static void Update()
	{
		Debug.Assert(Keyboard is not null);
		Debug.Assert(Mouse is not null);

		PreviousKeyboardState = CurrentKeyboardState;
		PreviousMouseState = CurrentMouseState;

		CurrentKeyboardState = Keyboard.GetCurrentState();
		CurrentMouseState = Mouse.GetCurrentState();
	}

	internal static bool IsKeyDown(Key key)
	{
		return CurrentKeyboardState.IsPressed(key) && !PreviousKeyboardState.IsPressed(key);
	}

	internal static bool IsKeyUp(Key key)
	{
		return !CurrentKeyboardState.IsPressed(key) && PreviousKeyboardState.IsPressed(key);
	}

	internal static bool IsKeyPress(Key key)
	{
		return CurrentKeyboardState.IsPressed(key) && PreviousKeyboardState.IsPressed(key);
	}

	internal static bool IsMouseDown(MouseButtons key)
	{
		return CurrentMouseState.Buttons[(int)key] && !PreviousMouseState.Buttons[(int)key];
	}

	internal static bool IsMouseUp(MouseButtons key)
	{
		return !CurrentMouseState.Buttons[(int)key] && PreviousMouseState.Buttons[(int)key];
	}

	internal static bool IsMousePress(MouseButtons key)
	{
		return CurrentMouseState.Buttons[(int)key] && PreviousMouseState.Buttons[(int)key];
	}

	internal static Vector2 GetMouseOffset()
	{
		return new Vector2(CurrentMouseState.X, CurrentMouseState.Y);
	}
}
