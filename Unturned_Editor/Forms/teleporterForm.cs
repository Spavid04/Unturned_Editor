using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Unturned_Editor
{
    public partial class teleporterForm : Form
    {
        public Bitmap IMAGE = Properties.Resources.map;
        public Bitmap overlay = new Bitmap(Properties.Resources.map.Width, Properties.Resources.map.Height);
        public int x, y;
        public int positionX, positionY;

        public teleporterForm(string key_value)
        {
            InitializeComponent();

            string[] values = key_value.Split(new char[] {';'}, StringSplitOptions.RemoveEmptyEntries);

            positionX = (int) Convert.ToDouble(values[0]);
            positionY = (int) Convert.ToDouble(values[2]);

            x = (positionX + 1000)*IMAGE.Width/2000;
            y = (1000 - positionY)*IMAGE.Width/2000;

            numericUpDown1.Value = (int) Convert.ToDouble(values[3]);
            numericUpDown2.Value = (decimal) Convert.ToDouble(values[1]);

            label5.Text = "Your position: " + positionX.ToString() + "x" + positionY.ToString();

            redraw();

            pictureBox1.Click += pictureBox1_Click;
            pictureBox1.MouseMove += pictureBox1_MouseMove;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            numericUpDown2.Value = 150;

            positionX = Convert.ToInt32(label2.Text.Split('x')[0]);
            positionY = Convert.ToInt32(label2.Text.Split('x')[1]);

            x = Convert.ToInt32(label7.Text.Split('x')[0]);
            y = Convert.ToInt32(label7.Text.Split('x')[1]);

            label5.Text = "Your position: " + positionX.ToString() + "x" + positionY.ToString();

            redraw();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            double tempX = (double) e.Location.X/(double) pictureBox1.Size.Width;
            double tempY = (double) e.Location.Y/(double) pictureBox1.Size.Height;

            label7.Text = ((int) (tempX*IMAGE.Width)).ToString() + "x" +
                          ((int) (tempY*IMAGE.Height)).ToString();

            tempX = 2000*tempX;
            tempY = 2000*tempY;

            label2.Text = ((int) (tempX - 1000)).ToString() + "x" + ((int) (1000 - tempY)).ToString();
        }

        public string getPosition()
        {
            string toreturn = "";

            toreturn += positionX.ToString() + ";";
            toreturn += numericUpDown2.Value.ToString() + ";";
            toreturn += positionY.ToString() + ";";
            toreturn += numericUpDown1.Value.ToString() + ";";

            return toreturn;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown1.Value == 360)
            {
                numericUpDown1.Value = 0;
                return;
            }
            if (numericUpDown1.Value == -1)
            {
                numericUpDown1.Value = 359;
                return;
            }
            redraw();
        }

        private void redraw()
        {
            overlay = new Bitmap(Properties.Resources.map.Width, Properties.Resources.map.Height);

            using (Graphics G = Graphics.FromImage(overlay))
            {
                using (Brush B = new SolidBrush(Color.Magenta))
                {
                    G.FillEllipse(B, x - 10, y - 10, 21, 21);
                }
                using (
                    Brush B =
                        new SolidBrush(Color.FromArgb(255 - Color.Magenta.R, 255 - Color.Magenta.G,
                            255 - Color.Magenta.B)))
                {
                    G.FillEllipse(B, x - 4, y - 4, 9, 9);
                }

                int endX = (int)(100 * Math.Cos(degreesToRadians(45))) + x;
                int endY = (int)(100 * Math.Sin(degreesToRadians(45))) + y;

                Pen P = new Pen(Color.Tomato, 10);
                P.StartCap = LineCap.ArrowAnchor;
                G.DrawLine(P, x + 20, y + 20, endX, endY);

                endX = (int)(100 * Math.Cos(degreesToRadians(135))) + x;
                endY = (int)(100 * Math.Sin(degreesToRadians(135))) + y;

                P = new Pen(Color.Tomato, 10);
                P.StartCap = LineCap.ArrowAnchor;
                G.DrawLine(P, x - 20, y + 20, endX, endY);

                endX = (int)(100 * Math.Cos(degreesToRadians(225))) + x;
                endY = (int)(100 * Math.Sin(degreesToRadians(225))) + y;

                P = new Pen(Color.Tomato, 10);
                P.StartCap = LineCap.ArrowAnchor;
                G.DrawLine(P, x - 20, y - 20, endX, endY);

                endX = (int)(100 * Math.Cos(degreesToRadians(315))) + x;
                endY = (int)(100 * Math.Sin(degreesToRadians(315))) + y;

                P = new Pen(Color.Tomato, 10);
                P.StartCap = LineCap.ArrowAnchor;
                G.DrawLine(P, x + 20, y - 20, endX, endY);

                endX = (int)(50 * Math.Cos(degreesToRadians((double)(numericUpDown1.Value - 90)))) + x;
                endY = (int)(50 * Math.Sin(degreesToRadians((double)(numericUpDown1.Value - 90)))) + y;

                P = new Pen(Color.Cyan, 6);
                P.StartCap = LineCap.ArrowAnchor;
                G.DrawLine(P, endX, endY, x, y);
            }

            pictureBox1.Image = overlay;
        }

        public double degreesToRadians(double angle)
        {
            return (Math.PI/180)*angle;
        }
    }
}