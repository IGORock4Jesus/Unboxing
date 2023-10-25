using System;
using System.Diagnostics;
using SharpDX;
using SharpDX.Direct2D1;

namespace Unboxing;
internal static class Graphics
{
	private static Factory? Direct2DFactory;
	private static WindowRenderTarget? RenderTargetInternal;
	private static SharpDX.DirectWrite.Factory? WriteFactoryInternal;

	public static WindowRenderTarget RenderTarget
	{
		get => RenderTargetInternal ?? throw new NullReferenceException();
	}

	public static SharpDX.DirectWrite.Factory WriteFactory
	{
		get => WriteFactoryInternal ?? throw new NullReferenceException();
	}

	public static void Initialize(nint windowHandle, int width, int height)
	{
		Direct2DFactory = new Factory(FactoryType.SingleThreaded, DebugLevel.Information);
		WriteFactoryInternal = new SharpDX.DirectWrite.Factory(SharpDX.DirectWrite.FactoryType.Shared);

		InitializeDeviceResources(windowHandle, width, height);
	}

	internal static void Begin()
	{
		Debug.Assert(RenderTargetInternal is not null);

		RenderTargetInternal.BeginDraw();
		RenderTargetInternal.Transform = Matrix3x2.Identity;
		RenderTargetInternal.Clear(Color.Aquamarine);
	}

	internal static void End()
	{
		Debug.Assert(RenderTargetInternal is not null);

		RenderTargetInternal.EndDraw();
	}

	internal static void Release()
	{
		WriteFactoryInternal?.Dispose();
		RenderTargetInternal?.Dispose();
		Direct2DFactory?.Dispose();
	}

	internal static void Resize(int width, int height)
	{
		RenderTargetInternal?.Resize(new(width, height));
	}

	private static void InitializeDeviceResources(nint windowHandle, int width, int height)
	{
		RenderTargetInternal = new WindowRenderTarget(Direct2DFactory,
			new RenderTargetProperties(),
			new HwndRenderTargetProperties()
			{
				Hwnd = windowHandle,
				PixelSize = new Size2(width, height)
			}
		);
	}
}
