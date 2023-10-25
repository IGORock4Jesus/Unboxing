using SharpDX;

namespace Unboxing.AssetLoaders;
public static class MathExtensions
{
	public static Vector2 AsVector(this Size2F size)
	{
		return new(size.Width, size.Height);
	}

	public static Size2F AsSize(this Vector2 vector)
	{
		return new(vector.X, vector.Y);
	}
}
