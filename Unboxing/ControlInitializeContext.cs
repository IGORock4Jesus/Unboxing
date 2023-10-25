using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.Direct2D1;

namespace Unboxing;
internal class ControlInitializeContext(RenderTarget renderTarget)
{
	public RenderTarget RenderTarget { get; } = renderTarget;
}
