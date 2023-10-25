using System.IO;
using System.Xml;

namespace Unboxing.AssetLoaders;
internal class UIAssetLoader() : IAssetLoader
{
	public string Filter => "*.xml";
	public string Folder => "ui";

	public void Load(string path)
	{
		var document = new XmlDocument();
		document.Load(path);
		var root = document.ChildNodes[1] ?? throw new System.Exception("Root ui node is not found");

		ResourceRepository.Add(Path.GetFileNameWithoutExtension(path), new UIDocument(root));
	}
}
