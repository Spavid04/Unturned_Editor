using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Unturned_Editor
{
    public partial class backupForm : Form
    {
        public backupForm()
        {
            InitializeComponent();

            #region Set the location of the open form to where the click was (aesthetics :) )

            Rectangle resolution = Screen.PrimaryScreen.Bounds;
            Point cursor = Cursor.Position;
            if (cursor.X + this.Width > resolution.Width)
            {
                cursor.X -= this.Width;
            }
            if (cursor.Y + this.Height > resolution.Height)
            {
                cursor.Y -= this.Height;
            }
            this.Location = cursor;

            #endregion
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result =
                MessageBox.Show(
                    "This will completely erase any stored data (including settings) and then restore the ones chosen.\nThere are high chances of failure if the game is running.",
                    "Are you sure?", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                DialogResult result2 = openFileDialog1.ShowDialog();
                if (result2 == DialogResult.OK)
                {
                    Process regeditProcess = Process.Start("regedit.exe", "/s " + openFileDialog1.FileName);
                    regeditProcess.WaitForExit();

                    MessageBox.Show("Success!");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = saveFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                string path = "\"" + saveFileDialog1.FileName + "\"";
                string key = "\"" + "HKEY_CURRENT_USER\\Software\\Smartly Dressed Games\\Unturned\\" + "\"";

                var proc = new Process();
                try
                {
                    proc.StartInfo.FileName = "regedit.exe";
                    proc.StartInfo.UseShellExecute = false;
                    proc = Process.Start("regedit.exe", "/e " + path + " " + key);

                    if (proc != null) proc.WaitForExit();
                }
                finally
                {
                    if (proc != null) proc.Dispose();
                }

                MessageBox.Show("Success!");
            }
        }
    }
}
