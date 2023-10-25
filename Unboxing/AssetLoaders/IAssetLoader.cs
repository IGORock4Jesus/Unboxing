namespace Unboxing.AssetLoaders;
internal interface IAssetLoader
{
	string Filter { get; }
	string Folder { get; }

	void Load(string path);
}
