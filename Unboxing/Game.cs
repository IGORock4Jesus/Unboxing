using SharpDX;
using Unboxing.AssetLoaders;
using Unboxing.NodeComponents;

namespace Unboxing;
internal static class Game
{
	public static void Initialize()
	{
		CreateRacquet();
		CreateBall();
		CreateBricks();
	}

	internal static void Release()
	{
	}

	private static void CreateBricks()
	{
		var padding = 4.0f;
		var size = new Size2F(Scene.Root.Size.Width / 12.0f,
			Scene.Root.Size.Height / 2.0f / 12.0f);

		for (var y = 0; y < 10; y++)
		{
			for (var x = 0; x < 10; x++)
			{
				var position = new Vector2(
					(x + 2.0f) * size.Width + padding,
					(y + 3.0f) * size.Height + padding
				);

				var node = new Node()
				{
					Size = (size.AsVector() - new Vector2(padding * 2.0f)).AsSize(),
					Position = position - size.AsVector() / 2.0f
				};

				var rectangle = node.AddComponent<RectangleNodeComponent>();
				rectangle.Color = Color.Brown;

				node.AddComponent<RigidBodyNodeComponent>();

				Scene.Root.AddChild(node);
			}
		}
	}

	private static void CreateBall()
	{
		var position = Scene.Root.Size.AsVector() / 2.0f;
		var size = new Size2F(16.0f, 16.0f);

		var node = new Node()
		{
			Size = size,
			Position = position - size.AsVector() / 2.0f
		};

		var ellipse = node.AddComponent<EllipseNodeComponent>();
		ellipse.Color = Color.Red;

		node.AddComponent<RigidBodyNodeComponent>();

		var ball = node.AddComponent<BallNodeComponent>();
		ball.Speed = 1000.0f;

		Scene.Root.AddChild(node);
	}

	private static void CreateRacquet()
	{
		var size = new Size2F(100.0f, 20.0f);
		var position = new Vector2(Scene.Root.Size.Width / 2.0f, Scene.Root.Size.Height - size.Height * 2.0f);

		var node = new Node()
		{
			Size = size,
			Position = position - size.AsVector() / 2.0f
		};

		var rectangle = node.AddComponent<RectangleNodeComponent>();
		rectangle.Color = Color.GreenYellow;

		node.AddComponent<RigidBodyNodeComponent>();

		node.AddComponent<RacquetControllerNodeComponent>();

		Scene.Root.AddChild(node);
	}

}
