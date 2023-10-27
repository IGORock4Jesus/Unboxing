using System;
using System.Collections.Generic;
using System.Linq;
using SharpDX;
using Unboxing.AssetLoaders;
using Unboxing.ControlComponents;
using Unboxing.NodeComponents;

namespace Unboxing;
internal class Node : IDisposable
{
	private readonly List<Node> _children = [];
	private Node? _parent;
	private readonly List<NodeComponent> _components = [];

	public string Name { get; set; } = string.Empty;
	public Vector2 Position { get; set; }
	public Size2F Size { get; set; }
	public Vector2 GlobalPosition
	{
		get
		{
			if (_parent is null)
			{
				return Position;
			}

			return _parent.GlobalPosition + Position;
		}
	}
	public Node? Parent => _parent;
	public IEnumerable<Node> Children => _children;

	public void Render()
	{
		foreach (var component in _components)
		{
			component.Render();
		}

		foreach (var child in _children)
		{
			child.Render();
		}
	}

	public void AddChild(Node node)
	{
		node._parent = this;
		_children.Add(node);
	}

	public void Dispose()
	{
		foreach (var component in _components)
		{
			component.Dispose();
		}
	}

	public T AddComponent<T>()
		where T : NodeComponent, new()
	{
		var component = new T();

		_components.Add(component);
		component.Initialize(this);

		return component;
	}

	internal T GetComponent<T>()
		where T : NodeComponent
	{
		return (T)_components.First(x => x.GetType() == typeof(T));
	}

	internal void ClearComponentsAndChildren()
	{
		foreach (var component in _components)
		{
			component.Dispose();
		}

		foreach (var child in _children)
		{
			child.ClearComponentsAndChildren();
		}

		_children.Clear();
	}

	internal void Update(float deltaTime)
	{
		foreach (var component in _components)
		{
			component.Update(deltaTime);
		}

		foreach (var child in _children)
		{
			child.Update(deltaTime);
		}
	}

	internal bool HasComponent(Type type)
	{
		return _components.Any(x => x.GetType() == type);
	}

	public override string? ToString()
	{
		return string.IsNullOrEmpty(Name) ? base.ToString() : Name;
	}
}
