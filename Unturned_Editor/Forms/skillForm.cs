using System;
using System.Drawing;
using System.Windows.Forms;

namespace Unturned_Editor
{
    public partial class skillForm : Form
    {
        public skillForm(string key_value)
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

            string[] skills = key_value.Split(new char[] {';'}, StringSplitOptions.RemoveEmptyEntries);

            numericUpDown1.Value = Convert.ToInt32(skills[0]);
            trackBar1.Value = Convert.ToInt32(skills[1]);
            trackBar2.Value = Convert.ToInt32(skills[2]);
            trackBar3.Value = Convert.ToInt32(skills[3]);
            trackBar4.Value = Convert.ToInt32(skills[4]);
            trackBar5.Value = Convert.ToInt32(skills[5]);
            trackBar6.Value = Convert.ToInt32(skills[6]);
            trackBar7.Value = Convert.ToInt32(skills[7]);
            trackBar8.Value = Convert.ToInt32(skills[8]);
        }

        public string getSkills()
        {
            string toreturn = "";

            toreturn += numericUpDown1.Value.ToString() + ";";
            toreturn += trackBar1.Value + ";";
            toreturn += trackBar2.Value + ";";
            toreturn += trackBar3.Value + ";";
            toreturn += trackBar4.Value + ";";
            toreturn += trackBar5.Value + ";";
            toreturn += trackBar6.Value + ";";
            toreturn += trackBar7.Value + ";";
            toreturn += trackBar8.Value + ";";

            return toreturn;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            numericUpDown1.Value = 10000;
            trackBar1.Value = trackBar1.Maximum;
            trackBar2.Value = trackBar2.Maximum;
            trackBar3.Value = trackBar3.Maximum;
            trackBar4.Value = trackBar4.Maximum;
            trackBar5.Value = trackBar5.Maximum;
            trackBar6.Value = trackBar6.Maximum;
            trackBar7.Value = trackBar7.Maximum;
            trackBar8.Value = trackBar8.Maximum;
            
            this.Close();
        }
    }
}
