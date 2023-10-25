using SharpDX.Direct2D1;

namespace Unboxing;

public class ControlRenderingContext(RenderTarget renderTarget)
{
	public RenderTarget RenderTarget { get; } = renderTarget;
}