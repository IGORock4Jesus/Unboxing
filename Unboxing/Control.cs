using System;
using System.Collections.Generic;
using System.Linq;
using SharpDX;
using Unboxing.AssetLoaders;
using Unboxing.ControlComponents;

namespace Unboxing;
internal class Control : IDisposable
{
	private readonly List<Control> _children = [];
	private Control? _parent;
	private readonly List<ControlComponent> _components = [];

	public Vector2 Position { get; set; }
	public Size2F Size { get; set; }
	public HorizontalAlignment? HorizontalAlignment { get; set; }
	public VerticalAlignment? VerticalAlignment { get; set; }
	public Vector2 GlobalPosition
	{
		get
		{
			if (_parent is null)
			{
				return Position;
			}

			var position = _parent.GlobalPosition;

			if (HorizontalAlignment is not null)
			{
				if (HorizontalAlignment == HorizontalAlignment.Left)
				{
				}
				else if (HorizontalAlignment == HorizontalAlignment.Right)
				{
					position.X += _parent.Size.Width - Size.Width;
				}
				else if (HorizontalAlignment == HorizontalAlignment.Center)
				{
					position.X += _parent.Size.Width * 0.5f - Size.Width * 0.5f;
				}
				else
				{
					throw new InvalidOperationException("Invalid horizontal alignment");
				}
			}

			if (VerticalAlignment is not null)
			{
				if (VerticalAlignment == VerticalAlignment.Top)
				{
				}
				else if (VerticalAlignment == VerticalAlignment.Bottom)
				{
					position.Y += _parent.Size.Height - Size.Height;
				}
				else if (HorizontalAlignment == HorizontalAlignment.Center)
				{
					position.Y += _parent.Size.Height * 0.5f - Size.Height * 0.5f;
				}
				else
				{
					throw new InvalidOperationException("Invalid vertical alignment");
				}
			}

			return position + Position;
		}
	}
	public Control? Parent => _parent;

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

	public void AddChild(Control control)
	{
		control._parent = this;
		_children.Add(control);
	}

	public void Dispose()
	{
		foreach (var component in _components)
		{
			component.Dispose();
		}
	}

	public T AddComponent<T>()
		where T : ControlComponent, new()
	{
		var component = new T();

		_components.Add(component);
		component.Initialize(this);

		return component;
	}

	internal T GetComponent<T>()
		where T : ControlComponent
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
}
