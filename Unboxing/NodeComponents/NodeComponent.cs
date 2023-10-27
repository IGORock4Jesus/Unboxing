using System;

namespace Unboxing.NodeComponents;
internal class NodeComponent : IDisposable
{
	private bool _isInitialized;

	public Node Node { get; private set; } = default!;
	public void Initialize(Node node)
	{
		Node = node;
	}

	protected virtual void OnInitialize()
	{
	}

	public void Update(float deltaTime)
	{
		if (!_isInitialized)
		{
			_isInitialized = true;
			OnInitialize();
		}

		OnUpdate(deltaTime);
	}
	
	protected virtual void OnUpdate(float deltaTime) { }
	public virtual void Render() { }

	public virtual void Dispose()
	{
	}
}
