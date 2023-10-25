namespace Unboxing.NodeComponents;
internal class CollisionEventArgs(Node node, Node other, bool xCollision, bool yCollision)
{
	public Node Node => node;
	public Node Other => other;
	public bool XCollision => xCollision;
	public bool YCollision => yCollision;
}
