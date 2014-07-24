using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Unturned_Editor
{
    public partial class attachForm : Form
    {
        //mode, tactical, barrel, sight, magazine, bullets, tac light/laser ON/OFF
        private string Weapon = "";

        public attachForm(string weapon, int[] attachments)
        {
            InitializeComponent();

            bool[] possible_attachments = new bool[] {true, true, true};

            #region Filters attachments by weapon

            switch (weapon.ToLower())
            {
                case "berette":
                    possible_attachments[0] = false;
                    comboBox1.Items.Add("Safety");
                    comboBox1.Items.Add("Semi");
                    comboBox5.Items.Add("Lebel Magazine");
                    numericUpDown1.Maximum = 13;
                    break;
                case "colt":
                    possible_attachments[0] = false;
                    comboBox1.Items.Add("Safety");
                    comboBox1.Items.Add("Semi");
                    comboBox5.Items.Add("Swift Magazine");
                    numericUpDown1.Maximum = 7;
                    break;
                case "desert falcon":
                    possible_attachments[0] = false;
                    comboBox1.Items.Add("Safety");
                    comboBox1.Items.Add("Semi");
                    comboBox5.Items.Add("Swift Magazine");
                    numericUpDown1.Maximum = 7;
                    break;
                case "magnum":
                    possible_attachments[0] = false;
                    comboBox1.Items.Add("Semi");
                    comboBox5.Items.Add("Winchestre Clip");
                    numericUpDown1.Maximum = 6;
                    break;
                case "lever action":
                    possible_attachments[1] = false;
                    comboBox1.Items.Add("Semi");
                    comboBox5.Items.Add("Buckshot");
                    comboBox5.Items.Add("Slug");
                    numericUpDown1.Maximum = 4;
                    break;
                case "double barrel":
                    possible_attachments[1] = false;
                    comboBox1.Items.Add("Semi");
                    comboBox5.Items.Add("Buckshot");
                    comboBox5.Items.Add("Slug");
                    numericUpDown1.Maximum = 2;
                    break;
                case "novuh":
                    possible_attachments[0] = false;
                    comboBox1.Items.Add("Safety");
                    comboBox1.Items.Add("Semi");
                    comboBox5.Items.Add("Buckshot");
                    comboBox5.Items.Add("Slug");
                    numericUpDown1.Maximum = 5;
                    break;
                case "mosen":
                    comboBox1.Items.Add("Semi");
                    comboBox5.Items.Add("Bonjour Clip");
                    numericUpDown1.Maximum = 5;
                    break;
                case "outfield":
                    comboBox1.Items.Add("Semi");
                    comboBox5.Items.Add("Bonjour Clip");
                    numericUpDown1.Maximum = 5;
                    break;
                case "maplestrike":
                    comboBox1.Items.Add("Safety");
                    comboBox1.Items.Add("Semi");
                    comboBox1.Items.Add("Auto");
                    comboBox5.Items.Add("NATO Magazine");
                    comboBox5.Items.Add("NATO Tracer");
                    comboBox5.Items.Add("NATO Drum");
                    numericUpDown1.Maximum = 100;
                    break;
                case "swissgewehr":
                    comboBox1.Items.Add("Safety");
                    comboBox1.Items.Add("Semi");
                    comboBox5.Items.Add("NATO Magazine");
                    comboBox5.Items.Add("NATO Tracer");
                    comboBox5.Items.Add("NATO Drum");
                    numericUpDown1.Maximum = 100;
                    break;
                case "zubeknakov":
                    comboBox1.Items.Add("Semi");
                    comboBox1.Items.Add("Safety");
                    comboBox5.Items.Add("Savage Magazine");
                    comboBox5.Items.Add("Savage Drum");
                    numericUpDown1.Maximum = 75;
                    break;
                case "uzy":
                    comboBox1.Items.Add("Auto");
                    comboBox5.Items.Add("Yuri Magazine");
                    numericUpDown1.Maximum = 40;
                    break;
                case "proninety":
                    comboBox1.Items.Add("Safety");
                    comboBox1.Items.Add("Semi");
                    comboBox1.Items.Add("Auto");
                    comboBox5.Items.Add("PDW Magazine");
                    numericUpDown1.Maximum = 50;
                    break;
                case "Matamorez":
                    possible_attachments[1] = false;
                    comboBox1.Items.Add("Auto");
                    comboBox5.Items.Add("Xtrmin Magazine");
                    numericUpDown1.Maximum = 10;
                    break;
                case "timberwolf":
                    possible_attachments[1] = false;
                    comboBox1.Items.Add("Safety");
                    comboBox1.Items.Add("Semi");
                    comboBox5.Items.Add("Lapua Magazine");
                    comboBox5.Items.Add("Lapua Tracer");
                    numericUpDown1.Maximum = 8;
                    break;
                case "longbow":
                    possible_attachments[0] = false;
                    possible_attachments[1] = false;
                    possible_attachments[2] = false;
                    comboBox1.Items.Add("Semi");
                    comboBox5.Items.Add("Arrow");
                    numericUpDown1.Maximum = 1;
                    break;
                case "compound bow":
                    possible_attachments[1] = false;
                    comboBox1.Items.Add("Semi");
                    comboBox5.Items.Add("Arrow");
                    numericUpDown1.Maximum = 1;
                    break;
                case "crossbow":
                    possible_attachments[1] = false;
                    comboBox1.Items.Add("Semi");
                    comboBox5.Items.Add("Arrow");
                    numericUpDown1.Maximum = 1;
                    break;
                case "<unknown>":
                    comboBox1.Items.Add("Safety");
                    comboBox1.Items.Add("Semi");
                    comboBox1.Items.Add("Auto");

                    comboBox5.Items.Add("Swift Magazine");
                    comboBox5.Items.Add("Lebel Magazine");
                    comboBox5.Items.Add("Winchester Clip");
                    comboBox5.Items.Add("Buckshot");
                    comboBox5.Items.Add("Slug");
                    comboBox5.Items.Add("Bonjour Clip");
                    comboBox5.Items.Add("NATO Magazine");
                    comboBox5.Items.Add("NATO Tracer");
                    comboBox5.Items.Add("NATO Drum");
                    comboBox5.Items.Add("Savage Magazine");
                    comboBox5.Items.Add("Savage Drum");
                    comboBox5.Items.Add("Yuri Magazine");
                    comboBox5.Items.Add("Lapua Magazine");
                    comboBox5.Items.Add("Lapua Tracer");
                    comboBox5.Items.Add("Arrow");
                    break;
            }

            #endregion

            #region Enable possible attachment slots

            if (possible_attachments[2]) comboBox2.Enabled = true;
            if (possible_attachments[1]) comboBox3.Enabled = true;
            if (possible_attachments[0]) comboBox4.Enabled = true;

            #endregion

            #region Sync the attachments to the weapon

            switch (attachments[0])
            {
                case 0:
                    comboBox1.Text = "Safety";
                    break;
                case 1:
                    comboBox1.Text = "Semi";
                    break;
                case 2:
                    comboBox1.Text = "Auto";
                    break;
            }
            comboBox2.Text = Items.getItem(attachments[3]);
            comboBox3.Text = Items.getItem(attachments[2]);
            comboBox4.Text = Items.getItem(attachments[1]);
            comboBox5.Text = Items.getItem(attachments[4]);
            numericUpDown1.Value = attachments[5];
            if (attachments[6] == 1)
                checkBox1.Checked = true;
            else
                checkBox1.Checked = false;

            #endregion

            Weapon = weapon;

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

            #region Events

            this.Text += weapon.ToUpper();
            comboBox5.SelectedIndexChanged += comboBox5_SelectedIndexChanged;

            #endregion
        }

        #region Change maximum ammo with respect to ammo type

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox5.Text.ToLower())
            {
                case "no ammo":
                    numericUpDown1.Value = 0;
                    numericUpDown1.Maximum = 0;
                    break;
                case "swift magazine":
                    numericUpDown1.Value = 0;
                    numericUpDown1.Maximum = 7;
                    break;
                case "winchestre clip":
                    numericUpDown1.Value = 0;
                    numericUpDown1.Maximum = 6;
                    break;
                case "lebel magazine":
                    numericUpDown1.Value = 0;
                    numericUpDown1.Maximum = 13;
                    break;
                case "buckshot":
                    goto case "slug";
                case "slug":
                    switch (Weapon.ToLower())
                    {
                        case "double barrel":
                            numericUpDown1.Value = 0;
                            numericUpDown1.Maximum = 2;
                            break;
                        case "lever action":
                            numericUpDown1.Value = 0;
                            numericUpDown1.Maximum = 4;
                            break;
                        case "novuh":
                            numericUpDown1.Value = 0;
                            numericUpDown1.Maximum = 5;
                            break;
                        default:
                            numericUpDown1.Value = 0;
                            numericUpDown1.Maximum = 100;
                            break;
                    }
                    break;
                case "lapua magazine":
                    goto case "lapua tracer";
                case "lapua tracer":
                    numericUpDown1.Value = 0;
                    numericUpDown1.Maximum = 8;
                    break;
                case "nato magazine":
                    numericUpDown1.Value = 0;
                    numericUpDown1.Maximum = 30;
                    break;
                case "nato tracer":
                    numericUpDown1.Value = 0;
                    numericUpDown1.Maximum = 20;
                    break;
                case "nato drum":
                    numericUpDown1.Value = 0;
                    numericUpDown1.Maximum = 100;
                    break;
                case "savage magazine":
                    numericUpDown1.Value = 0;
                    numericUpDown1.Maximum = 30;
                    break;
                case "savage drum":
                    numericUpDown1.Value = 0;
                    numericUpDown1.Maximum = 75;
                    break;
                case "arrow":
                    numericUpDown1.Value = 0;
                    numericUpDown1.Maximum = 1;
                    break;
            }
        }

        #endregion

        #region Function accessible from any other class. Used for retrieving changed attachments

        public int[] getAttachments()
        {
            List<int> toreturn = new List<int>();
            switch (comboBox1.Text)
            {
                case "Safety":
                    toreturn.Add(0);
                    break;
                case "Semi":
                    toreturn.Add(1);
                    break;
                case "Auto":
                    toreturn.Add(2);
                    break;
            }
            if (comboBox4.Text != "No Tactical")
                toreturn.Add(Items.getID(comboBox4.Text));
            else
                toreturn.Add(-1);
            if (comboBox3.Text != "No Barrel")
                toreturn.Add(Items.getID(comboBox3.Text));
            else
                toreturn.Add(-1);
            if (comboBox2.Text != "No Sight")
                toreturn.Add(Items.getID(comboBox2.Text));
            else
                toreturn.Add(-1);
            if (comboBox5.Text != "No Ammo")
                toreturn.Add(Items.getID(comboBox5.Text));
            else
                toreturn.Add(-1);
            toreturn.Add((int) numericUpDown1.Value);
            if (checkBox1.Checked)
                toreturn.Add(1);
            else
                toreturn.Add(0);
            return toreturn.ToArray();
        }

        #endregion
    }
}