using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Unturned_Editor
{
    public partial class openForm : Form
    {
        private string keyName = "";

        public openForm(string[] items, bool multiselect)
        {
            InitializeComponent();

            if (multiselect)
                listBox1.SelectionMode = SelectionMode.MultiExtended;

            #region Add all openable keys to the list

            foreach (string S in items)
            {
                listBox1.Items.Add(S);
            }

            #endregion

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

            listBox1.DoubleClick += listBox1_DoubleClick;

            #endregion
        }

        //stores the selected item, then closes the form
        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                keyName = listBox1.SelectedItem.ToString();
                this.Close();
            }
        }

        //accessible from any other class, used for returning the selected item(s)
        public string getKey()
        {
            if (listBox1.SelectionMode == SelectionMode.MultiExtended)
            {
                string toreturn = "";
                foreach (string S in listBox1.SelectedItems)
                {
                    toreturn += S + "\\";
                }
                return toreturn.Substring(0, toreturn.Length - 1);
            }
            else
                return keyName;
        }
    }
}
