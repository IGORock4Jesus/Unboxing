namespace Unboxing;
public class HorizontalAlignment(string value)
{
	public static readonly HorizontalAlignment Left = new("left");
	public static readonly HorizontalAlignment Center = new("center");
	public static readonly HorizontalAlignment Right = new("right");

	public static readonly HorizontalAlignment[] All =
	[
		Left,
		Center,
		Right
	];

	public string Value => value;
}
