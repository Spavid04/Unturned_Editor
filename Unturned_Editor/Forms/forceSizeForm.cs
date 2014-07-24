using System.Windows.Forms;

namespace Unturned_Editor
{
    public partial class forceSizeForm : Form
    {
        public forceSizeForm()
        {
            InitializeComponent();
        }

        public int[] getSize()
        {
            int[] toreturn = new int[] {-1, -1, -1};

            if (numericUpDown1.Value != -1)
                toreturn[0] = (int) numericUpDown1.Value;
            if (numericUpDown2.Value != -1)
                toreturn[1] = (int) numericUpDown2.Value;
            if ((int) numericUpDown3.Value >= 0)
                toreturn[2] = (int) (numericUpDown3.Value*1000);

            return toreturn;
        }
    }
}
