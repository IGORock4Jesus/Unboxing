namespace Unboxing.NodeComponents;
internal class CollisionEventArgs(Node node, Node other)
{
	public Node Node => node;
	public Node Other => other;
}
