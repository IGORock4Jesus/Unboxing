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
		var distance = collision.Other.GlobalPosition - collision.Node.GlobalPosition;
		if (MathF.Abs(distance.X) < MathF.Min(collision.Node.Size.Width, collision.Other.Size.Width))
		{
			_direction.X = -_direction.X;
		}

		if (MathF.Abs(distance.Y) < MathF.Min(collision.Node.Size.Height, collision.Other.Size.Height))
		{
			_direction.Y = -_direction.Y;
		}
	}

	protected override void OnUpdate(float deltaTime)
	{
		UpdatePosition(deltaTime);
	}

	private void UpdatePosition(float deltaTime)
	{
		var rigidBody = Node.GetComponent<RigidBodyNodeComponent>();
		rigidBody.SetVelocity(_direction * deltaTime * Speed);

		//var position = Node.Position;
		//position += _direction * deltaTime * Speed;

		//if (position.X < Node.Size.Width / 2.0f)
		//{
		//	_direction = new Vector2(-_direction.X, _direction.Y);
		//	position = new(Node.Size.Width / 2.0f, position.Y);
		//}

		//if (position.Y < Node.Size.Height / 2.0f)
		//{
		//	_direction = new Vector2(_direction.X, -_direction.Y);
		//	position = new(position.X, Node.Size.Height / 2.0f);
		//}

		//Debug.Assert(Node.Parent is not null);

		//if (position.X > Node.Parent.Size.Width - Node.Size.Width / 2.0f)
		//{
		//	_direction = new Vector2(-_direction.X, _direction.Y);
		//	position = new(Node.Parent.Size.Width - Node.Size.Width / 2.0f, position.Y);
		//}

		//if (position.Y > Node.Parent.Size.Height - Node.Size.Height / 2.0f)
		//{
		//	_direction = new Vector2(_direction.X, -_direction.Y);
		//	position = new(position.X, Node.Parent.Size.Height - Node.Size.Height / 2.0f);
		//}

		//rigidBody.SetPosition(position);
	}
}
