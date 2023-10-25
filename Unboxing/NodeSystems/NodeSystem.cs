using System;
using System.Collections.Generic;

namespace Unboxing.NodeSystems;
internal abstract class NodeSystem
{
	public abstract IEnumerable<Type> GetComponentTypes();
	public virtual void Update(NodeCollection nodes, float deltaTime) { }
}

