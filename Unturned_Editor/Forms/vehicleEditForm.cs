using System.Drawing;
using System.Windows.Forms;

namespace Unturned_Editor
{
    public partial class vehicleEditForm : Form
    {
        public vehicleEditForm(Vehicle vehicle)
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

            if (vehicle.name != "<new>")
            {
                comboBox1.Text = vehicle.name;
            }
            numericUpDown1.Value = vehicle.health;
            numericUpDown2.Value = vehicle.gas;
            numericUpDown3.Value = (int) vehicle.x;
            numericUpDown4.Value = (int) vehicle.y;
            numericUpDown5.Value = (int) vehicle.z;
            numericUpDown6.Value = vehicle.Rx;
            numericUpDown7.Value = vehicle.Ry;
            numericUpDown8.Value = vehicle.Rz;

            button1.BackColor = Color.FromArgb((int) (255*vehicle.R), (int) (255*vehicle.G), (int) (255*vehicle.B));
        }

        public Vehicle getVehicle()
        {
            Vehicle toreturn = new Vehicle();

            if (comboBox1.Text == "null" || comboBox1.Text == "")
                return null;
            else
            {
                toreturn.name = comboBox1.Text;
                toreturn.health = (int) numericUpDown1.Value;
                toreturn.gas = (int) numericUpDown2.Value;
                toreturn.x = (float) numericUpDown3.Value;
                toreturn.y = (float) numericUpDown4.Value;
                toreturn.z = (float) numericUpDown5.Value;
                toreturn.Rx = (int) numericUpDown6.Value;
                toreturn.Ry = (int) numericUpDown7.Value;
                toreturn.Rz = (int) numericUpDown8.Value;
                toreturn.R = (float) button1.BackColor.R/255;
                toreturn.G = (float) button1.BackColor.G/255;
                toreturn.B = (float) button1.BackColor.B/255;

                return toreturn;
            }
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            colorDialog1.Color = button1.BackColor;

            colorDialog1.ShowDialog();

            button1.BackColor = colorDialog1.Color;
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            comboBox1.Text = "null";

            this.Close();
        }
    }
}
