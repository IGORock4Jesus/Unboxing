using System;
using System.Diagnostics;
using SharpDX;

namespace Unboxing.NodeComponents;
internal class BallNodeComponent : NodeComponent
{
	private Vector2 _direction;
	public float Speed = 1.0f;

	public BallNodeComponent()
	{
		_direction = new Vector2(1.0f, -1.0f);
		_direction.Normalize();
	}

	protected override void OnInitialize()
	{
		var rigidBody = Node.GetComponent<RigidBodyNodeComponent>();
		rigidBody.Collision += RigidBody_Collision;
	}

	private void RigidBody_Collision(CollisionEventArgs collision)
	{
		var otherPosition = collision.Other.GlobalPosition;
		var otherSize = collision.Other.Size;

		if (collision.XCollision)
		{
			_direction.X = -_direction.X;
		}


		if (collision.YCollision)
		{
			_direction.Y = -_direction.Y;
		}
	}

	public override void Update(float deltaTime)
	{
		UpdatePosition(deltaTime);
	}

	private void UpdatePosition(float deltaTime)
	{
		Node.Position += _direction * deltaTime * Speed;

		if (Node.Position.X < Node.Size.Width / 2.0f)
		{
			_direction = new Vector2(-_direction.X, _direction.Y);
			Node.Position = new(Node.Size.Width / 2.0f, Node.Position.Y);
		}

		if (Node.Position.Y < Node.Size.Height / 2.0f)
		{
			_direction = new Vector2(_direction.X, -_direction.Y);
			Node.Position = new(Node.Position.X, Node.Size.Height / 2.0f);
		}

		Debug.Assert(Node.Parent is not null);

		if (Node.Position.X > Node.Parent.Size.Width - Node.Size.Width / 2.0f)
		{
			_direction = new Vector2(-_direction.X, _direction.Y);
			Node.Position = new(Node.Parent.Size.Width - Node.Size.Width / 2.0f, Node.Position.Y);
		}

		if (Node.Position.Y > Node.Parent.Size.Height - Node.Size.Height / 2.0f)
		{
			_direction = new Vector2(_direction.X, -_direction.Y);
			Node.Position = new(Node.Position.X, Node.Parent.Size.Height - Node.Size.Height / 2.0f);
		}
	}
}
