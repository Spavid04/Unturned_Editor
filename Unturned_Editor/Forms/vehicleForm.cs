using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Unturned_Editor
{
    public partial class vehicleForm : Form
    {
        public Bitmap IMAGE = Properties.Resources.map;
        public Bitmap overlay = new Bitmap(Properties.Resources.map.Width, Properties.Resources.map.Height);
        public int x, y;
        public int positionX, positionY;
        public List<Vehicle> VEHICLES = new List<Vehicle>(); 

        public vehicleForm(string key_value)
        {
            InitializeComponent();

            string[] vehicles = key_value.Split(new char[] {';'}, StringSplitOptions.RemoveEmptyEntries);

            foreach (string S in vehicles)
            {
                Vehicle tempVehicle = new Vehicle();

                string[] values = S.Split(new char[] {':'}, StringSplitOptions.RemoveEmptyEntries);

                tempVehicle.name = values[0];
                tempVehicle.health = Convert.ToInt32(values[1]);
                tempVehicle.gas = Convert.ToInt32(values[2]);
                tempVehicle.x = (float) Convert.ToDouble(values[3]);
                tempVehicle.y = (float) Convert.ToDouble(values[4]);
                tempVehicle.z = (float) Convert.ToDouble(values[5]);
                tempVehicle.Rx = Convert.ToInt32(values[6]);
                tempVehicle.Ry = Convert.ToInt32(values[7]);
                tempVehicle.Rz = Convert.ToInt32(values[8]);
                tempVehicle.R = (float)Convert.ToDouble(values[9]);
                tempVehicle.G = (float)Convert.ToDouble(values[10]);
                tempVehicle.B = (float)Convert.ToDouble(values[11]);

                VEHICLES.Add(tempVehicle);
            }

            foreach (Vehicle V in VEHICLES)
            {
                string toadd = V.name;
                toadd = char.ToUpper(toadd[0]) + toadd.Substring(1);
                listBox1.Items.Add(toadd);
            }

            redraw();

            pictureBox1.Click += pictureBox1_Click;
            pictureBox1.MouseMove += pictureBox1_MouseMove;
            listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            double tempX = (double)e.Location.X / (double)pictureBox1.Size.Width;
            double tempY = (double)e.Location.Y / (double)pictureBox1.Size.Height;

            label3.Text = ((int)(tempX * IMAGE.Width)).ToString() + "x" +
                          ((int)(tempY * IMAGE.Height)).ToString();

            tempX = 2000 * tempX;
            tempY = 2000 * tempY;

            label4.Text = ((int)(tempX - 1000)).ToString() + "x" + ((int)(1000 - tempY)).ToString();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (button2.Text != "Cancel")
                return;
            button2.Text = "Add vehicle";

            positionX = Convert.ToInt32(label4.Text.Split('x')[0]);
            positionY = Convert.ToInt32(label4.Text.Split('x')[1]);

            x = Convert.ToInt32(label3.Text.Split('x')[0]);
            y = Convert.ToInt32(label3.Text.Split('x')[1]);

            Vehicle newVehicle = new Vehicle();

            newVehicle.name = "<new>";
            newVehicle.x = positionX;
            newVehicle.y = 150;
            newVehicle.z = positionY;

            vehicleEditForm form = new vehicleEditForm(newVehicle);
            form.ShowDialog();

            if (form.getVehicle() == null)
            {
                if (listBox1.SelectedIndex >= 0)
                {
                    VEHICLES.RemoveAt(listBox1.SelectedIndex);
                    listBox1.Items.RemoveAt(listBox1.SelectedIndex);
                }
            }
            else
            {
                listBox1.Items.Add(char.ToUpper(form.getVehicle().name[0]) + form.getVehicle().name.Substring(1));
                VEHICLES.Add(form.getVehicle());
            }

            redraw();
        }

        void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            redraw();
        }

        public void redraw()
        {
            overlay = new Bitmap(Properties.Resources.map.Width, Properties.Resources.map.Height);

            using (Graphics G = Graphics.FromImage(overlay))
            {
                foreach (Vehicle V in VEHICLES)
                {
                    int tempx = (int) ((V.x + 1000)*IMAGE.Width/2000);
                    int tempy = (int) ((1000 - V.z)*IMAGE.Height/2000);
                    using (Brush B = new SolidBrush(Color.Magenta))
                    {
                        G.FillEllipse(B, tempx - 10, tempy - 10, 21, 21);
                    }
                    using (
                        Brush B =
                            new SolidBrush(Color.FromArgb(255 - Color.Magenta.R, 255 - Color.Magenta.G,
                                255 - Color.Magenta.B)))
                    {
                        G.FillEllipse(B, tempx - 4, tempy - 4, 9, 9);
                    }

                    int endX, endY;
                    Pen P;

                    if (listBox1.SelectedIndex >= 0)
                    {
                        if (listBox1.SelectedIndex == VEHICLES.IndexOf(V))
                        {
                            endX = (int) (100*Math.Cos(degreesToRadians(45))) + tempx;
                            endY = (int) (100*Math.Sin(degreesToRadians(45))) + tempy;

                            P = new Pen(Color.Tomato, 10);
                            P.StartCap = LineCap.ArrowAnchor;
                            G.DrawLine(P, tempx + 20, tempy + 20, endX, endY);

                            endX = (int)(100 * Math.Cos(degreesToRadians(135))) + tempx;
                            endY = (int)(100 * Math.Sin(degreesToRadians(135))) + tempy;

                            P = new Pen(Color.Tomato, 10);
                            P.StartCap = LineCap.ArrowAnchor;
                            G.DrawLine(P, tempx - 20, tempy + 20, endX, endY);

                            endX = (int)(100 * Math.Cos(degreesToRadians(225))) + tempx;
                            endY = (int)(100 * Math.Sin(degreesToRadians(225))) + tempy;

                            P = new Pen(Color.Tomato, 10);
                            P.StartCap = LineCap.ArrowAnchor;
                            G.DrawLine(P, tempx - 20, tempy - 20, endX, endY);

                            endX = (int)(100 * Math.Cos(degreesToRadians(315))) + tempx;
                            endY = (int)(100 * Math.Sin(degreesToRadians(315))) + tempy;

                            P = new Pen(Color.Tomato, 10);
                            P.StartCap = LineCap.ArrowAnchor;
                            G.DrawLine(P, tempx + 20, tempy - 20, endX, endY);
                        }
                    }

                    endX = (int) (50*Math.Cos(degreesToRadians((double) (V.Rz)))) + tempx;
                    endY = (int) (50*Math.Sin(degreesToRadians((double) (V.Rz)))) + tempy;

                    P = new Pen(Color.Cyan, 4);
                    P.StartCap = LineCap.ArrowAnchor;
                    G.DrawLine(P, endX, endY, tempx, tempy);
                }
            }

            pictureBox1.Image = overlay;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {
                vehicleEditForm form = new vehicleEditForm(VEHICLES[listBox1.SelectedIndex]);
                form.ShowDialog();

                if (form.getVehicle() == null)
                {
                    VEHICLES.RemoveAt(listBox1.SelectedIndex);
                    listBox1.Items.RemoveAt(listBox1.SelectedIndex);
                }
                else
                {
                    VEHICLES.RemoveAt(listBox1.SelectedIndex);
                    listBox1.Items.RemoveAt(listBox1.SelectedIndex);
                    listBox1.Items.Add(char.ToUpper(form.getVehicle().name[0]) + form.getVehicle().name.Substring(1));
                    VEHICLES.Add(form.getVehicle());
                }

                redraw();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == "Cancel")
                button2.Text = "Add vehicle";
            else
                button2.Text = "Cancel";
        }

        public double degreesToRadians(double angle)
        {
            return (Math.PI / 180) * angle;
        }

        public string getVehicles()
        {
            string toreturn = "";

            for (int i = 0; i < VEHICLES.Count; i++)
            {
                toreturn += VEHICLES[i].name + ":";
                toreturn += VEHICLES[i].health + ":";
                toreturn += VEHICLES[i].gas + ":";
                toreturn += VEHICLES[i].x + ":";
                toreturn += VEHICLES[i].y + ":";
                toreturn += VEHICLES[i].z + ":";
                toreturn += VEHICLES[i].Rx + ":";
                toreturn += VEHICLES[i].Ry + ":";
                toreturn += VEHICLES[i].Rz + ":";
                toreturn += Math.Round(VEHICLES[i].R, 2) + ":";
                toreturn += Math.Round(VEHICLES[i].G, 2) + ":";
                toreturn += Math.Round(VEHICLES[i].B, 2) + ":";

                toreturn += ";";
            }

            return toreturn;
        }
    }
}
