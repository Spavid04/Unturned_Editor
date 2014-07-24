using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Unturned_Editor
{
    public partial class clothesForm : Form
    {
        public string initial_backpack = "";

        public clothesForm(string key_value)
        {
            InitializeComponent();

            List<string> toadd = new List<string>();

            comboBox1.Items.Add("No Shirt");
            toadd = Items.getShirts();
            toadd.Sort();
            comboBox1.Items.AddRange(toadd.ToArray());

            comboBox2.Items.Add("No Pants");
            toadd = Items.getPants();
            toadd.Sort();
            comboBox2.Items.AddRange(toadd.ToArray());

            comboBox3.Items.Add("No Helmet");
            toadd = Items.getHelmets();
            toadd.Sort();
            comboBox3.Items.AddRange(toadd.ToArray());

            comboBox4.Items.Add("No Backpack");
            toadd = Items.getBackpacks();
            toadd.Sort();
            comboBox4.Items.AddRange(toadd.ToArray());

            comboBox5.Items.Add("No Armor");
            toadd = Items.getArmors();
            toadd.Sort();
            comboBox5.Items.AddRange(toadd.ToArray());

            string[] values = key_value.Split(new char[] {';'}, StringSplitOptions.RemoveEmptyEntries);

            if (values[0] == "-1")
                comboBox1.Text = "No Shirt";
            else
                comboBox1.Text = Items.getItem(Convert.ToInt32(values[0]));

            if (values[1] == "-1")
                comboBox2.Text = "No Pants";
            else
                comboBox2.Text = Items.getItem(Convert.ToInt32(values[1]));

            if (values[2] == "-1")
                comboBox3.Text = "No Helmet";
            else
                comboBox3.Text = Items.getItem(Convert.ToInt32(values[2]));

            if (values[3] == "-1")
                comboBox4.Text = "No Backpack";
            else
                comboBox4.Text = Items.getItem(Convert.ToInt32(values[3]));
            initial_backpack = comboBox4.Text;

            if (values[4] == "-1")
                comboBox5.Text = "No Armor";
            else
                comboBox5.Text = Items.getItem(Convert.ToInt32(values[4]));
        }

        public string getClothes()
        {
            string toreturn = "";

            if (initial_backpack != comboBox4.Text)
            {
                DialogResult result =
                    MessageBox.Show(
                        "I strongly advise you not to change the backpack.\nIt may cause bugs and you may be required to reset your inventory!\nDo you still want to change it?",
                        "Warning!", MessageBoxButtons.YesNo);

                if (result == DialogResult.No)
                    comboBox4.Text = initial_backpack;
            }

            if (comboBox1.Text == "No Shirt")
                toreturn += "-1;";
            else
                toreturn += Items.getID(comboBox1.Text).ToString() + ";";

            if (comboBox2.Text == "No Pants")
                toreturn += "-1;";
            else
                toreturn += Items.getID(comboBox2.Text).ToString() + ";";

            if (comboBox3.Text == "No Helmet")
                toreturn += "-1;";
            else
                toreturn += Items.getID(comboBox3.Text).ToString() + ";";

            if (comboBox4.Text == "No Backpack")
                toreturn += "-1;";
            else
                toreturn += Items.getID(comboBox4.Text).ToString() + ";";

            if (comboBox5.Text == "No Armor")
                toreturn += "-1;";
            else
                toreturn += Items.getID(comboBox5.Text).ToString() + ";";

            return toreturn;
        }
    }
}
