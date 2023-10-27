using System;
using System.Diagnostics;
using Box2D.NetStandard.Dynamics.Bodies;
using SharpDX;

namespace Unboxing.NodeComponents;
internal class RigidBodyNodeComponent : NodeComponent
{
	private Body? _body;

	public bool IsDynamic;
	public event Action<CollisionEventArgs>? Collision;

	internal void RaiseCollision(Node other)
	{
		var args = new CollisionEventArgs(Node, other);
		Collision?.Invoke(args);
	}

	protected override void OnInitialize()
	{
		_body = Scene.CreateRigidBody(Node.GlobalPosition, Node.Size, IsDynamic);
		Debug.Assert(_body is not null);

		_body.SetUserData(this);
	}

	public override void Dispose()
	{
		Debug.Assert(_body is not null);

		Scene.RemoveRigidBody(_body);
	}

	protected override void OnUpdate(float deltaTime)
	{
		Debug.Assert(_body is not null);

		Node.Position = new(_body.Position.X, _body.Position.Y);
	}

	internal void SetVelocity(Vector2 velocity)
	{
		Debug.Assert(_body is not null);

		_body.SetLinearVelocity(new System.Numerics.Vector2(velocity.X, velocity.Y) * 1000.0f);
		//_body.ApplyForceToCenter(new System.Numerics.Vector2(velocity.X, velocity.Y) * 1000.0f);
	}

	internal void SetPosition(Vector2 position)
	{
		Debug.Assert(_body is not null);

		_body.SetTransform(new System.Numerics.Vector2(position.X, position.Y), 0.0f);
	}
}
