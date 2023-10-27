using System;
using Box2D.NetStandard.Collision;
using Box2D.NetStandard.Dynamics.Contacts;
using Box2D.NetStandard.Dynamics.World;
using Box2D.NetStandard.Dynamics.World.Callbacks;

namespace Unboxing;
internal class RigidBodyContactListener : ContactListener
{
	public event Action<Contact>? OnBeginContact;
	public event Action<Contact>? OnEndContact;
	public event Action<Contact, ContactImpulse>? OnPostSolve;
	public event Action<Contact, Manifold>? OnPreSolve;

	public override void BeginContact(in Contact contact)
	{
		OnBeginContact?.Invoke(contact);
	}

	public override void EndContact(in Contact contact)
	{
		OnEndContact?.Invoke(contact);
	}

	public override void PostSolve(in Contact contact, in ContactImpulse impulse)
	{
		OnPostSolve?.Invoke(contact, impulse);
	}

	public override void PreSolve(in Contact contact, in Manifold oldManifold)
	{
		OnPreSolve?.Invoke(contact, oldManifold);
	}
}
