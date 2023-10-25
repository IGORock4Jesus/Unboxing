using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SharpDX.Direct2D1;
using Unboxing.AssetLoaders;

namespace Unboxing;
internal static class Assets
{
	private const string AssetsFolder = "Assets";
	static readonly List<IAssetLoader> Loaders = [];

	public static void AddLoader(IAssetLoader loader)
	{
		if (Loaders.FirstOrDefault(x=>x.GetType() == loader.GetType()) != null)
		{
			throw new Exception($"The {loader.GetType()} is already added to assets");
		}

		Loaders.Add(loader);
	}

	public static void Load()
	{
		foreach (var loader in Loaders)
		{
			EnumerateFiles(loader.Folder, loader.Filter, loader.Load);
		}
	}

	private static void EnumerateFiles(string folder, string filter, Action<string> callback)
	{
		folder = Path.Combine(AssetsFolder, folder);

		var directories = Directory.EnumerateDirectories(folder);
		foreach (var directory in directories)
		{
			EnumerateFiles(Path.Combine(folder, directory), filter, callback);
		}

		var files = Directory.EnumerateFiles(folder, filter);
		foreach (var filename in files)
		{
			callback(filename);
		}
	}
}
