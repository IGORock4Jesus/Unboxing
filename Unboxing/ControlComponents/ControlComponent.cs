using System;

namespace Unboxing.ControlComponents;
internal class ControlComponent : IDisposable
{
	public Control Control { get; private set; } = default!;
	public  void Initialize(Control control)
	{
		Control = control;
		OnInitialize();
	}

	protected virtual void OnInitialize()
	{
	}

	public virtual void Update() { }
	public virtual void Render() { }

	public virtual void Dispose()
	{
	}
}
