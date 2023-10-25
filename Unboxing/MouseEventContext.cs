namespace Unboxing;
internal class MouseEventContext(int x, int y, System.Windows.Forms.MouseButtons button, int wheel)
{
	public int X => x;
	public int Y => y;
	public System.Windows.Forms.MouseButtons Button => button;
	public int Wheel => wheel;
}
