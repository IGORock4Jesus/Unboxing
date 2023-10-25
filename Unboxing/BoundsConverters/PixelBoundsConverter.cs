using System.Diagnostics;

namespace Unboxing.BoundsConverters;
internal class PixelBoundsConverter : IBoundsConverter
{
	public bool Can(string value)
	{
		return value.EndsWith("px");
	}

	public float Convert(string value)
	{
		var index = value.IndexOf("px");
		value = value[..index];

		if (float.TryParse(value, out var result))
		{
			return result;
		}

		Debug.Fail($"Cannot to convert value {value} to pixels");

		return 0.0f;
	}
}
