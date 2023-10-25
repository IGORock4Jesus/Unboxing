using System;

namespace Unboxing.NodeComponents;
internal class NodeComponent : IDisposable
{
	public Node Node { get; private set; } = default!;
	public void Initialize(Node node)
	{
		Node = node;
		OnInitialize();
	}

	protected virtual void OnInitialize()
	{
	}

	public virtual void Update(float deltaTime) { }
	public virtual void Render() { }

	public virtual void Dispose()
	{
	}
}
