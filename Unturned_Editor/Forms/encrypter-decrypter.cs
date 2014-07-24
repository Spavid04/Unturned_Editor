using System;
using System.Windows.Forms;

namespace Unturned_Editor
{
    public partial class encrypter_decrypter : Form
    {
        public encrypter_decrypter()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != null) Clipboard.SetText(Encryption.encrypt(textBox1.Text));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != null) Clipboard.SetText(Encryption.decrypt(textBox1.Text));
        }
    }
}
