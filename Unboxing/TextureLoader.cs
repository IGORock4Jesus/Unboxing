namespace Unboxing;

public class TextureLoader
{
	/// <summary>
	/// Loads a bitmap using WIC.
	/// </summary>
	/// <param name="deviceManager"></param>
	/// <param name="filename"></param>
	/// <returns></returns>
	public static SharpDX.WIC.BitmapSource LoadBitmap(SharpDX.WIC.ImagingFactory2 factory, string filename)
	{
		var bitmapDecoder = new SharpDX.WIC.BitmapDecoder(
			factory,
			filename,
			SharpDX.WIC.DecodeOptions.CacheOnDemand
			);

		var formatConverter = new SharpDX.WIC.FormatConverter(factory);

		formatConverter.Initialize(
			bitmapDecoder.GetFrame(0),
			SharpDX.WIC.PixelFormat.Format32bppPRGBA,
			SharpDX.WIC.BitmapDitherType.None,
			null,
			0.0,
			SharpDX.WIC.BitmapPaletteType.Custom);

		return formatConverter;
	}
}