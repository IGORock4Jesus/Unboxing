namespace Unboxing;

partial class Form1
{
	/// <summary>
	///  Required designer variable.
	/// </summary>
	private System.ComponentModel.IContainer components = null;

	/// <summary>
	///  Clean up any resources being used.
	/// </summary>
	/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
	protected override void Dispose(bool disposing)
	{
		if (disposing && (components != null))
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	#region Windows Form Designer generated code

	/// <summary>
	///  Required method for Designer support - do not modify
	///  the contents of this method with the code editor.
	/// </summary>
	private void InitializeComponent()
	{
		SuspendLayout();
		// 
		// Form1
		// 
		AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
		AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		ClientSize = new System.Drawing.Size(1000, 724);
		FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		Name = "Form1";
		StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		Text = "Unboxing";
		FormClosed += Form1_FormClosed;
		Load += Form1_Load;
		ResizeEnd += Form1_ResizeEnd;
		KeyDown += Form1_KeyDown;
		KeyPress += Form1_KeyPress;
		KeyUp += Form1_KeyUp;
		MouseDown += Form1_MouseDown;
		MouseEnter += Form1_MouseEnter;
		MouseLeave += Form1_MouseLeave;
		MouseMove += Form1_MouseMove;
		MouseUp += Form1_MouseUp;
		ResumeLayout(false);
	}

	#endregion
}
