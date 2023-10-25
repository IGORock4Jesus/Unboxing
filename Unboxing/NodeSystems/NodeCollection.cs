using System.Collections;
using System.Collections.Generic;
using Unboxing.NodeComponents;

namespace Unboxing.NodeSystems;
internal class NodeCollection(IEnumerable<Node> nodes)
	: IEnumerable<Node>
{
	public IEnumerator<Node> GetEnumerator()
	{
		return nodes.GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return nodes.GetEnumerator();
	}
}
