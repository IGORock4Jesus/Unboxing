namespace Unboxing;
public class VerticalAlignment(string value)
{
	public static readonly VerticalAlignment Top = new("top");
	public static readonly VerticalAlignment Middle = new("middle");
	public static readonly VerticalAlignment Bottom = new("bottom");

	public static readonly VerticalAlignment[] All =
	[
		Top,
		Middle,
		Bottom
	];

	public string Value => value;
}
