using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using SharpDX.Direct2D1;
using Unboxing.AssetLoaders;
using Unboxing.ControlComponents;
using Unboxing.NodeSystems;

namespace Unboxing;

public partial class Form1 : Form
{
	const int WIDTH = 800;
	const int HEIGHT = 600;
	readonly Thread _renderThread;
	bool _renderThreadEnabled;
	private bool _needToReload;
	readonly NodeSystemCollection _nodeSystems;

	public Form1()
	{
		InitializeComponent();

		ClientSize = new Size(WIDTH, HEIGHT);
		_renderThread = new(Rendering);

		var mp = MousePosition;
		mp = PointToClient(mp);

		Cursor.Position = new(Cursor.Position.X - mp.X, Cursor.Position.Y - mp.Y);
		_nodeSystems = new NodeSystemCollection();
	}

	private void CreateDemo()
	{
		var parent = new Control()
		{
			Position = new SharpDX.Vector2(100, 100),
			Size = new SharpDX.Size2F(200, 50),
		};

		var sprite = parent.AddComponent<SpriteControlComponent>();
		sprite.Color = SharpDX.Color.GreenYellow;
		sprite.BitmapResourceName = "robot";

		var label = parent.AddComponent<LabelControlComponent>();
		label.Text = "Привет, Мир!!!";

		var child = new Control()
		{
			Position = new(10, 10),
			Size = new(50, 50),
		};

		sprite = child.AddComponent<SpriteControlComponent>();
		sprite.Color = SharpDX.Color.Green;

		label = child.AddComponent<LabelControlComponent>();
		label.Text = "test!";

		child.AddComponent<MouseZoneControlComponent>();

		child.AddComponent<HighlightSpriteControlComponent>();

		parent.AddChild(child);
		HUD.Root.AddChild(parent);
	}

	private void Rendering()
	{
		try
		{
			HUD.Initialize(ClientSize.Width, ClientSize.Height);

			LoadAssets();

			LoadUI("main");

			Game.Initialize();

			var oldTime = Environment.TickCount;

			while (_renderThreadEnabled)
			{
				var newTime = Environment.TickCount;
				var deltaTime = (newTime - oldTime) * 0.001f;
				oldTime = newTime;

				if (_needToReload)
				{
					_needToReload = false;

					ReloadAssets();
					LoadUI("main");
				}

				//Inputs.Update();

				Scene.Update(deltaTime);
				HUD.Update();
				_nodeSystems.Update(deltaTime);

				Graphics.Begin();

				Scene.Render();
				//HUD.Render();

				Graphics.End();
			}

			Game.Release();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString());
		}
	}

	private void LoadUI(string name)
	{
		HUD.Load(name);
	}

	private static void LoadAssets()
	{
		Assets.AddLoader(new TextureAssetLoader());
		Assets.AddLoader(new UIAssetLoader());

		Assets.Load();
	}

	private static void ReloadAssets()
	{
		ClearAssets();

		Assets.Load();
	}

	private static void ClearAssets()
	{
		ResourceRepository.Clear<SharpDX.Direct2D1.Bitmap>();
		ResourceRepository.Clear<UIDocument>();
	}

	private void Form1_Load(object sender, EventArgs e)
	{
		Graphics.Initialize(Handle, WIDTH, HEIGHT);
		// TODO: Пока используем события формы
		// Inputs.Initialize(Handle);
		MouseWheel += Form1_MouseWheel;

		Scene.Initialize(WIDTH, HEIGHT);

		_renderThreadEnabled = true;
		_renderThread.Start();
	}

	private void Form1_FormClosed(object sender, FormClosedEventArgs e)
	{
		_renderThreadEnabled = false;
		if (_renderThread.IsAlive)
		{
			_renderThread.Join();
		}

		ClearAssets();
		HUD.Release();
		Scene.Release();

		Inputs.Release();
		Graphics.Release();
	}

	private void Form1_ResizeEnd(object sender, EventArgs e)
	{
		Graphics.Resize(ClientSize.Width, ClientSize.Height);
	}

	private void ReloadButton_Click(object sender, EventArgs e)
	{
		try
		{
			_needToReload = true;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString());
		}
	}

	private void Form1_KeyDown(object sender, KeyEventArgs e)
	{
		WindowInputs.RaiseKeyDown(e.KeyCode);
	}

	private void Form1_KeyPress(object sender, KeyPressEventArgs e)
	{
		WindowInputs.RaiseKeyPress(e.KeyChar);
	}

	private void Form1_KeyUp(object sender, KeyEventArgs e)
	{
		WindowInputs.RaiseKeyUp(e.KeyCode);
	}

	private void Form1_MouseDown(object sender, MouseEventArgs e)
	{
		WindowInputs.RaiseMouseDown(e.X, e.Y, e.Button, e.Delta);
	}

	private void Form1_MouseUp(object sender, MouseEventArgs e)
	{
		WindowInputs.RaiseMouseUp(e.X, e.Y, e.Button, e.Delta);
	}

	private void Form1_MouseMove(object sender, MouseEventArgs e)
	{
		WindowInputs.RaiseMouseMove(e.X, e.Y, e.Button, e.Delta);
	}

	private void Form1_MouseWheel(object? sender, MouseEventArgs e)
	{
		WindowInputs.RaiseMouseWheel(e.X, e.Y, e.Button, e.Delta);
	}

	private void Form1_MouseEnter(object sender, EventArgs e)
	{

	}

	private void Form1_MouseLeave(object sender, EventArgs e)
	{

	}
}
