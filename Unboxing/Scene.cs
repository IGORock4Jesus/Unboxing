using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Box2D.NetStandard.Collision;
using Box2D.NetStandard.Dynamics.Bodies;
using Box2D.NetStandard.Dynamics.Contacts;
using Box2D.NetStandard.Dynamics.World;
using Box2D.NetStandard.Dynamics.World.Callbacks;
using SharpDX;
using Unboxing.NodeComponents;
using Unboxing.NodeSystems;

namespace Unboxing;
internal static class Scene
{
	public static Node Root { get; } = new();

	private static World? PhysicsWorld;
	private static readonly RigidBodyContactListener ContactListener = new();

	public static void Initialize(int width, int height)
	{
		Root.Size = new(width, height);
		PhysicsWorld = new(
			new System.Numerics.Vector2(0.0f, 0.0f)
		);

		ContactListener.OnBeginContact += ContactListener_OnBeginContact;
		ContactListener.OnEndContact += ContactListener_OnEndContact;
		ContactListener.OnPreSolve += ContactListener_OnPreSolve;
		ContactListener.OnPostSolve += ContactListener_OnPostSolve;

		PhysicsWorld.SetContactListener(ContactListener);
	}

	private static void ContactListener_OnPostSolve(Contact contact, ContactImpulse impulse)
	{
		//foreach (var item in impulse.normalImpulses)
		{
			//Debug.WriteLine("PostSolve normalImpulses: " + new Vector2(impulse.normalImpulses[0], impulse.normalImpulses[1]));
		}
		//foreach (var item in impulse.tangentImpulses)
		//{
		//	Debug.WriteLine("PostSolve tangentImpulses: " + item);
		//}
		var aRigidBody = contact.FixtureA.Body.UserData as RigidBodyNodeComponent;
		var bRigidBody = contact.FixtureB.Body.UserData as RigidBodyNodeComponent;

		Debug.Assert(aRigidBody is not null);
		Debug.Assert(bRigidBody is not null);

		//Debug.WriteLine("Begin Contact A: " + aRigidBody.Node);
		//Debug.WriteLine("Begin Contact B: " + bRigidBody.Node);

		aRigidBody.RaiseCollision(bRigidBody.Node);
		bRigidBody.RaiseCollision(aRigidBody.Node);
	}

	private static void ContactListener_OnPreSolve(Contact contact, Manifold manifold)
	{
		//Debug.WriteLine("PreSolve manifold: " + manifold);

		var s = manifold.ToString();
	}

	private static void ContactListener_OnEndContact(Contact contact)
	{
		var aRigidBody = contact.FixtureA.Body.UserData as RigidBodyNodeComponent;
		var bRigidBody = contact.FixtureB.Body.UserData as RigidBodyNodeComponent;

		Debug.Assert(aRigidBody is not null);
		Debug.Assert(bRigidBody is not null);

		//Debug.WriteLine("End Contact A: " + aRigidBody.Node);
		//Debug.WriteLine("End Contact B: " + bRigidBody.Node);

	}

	private static void ContactListener_OnBeginContact(Contact contact)
	{
		//var aRigidBody = contact.FixtureA.Body.UserData as RigidBodyNodeComponent;
		//var bRigidBody = contact.FixtureB.Body.UserData as RigidBodyNodeComponent;

		//Debug.Assert(aRigidBody is not null);
		//Debug.Assert(bRigidBody is not null);

		////Debug.WriteLine("Begin Contact A: " + aRigidBody.Node);
		////Debug.WriteLine("Begin Contact B: " + bRigidBody.Node);

		//aRigidBody.RaiseCollision(bRigidBody.Node);
		//bRigidBody.RaiseCollision(aRigidBody.Node);
	}

	internal static void Release()
	{
		Clear();
	}

	private static void Clear()
	{
		Root.ClearComponentsAndChildren();
	}

	public static void Update(float deltaTime)
	{
		Debug.Assert(PhysicsWorld is not null);

		Root.Update(deltaTime);
		PhysicsWorld.Step(deltaTime, 3, 2);
	}

	public static void Render()
	{
		Root.Render();
	}

	internal static NodeCollection GetWithAll(IEnumerable<Type> componentTypes)
	{
		var result = new List<Node>();
		if (componentTypes.Any())
		{
			GetWithAll(Root, componentTypes, result);
		}

		return new(result);
	}

	private static void GetWithAll(Node node, IEnumerable<Type> componentTypes, List<Node> result)
	{
		var hasAll = true;
		foreach (var type in componentTypes)
		{
			if (!node.HasComponent(type))
			{
				hasAll = false;
				break;
			}
		}

		if (hasAll)
		{
			result.Add(node);
		}

		foreach (var child in node.Children)
		{
			GetWithAll(child, componentTypes, result);
		}
	}

	public static Body CreateRigidBody(Vector2 position, Size2F size, bool isDynamic = false)
	{
		Debug.Assert(PhysicsWorld is not null);

		var bodyDef = new BodyDef()
		{
			position = new System.Numerics.Vector2(position.X, position.Y),
			type = isDynamic ? BodyType.Dynamic : BodyType.Static
		};
		var body = PhysicsWorld.CreateBody(bodyDef);

		var shape = new Box2D.NetStandard.Collision.Shapes.PolygonShape(
			new System.Numerics.Vector2(-size.Width / 2.0f, -size.Height / 2.0f),
			new System.Numerics.Vector2(size.Width / 2.0f, -size.Height / 2.0f),
			new System.Numerics.Vector2(size.Width / 2.0f, size.Height / 2.0f),
			new System.Numerics.Vector2(-size.Width / 2.0f, size.Height / 2.0f)
		);

		var fixtureDef = new Box2D.NetStandard.Dynamics.Fixtures.FixtureDef()
		{
			density = 1.0f,
			friction = 0.0f,
			restitution = 1.0f,
			shape = shape,
		};
		body.CreateFixture(fixtureDef);

		return body;
	}

	internal static void RemoveRigidBody(Body body)
	{
		Debug.Assert(PhysicsWorld is not null);

		PhysicsWorld.DestroyBody(body);
	}
}
