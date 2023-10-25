using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using SharpDX;
using Unboxing.AssetLoaders;
using Unboxing.BoundsConverters;
using Unboxing.ControlComponents;

namespace Unboxing;
internal static partial class HUD
{
	private static Vector2 MousePosition = new(0.0f, 0.0f);
	private static readonly List<IBoundsConverter> BoundsConverters =
	[
		new PixelBoundsConverter()
	];

	public static Control Root { get; } = new();
	public static event Action<Vector2>? MousePositionChanged;

	public static void Initialize(int width, int height)
	{
		Root.Size = new(width, height);
	}
	internal static void Release()
	{
		Clear();
	}

	public static void Update()
	{
		MousePosition += Inputs.GetMouseOffset();
		MousePositionChanged?.Invoke(MousePosition);
	}

	public static void Render()
	{
		Root.Render();
	}

	internal static void Load(string name)
	{
		var document = ResourceRepository.Get<UIDocument>(name);
		Clear();

		ParseNodes(document.Node.ChildNodes, Root);
	}

	private static void ParseNodes(XmlNodeList nodes, Control parent)
	{
		foreach (XmlNode node in nodes)
		{
			var control = new Control
			{
				Position = new(
					ToPixels(node.Attributes?["x"]?.Value),
					ToPixels(node.Attributes?["y"]?.Value)
				),
				Size = new(
					ToPixels(node.Attributes?["width"]?.Value),
					ToPixels(node.Attributes?["height"]?.Value)
				)
			};

			TryParseAlignments(node, control);

			TryParseSprite(node, control);

			TryParseLabel(node, control);

			parent.AddChild(control);

			ParseNodes(node.ChildNodes, control);
		}
	}

	private static void TryParseAlignments(XmlNode node, Control control)
	{
		var value = node.Attributes?["horizontal-align"]?.Value;
		if (!string.IsNullOrEmpty(value))
		{
			foreach (var alignment in HorizontalAlignment.All)
			{
				if (value == alignment.Value)
				{
					control.HorizontalAlignment = alignment;
					break;
				}
			}
		}

		value = node.Attributes?["vertical-align"]?.Value;
		if (!string.IsNullOrEmpty(value))
		{
			foreach (var alignment in VerticalAlignment.All)
			{
				if (value == alignment.Value)
				{
					control.VerticalAlignment = alignment;
					break;
				}
			}
		}
	}

	private static void TryParseLabel(XmlNode node, Control control)
	{
		if (node.Name == "label")
		{
			var label = control.AddComponent<LabelControlComponent>();
			label.Color = ToColor(node.Attributes?["foreground-color"]?.Value);
			label.FontFamily = node.Attributes?["font-family"]?.Value ?? "Consolas";
			label.FontSize = ToPixels(node.Attributes?["font-size"]?.Value);
			label.Text = node.InnerText.Trim();
		}
	}

	private static void TryParseSprite(XmlNode node, Control control)
	{
		if (node.Name == "panel")
		{
			var sprite = control.AddComponent<SpriteControlComponent>();
			sprite.BitmapResourceName = node.Attributes?["image"]?.Value;
			sprite.Color = ToColor(node.Attributes?["background-color"]?.Value);
		}
	}

	private static Color ToColor(string? value)
	{
		if (string.IsNullOrEmpty(value))
		{
			return Color.Transparent;
		}

		var regex = RGBAColorRegex();
		var match = regex.Match(value);
		if (match.Success)
		{
			var groups = match.Groups;

			var r = Convert.ToByte(groups["R"].Value, 16);
			var g = Convert.ToByte(groups["G"].Value, 16);
			var b = Convert.ToByte(groups["B"].Value, 16);

			var alphaChannel = groups["A"].Value;
			var a = 255;
			if (!string.IsNullOrEmpty(alphaChannel))
			{
				a = Convert.ToByte(alphaChannel, 16);
			}

			return new Color(r, g, b, a);
		}
		else
		{
			var field = typeof(Color)
				.GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static)
				.FirstOrDefault(x => x.Name.Equals(value, StringComparison.CurrentCultureIgnoreCase));

			return (Color?)field?.GetValue(null) ?? Color.Transparent;
		}
	}

	private static float ToPixels(string? value)
	{
		if (string.IsNullOrEmpty(value))
		{
			return 0.0f;
		}

		foreach (var converter in BoundsConverters)
		{
			if (converter.Can(value))
			{
				return converter.Convert(value);
			}
		}

		Debug.Fail($"Bounds converter for {value} value is not found");

		return 0.0f;
	}

	private static void Clear()
	{
		Root.ClearComponentsAndChildren();
	}

	[GeneratedRegex("[#]{1}(?<R>[0-9a-fA-F]{2})(?<G>[0-9a-fA-F]{2})(?<B>[0-9a-fA-F]{2})(?<A>[0-9a-fA-F]{2})?")]
	private static partial Regex RGBAColorRegex();
}
