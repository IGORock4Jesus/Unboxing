using System;
using System.Xml;

namespace Unboxing.AssetLoaders;
internal class UIDocument(XmlNode node) : IDisposable
{
	public XmlNode Node => node;

	public void Dispose() { }
}
