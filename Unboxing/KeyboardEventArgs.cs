using System.Windows.Forms;

namespace Unboxing;
internal class KeyboardEventArgs(Keys keys, char key)
{
	public Keys Keys => keys;
	public char Key => key;
}
