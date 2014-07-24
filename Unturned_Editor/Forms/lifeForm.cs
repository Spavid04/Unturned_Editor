using System;
using System.Drawing;
using System.Windows.Forms;

namespace Unturned_Editor
{
    public partial class lifeForm : Form
    {
        public lifeForm(string key_value)
        {
            InitializeComponent();

            string[] values = key_value.Split(new char[] {';'}, StringSplitOptions.RemoveEmptyEntries);

            trackBar1.Value = Convert.ToInt32(values[0]);
            trackBar2.Value = 100 - Convert.ToInt32(values[1]);
            trackBar3.Value = 100 - Convert.ToInt32(values[2]);
            trackBar4.Value = 100 - Convert.ToInt32(values[3]);

            groupBox1.Text = "Health     " + trackBar1.Value.ToString() + "%";
            groupBox2.Text = "Hunger     " + trackBar2.Value.ToString() + "%";
            groupBox3.Text = "Thirst     " + trackBar3.Value.ToString() + "%";
            groupBox4.Text = "Disease     " + trackBar4.Value.ToString() + "%";

            if (values[4] == "t")
                checkBox1.Checked = true;
            else
                checkBox1.Checked = false;

            if (values[5] == "t")
                checkBox2.Checked = true;
            else
                checkBox2.Checked = false;

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

        public string getLife()
        {
            string toreturn = "";

            toreturn += trackBar1.Value.ToString() + ";";
            toreturn += (100 - trackBar2.Value).ToString() + ";";
            toreturn += (100 - trackBar3.Value).ToString() + ";";
            toreturn += (100 - trackBar4.Value).ToString() + ";";

            if (checkBox1.Checked)
                toreturn += "t" + ";";
            else
                toreturn += "f" + ";";

            if (checkBox2.Checked)
                toreturn += "t" + ";";
            else
                toreturn += "f" + ";";

            return toreturn;
        }

        private void trackBar1_Scroll(object sender, System.EventArgs e)
        {
            groupBox1.Text = "Health     " + trackBar1.Value.ToString() + "%";
        }

        private void trackBar2_Scroll(object sender, System.EventArgs e)
        {
            groupBox2.Text = "Hunger     " + trackBar2.Value.ToString() + "%";
        }

        private void trackBar3_Scroll(object sender, System.EventArgs e)
        {
            groupBox3.Text = "Thirst     " + trackBar3.Value.ToString() + "%";
        }

        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            groupBox4.Text = "Disease     " + trackBar4.Value.ToString() + "%";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            trackBar1.Value = 100;
            trackBar2.Value = 0;
            trackBar3.Value = 0;
            trackBar4.Value = 0;

            checkBox1.Checked = false;
            checkBox2.Checked = false;

            this.Close();
        }
    }
}