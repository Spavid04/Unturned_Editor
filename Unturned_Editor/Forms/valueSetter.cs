using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Unturned_Editor
{
    public partial class valueSetter : Form
    {
        public valueSetter()
        {
            InitializeComponent();

            List<string> KEYS =
                 new List<string>(
                     Registry.CurrentUser.OpenSubKey("Software\\Smartly Dressed Games\\Unturned").GetValueNames());
            KEYS.Sort();

            foreach (string S in KEYS)
                listBox1.Items.Add(S);
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            RegistryKey finalKey =
                    Registry.CurrentUser.OpenSubKey("Software\\Smartly Dressed Games\\Unturned\\", true);
            if (listBox1.SelectedItem != null) finalKey.SetValue(listBox1.SelectedItem.ToString(), textBox1.Text);
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            RegistryKey finalKey =
                    Registry.CurrentUser.OpenSubKey("Software\\Smartly Dressed Games\\Unturned\\", true);
            if (listBox1.SelectedItem != null) finalKey.DeleteValue(listBox1.SelectedItem.ToString());

            button3.PerformClick();
        }

        private void listBox1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            textBox1.Text = Registry.CurrentUser.OpenSubKey("Software\\Smartly Dressed Games\\Unturned\\")
                .GetValue(listBox1.SelectedItem.ToString())
                .ToString();
        }

        private void button3_Click(object sender, System.EventArgs e)
        {
            List<string> KEYS =
                new List<string>(
                    Registry.CurrentUser.OpenSubKey("Software\\Smartly Dressed Games\\Unturned").GetValueNames());
            KEYS.Sort();

            listBox1.Items.Clear();
            foreach (string S in KEYS)
                listBox1.Items.Add(S);
        }
    }
}
