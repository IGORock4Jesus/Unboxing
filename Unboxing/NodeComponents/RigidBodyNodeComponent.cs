using System;

namespace Unboxing.NodeComponents;
internal class RigidBodyNodeComponent : NodeComponent
{
	public event Action<CollisionEventArgs>? Collision;

	internal void RaiseCollision(Node other, bool xCollision, bool yCollision)
	{
		var args = new CollisionEventArgs(Node, other, xCollision, yCollision);
		Collision?.Invoke(args);
	}
}
