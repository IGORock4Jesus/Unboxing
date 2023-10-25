using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unboxing.NodeSystems;

namespace Unboxing;
internal class NodeSystemCollection : IEnumerable<NodeSystem>
{
	private readonly List<NodeSystem> _systems = [];

	public NodeSystemCollection()
	{
		var assembly = Assembly.GetExecutingAssembly();
		var types = assembly.GetTypes()
			.Where(x => x.BaseType == typeof(NodeSystem))
			.ToList();

		foreach (var type in types)
		{
			var instance = Activator.CreateInstance(type) as NodeSystem 
				?? throw new InvalidOperationException("Cannot to create node system: " + type.FullName);
			_systems.Add(instance);
		}
	}

	public IEnumerator<NodeSystem> GetEnumerator()
	{
		return _systems.GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return _systems.GetEnumerator();
	}

	public void Update(float deltaTime)
	{
		foreach (var system in _systems)
		{
			var types = system.GetComponentTypes();
			var nodes = Scene.GetWithAll(types);

			system.Update(nodes, deltaTime);
		}
	}
}
