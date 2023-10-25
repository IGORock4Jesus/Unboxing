using System;
using System.Collections.Generic;
using System.Linq;
using SharpDX;
using Unboxing.AssetLoaders;
using Unboxing.NodeComponents;
using Unboxing.NodeSystems;

namespace Unboxing;
internal static class Scene
{
	public static Node Root { get; } = new();

	public static void Initialize(int width, int height)
	{
		Root.Size = new(width, height);
	}

	internal static void Release()
	{
		Clear();
	}

	private static void Clear()
	{
		Root.ClearComponentsAndChildren();
	}

	public static void Update(float deltaTime)
	{
		Root.Update(deltaTime);
	}

	public static void Render()
	{
		Root.Render();
	}

	internal static NodeCollection GetWithAll(IEnumerable<Type> componentTypes)
	{
		var result = new List<Node>();
		if (componentTypes.Any())
		{
			GetWithAll(Root, componentTypes, result);
		}

		return new(result);
	}

	private static void GetWithAll(Node node, IEnumerable<Type> componentTypes, List<Node> result)
	{
		var hasAll = true;
		foreach (var type in componentTypes)
		{
			if (!node.HasComponent(type))
			{
				hasAll = false;
				break;
			}
		}

		if (hasAll)
		{
			result.Add(node);
		}

		foreach (var child in node.Children)
		{
			GetWithAll(child, componentTypes, result);
		}
	}
}
