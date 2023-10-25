using System.IO;

namespace Unboxing.AssetLoaders;
internal class TextureAssetLoader() : IAssetLoader
{
	public string Filter => "*.png";
	public string Folder => "textures";

	public void Load(string path)
	{
		using var bitmapSource = TextureLoader.LoadBitmap(new(), path);
		var bitmap = SharpDX.Direct2D1.Bitmap.FromWicBitmap(Graphics.RenderTarget, bitmapSource);

		ResourceRepository.Add(Path.GetFileNameWithoutExtension(path), bitmap);
    }
}
