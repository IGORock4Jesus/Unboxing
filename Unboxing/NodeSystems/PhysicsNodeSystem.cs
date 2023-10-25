using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unboxing.NodeComponents;

namespace Unboxing.NodeSystems;
internal class PhysicsNodeSystem : NodeSystem
{
	public override IEnumerable<Type> GetComponentTypes()
	{
		return [typeof(RigidBodyNodeComponent)];
	}

	public override void Update(NodeCollection nodes, float deltaTime)
	{
		foreach (var a in nodes)
		{
			foreach (var b in nodes)
			{
				if (a == b)
				{
					continue;
				}

				var distance = a.GlobalPosition - b.GlobalPosition;

				var xCollision = MathF.Abs(distance.X) < MathF.Min(a.Size.Width, b.Size.Width);
				var yCollision = MathF.Abs(distance.Y) < MathF.Min(a.Size.Height, b.Size.Height);
				if (xCollision && yCollision)
                {
					RaiseCollision(a, b, xCollision, yCollision);
				}
            }
		}
	}

	private static void RaiseCollision(Node node, Node other, bool xCollision, bool yCollision)
	{
		var rigidBody = node.GetComponent<RigidBodyNodeComponent>();
		rigidBody.RaiseCollision(other, xCollision, yCollision);
	}
}
