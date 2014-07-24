using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Unturned_Editor
{
    public partial class newMainForm : Form
    {
        #region Global variables

        public List<string> KEYS = new List<string>(); //global key value list
        public List<string> ITEMS = new List<string>(); //global item list
        public string current_inventory = ""; //global inventory string
        public string current_key = ""; //global current key string
        public int[] size = new int[] {1, 4}; //global inventory size
        public int x, y; //global selected item slot coordinates
        public PictureBox[,] slots = new PictureBox[5, 5]; //global picturebox matrix
        public Item[,] items = new Item[5, 5]; //global item map
        public bool nochange = false; //global boolean flag 
        public float max_weight = 0; //global backpack maximum weight
        public int[] forcedSize = new int[3] {-1, -1, -1}; //global forced backpack size value

        #endregion

        #region [OLD] Global separators and markers

        /*
        public byte[] master_separator = {0xE2, 0x80, 0xBB}; //global master separator
        public byte[] normal_separator = {0xE2, 0x80, 0xBA}; //global separator
        public byte[] attachment_separator = {0xE2, 0x81, 0x9F}; //global attachment separator
        public byte[] null_marker = {0xE2, 0x80, 0xB1, 0xE2, 0x80, 0xAD}; //global null-attachment marker
        public byte[] filler = {0xE2, 0x80}; //globall filler string (that AE thing) (â€)

        public byte[] no_item =
        {
            0xE2, 0x80, 0xBA, 0xE2, 0x80, 0xBA, 0xE2, 0x80, 0xB0, 0xE2, 0x80, 0xBA, 0xE2, 0x80, 0xB1,
            0xE2, 0x80, 0xAD
        }; //global no-item marker
        */

        #endregion

        public newMainForm()
        {
            InitializeComponent();

            #region Initialize the item map

            for (int a = 0; a < 5; a++)
                for (int b = 0; b < 5; b++)
                    items[a, b] = new Item();

            #endregion

            #region Read all items and store them into the list

            ITEMS =
                new List<string>(Properties.Resources.Items.Split(new string[] {"\n", "\r\n"},
                    StringSplitOptions.RemoveEmptyEntries));
            ITEMS.Sort();

            #endregion

            #region Populate the list with all the items (and add a "no item")

            listBox1.Items.Add("<empty>");
            foreach (string S in ITEMS)
            {
                listBox1.Items.Add(S);
            }

            #endregion

            #region Read and store all the keys under HKEY_CURRENT_USER/Software/Smartly Dressed Games/Unturned (where the game saves everything)

            KEYS =
                new List<string>(
                    Registry.CurrentUser.OpenSubKey("Software\\Smartly Dressed Games\\Unturned").GetValueNames());
            KEYS.Sort();

            #endregion

            #region Store all picture boxes (slots) in a matrix

            int i = 4;
            int j = 4;
            foreach (Control C in tableLayoutPanel2.Controls)
            {
                if (C.GetType() != (new PictureBox()).GetType()) continue;
                if (j < 0)
                {
                    j = 4;
                    i--;
                }
                slots[i, j] = (PictureBox) C;
                j--;
            }

            #endregion

            #region Events

            foreach (Control C in tableLayoutPanel2.Controls)
            {
                C.Click += C_Click;
            }

            listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
            textBox1.TextChanged += textBox1_TextChanged;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;

            #endregion
        }

        #region Text to bitmap converter (due to lack of icons)

        private static Image DrawText(String text, Font font, Color textColor, Color backColor)
        {
            Image img = new Bitmap(1, 1);
            Graphics drawing = Graphics.FromImage(img);

            SizeF textSize = drawing.MeasureString(text, font);

            img.Dispose();
            drawing.Dispose();

            img = new Bitmap(175, 175);

            drawing = Graphics.FromImage(img);

            drawing.Clear(backColor);

            Brush textBrush = new SolidBrush(textColor);

            drawing.DrawString(text, font, textBrush, (int) (175.0/2 - textSize.Width/2),
                (int) (175.0/2 - textSize.Height/2));

            drawing.Save();

            textBrush.Dispose();
            drawing.Dispose();

            return img;
        }

        #endregion

        #region Event that occurs when an item slot is clicked

        private void C_Click(object sender, EventArgs e)
        {
            if (sender == null) return;

            int tempx, tempy;
            try
            {
                tempx = ((PictureBox) sender).TabIndex/5;
                tempy = ((PictureBox) sender).TabIndex%5 - 1;
            }
            catch (Exception)
            {
                return;
            }
            if (tempy == -1)
            {
                tempx--;
                tempy = 4;
            }
            if (slots[tempx, tempy].BackColor == Color.Gray) return;

            nochange = true;
            button1.Enabled = false;
            numericUpDown1.Enabled = false;
            numericUpDown1.Value = 1;
            checkBox1.Enabled = false;
            checkBox1.Checked = false;

            slots[x, y].BackColor = Color.Gainsboro;

            x = tempx;
            y = tempy;
            slots[x, y].BackColor = Color.Khaki;

            if (items[x, y].count == 0)
                numericUpDown1.Value = 1;
            else
                numericUpDown1.Value = items[x, y].count;

            if (items[x, y].hasAttachments) button1.Enabled = true;
            else if (items[x, y].isToggleableOrFillable) checkBox1.Enabled = true;
            else numericUpDown1.Enabled = true;

            if (checkBox1.Enabled == true)
            {
                if (items[x, y].attachments[6] == 1)
                    checkBox1.Checked = true;
                else
                    checkBox1.Checked = false;
            }

            textBox1.Text += " ";
            textBox1.Clear();

            if (ITEMS.IndexOf(items[x, y].name) == -1)
            {
                if (items[x, y].name == "<empty>")
                    listBox1.SelectedIndex = 0;
            }
            else
            {
                listBox1.SelectedIndex = ITEMS.IndexOf(items[x, y].name) + 1;
            }

            nochange = false;
        }

        #endregion

        #region Change the item to the selected one in the listbox

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (nochange) return;

            if (saveKeyToolStripMenuItem.Enabled == false) return;

            button1.Enabled = false;
            numericUpDown1.Enabled = true;
            checkBox1.Enabled = false;
            checkBox1.Checked = false;
            Item temp = new Item();

            try
            {
                temp.name = listBox1.SelectedItem.ToString();
            }
            catch (Exception)
            {
                return;
            }
            temp.ID = Items.getID(temp.name);
            temp.count = 1;
            numericUpDown1.Value = 1;

            if (Items.isWeapon(temp.name))
            {
                temp.hasAttachments = true;
                temp.isToggleableOrFillable = false;
                button1.Enabled = true;
                numericUpDown1.Enabled = false;
                checkBox1.Enabled = false;
            }
            else if (Items.isToggleable(temp.name) || Items.isFillable(temp.name))
            {
                temp.hasAttachments = false;
                temp.isToggleableOrFillable = true;
                button1.Enabled = false;
                numericUpDown1.Enabled = false;
                checkBox1.Enabled = true;
                checkBox1.Checked = true;
                temp.attachments[6] = 1;
            }

            items[x, y] = temp;
            redraw();
        }

        #endregion

        #region Change item count

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (nochange) return;
            items[x, y].count = (int) numericUpDown1.Value;
            redraw();
        }

        #endregion

        #region When the TextBox's text changes, filter the items

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            bool empty = false;
            listBox1.Items.Clear();
            if (textBox1.Text == "") empty = true;
            listBox1.Items.Add("<empty>");
            foreach (string S in ITEMS)
            {
                if (S.ToLower().Contains(textBox1.Text.ToLower()) || empty)
                {
                    listBox1.Items.Add(S);
                }
            }
        }

        #endregion

        #region Open an inventory and prepare it for editing

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            x = y = 0;
            textBox1.Text += " ";
            textBox1.Clear();

            #region Open one of the possible keys

            List<string> temp = new List<string>();
            foreach (string S in KEYS)
            {
                if (S.StartsWith("inventory"))
                {
                    temp.Add(S);
                }
            }
            openForm openFrm = new openForm(temp.ToArray(), false);
            openFrm.ShowDialog();

            string key_name = openFrm.getKey();
            if (key_name == "") return;

            saveKeyToolStripMenuItem.Enabled = true;

            #endregion

            #region Read the key and store it

            current_key = key_name;
            current_inventory = Registry.CurrentUser.OpenSubKey("Software\\Smartly Dressed Games\\Unturned\\")
                .GetValue(key_name)
                .ToString();

            #endregion

            #region Cut out useless parts from the inventory string

            current_inventory = current_inventory.Substring(4, current_inventory.Length - 4);
            current_inventory = current_inventory.Substring(0, current_inventory.Length - 1);

            #endregion

            #region DeObfuscate the inventory (my god it looks so satisfying)

            current_inventory = deObfuscate(current_inventory);

            #endregion

            #region OLD ALGORITHM

            /*
            #region Read the content of the selected key and split it into "readable" segments

            current_key = key_name;
            current_inventory =
                Registry.CurrentUser.OpenSubKey("Software\\Smartly Dressed Games\\Unturned\\")
                    .GetValue(key_name)
                    .ToString();

            current_inventory = current_inventory.Substring(4, current_inventory.Length - 4);
            current_inventory = current_inventory.Substring(0, current_inventory.Length - 7);

            string[] relevantBytes =
                current_inventory.Split(new string[] {System.Text.Encoding.Default.GetString(master_separator)},
                    StringSplitOptions.RemoveEmptyEntries);

            #endregion

            #region Process all items

            List<Item> processedItems = new List<Item>();
            for (int i = 0; i < relevantBytes.Length - 2; i++)
            {
                Item tempItem = new Item();

                #region Check if the slot is occupied by an item

                if (Encoding.Default.GetBytes(relevantBytes[i]).SequenceEqual(no_item))
                {
                    tempItem.name = "<empty>";
                    processedItems.Add(tempItem);
                    continue;
                }

                #endregion

                #region If the item has attachments, process them

                if (relevantBytes[i].Contains(System.Text.Encoding.Default.GetString(attachment_separator)))
                {
                    tempItem.hasAttachments = true;
                    tempItem.count = 1;

                    string[] contents =
                        relevantBytes[i].Split(
                            new string[] {System.Text.Encoding.Default.GetString(attachment_separator)},
                            StringSplitOptions.RemoveEmptyEntries);

                    //We do not need to process the first and second part.

                    #region Firing mode

                    tempItem.attachments[0] = contents[2][2] - 0xB0;

                    #endregion

                    #region Sight

                    if (!Encoding.Default.GetBytes(contents[3]).SequenceEqual(null_marker))
                    {
                        string workingstring = "";

                        for (int j = 2; j < contents[3].Length; j += 3)
                        {
                            workingstring += (contents[3][j] - 0xB0).ToString();
                        }

                        tempItem.attachments[1] =
                            Convert.ToInt32(new string(workingstring.ToCharArray().Reverse().ToArray()));
                    }

                    #endregion

                    #region Barrel

                    if (!Encoding.Default.GetBytes(contents[4]).SequenceEqual(null_marker))
                    {
                        string workingstring = "";

                        for (int j = 2; j < contents[4].Length; j += 3)
                        {
                            workingstring += (contents[4][j] - 0xB0).ToString();
                        }

                        tempItem.attachments[2] =
                            Convert.ToInt32(new string(workingstring.ToCharArray().Reverse().ToArray()));
                    }

                    #endregion

                    #region Tactical

                    if (!Encoding.Default.GetBytes(contents[5]).SequenceEqual(null_marker))
                    {
                        string workingstring = "";

                        for (int j = 2; j < contents[5].Length; j += 3)
                        {
                            workingstring += (contents[5][j] - 0xB0).ToString();
                        }

                        tempItem.attachments[3] =
                            Convert.ToInt32(new string(workingstring.ToCharArray().Reverse().ToArray()));
                    }

                    #endregion

                    #region Type of magazine/ammo

                    if (!Encoding.Default.GetBytes(contents[6]).SequenceEqual(null_marker))
                    {
                        string workingstring = "";

                        for (int j = 2; j < contents[6].Length; j += 3)
                        {
                            workingstring += (contents[6][j] - 0xB0).ToString();
                        }

                        tempItem.attachments[4] =
                            Convert.ToInt32(new string(workingstring.ToCharArray().Reverse().ToArray()));
                    }

                    #endregion

                    #region Type of weapon and ammo number

                    string[] weapon =
                        contents[7].Split(new string[] {System.Text.Encoding.Default.GetString(normal_separator)},
                            StringSplitOptions.RemoveEmptyEntries);

                    string auxstring = "";

                    for (int j = 2; j < weapon[0].Length; j += 3)
                    {
                        auxstring += (weapon[0][j] - 0xB0).ToString();
                    }

                    tempItem.attachments[5] = Convert.ToInt32(new string(auxstring.ToCharArray().Reverse().ToArray()));

                    auxstring = "";

                    for (int j = 2; j < weapon[2].Length; j += 3)
                    {
                        auxstring += (weapon[2][j] - 0xB0).ToString();
                    }

                    tempItem.ID = Convert.ToInt32(new string(auxstring.ToCharArray().Reverse().ToArray()));
                    tempItem.name = Items.getItem(tempItem.ID);

                    #endregion
                }
                    #endregion
                    #region else the item has no attachments, so treat it as a normal item or a magazine item

                else
                {
                    string[] contents =
                        relevantBytes[i].Split(new string[] {System.Text.Encoding.Default.GetString(normal_separator)},
                            StringSplitOptions.RemoveEmptyEntries);

                    string workingstring = "";

                    for (int j = 2; j < contents[0].Length; j += 3)
                    {
                        workingstring += (contents[0][j] - 0xB0).ToString();
                    }

                    tempItem.count = Convert.ToInt32(new string(workingstring.ToCharArray().Reverse().ToArray()));

                    workingstring = "";

                    for (int j = 2; j < contents[1].Length; j += 3)
                    {
                        workingstring += (contents[1][j] - 0xB0).ToString();
                    }

                    tempItem.ID = Convert.ToInt32(new string(workingstring.ToCharArray().Reverse().ToArray()));
                    tempItem.name = Items.getItem(tempItem.ID);
                }

                #endregion

                processedItems.Add(tempItem);
            }

            #endregion

            #region Get backpack type

            switch (relevantBytes.Length - 2)
            {
                case 4: //no backpack
                    size[0] = 1;
                    size[1] = 4;
                    label1.Text = "0.00 / 5.00 kg        Current backpack: None";
                    max_weight = 5.0f;
                    break;
                case 8: //napsack
                    size[0] = 2;
                    size[1] = 4;
                    label1.Text = "0.00 / 7.50 kg        Current backpack: Napsack";
                    max_weight = 7.5f;
                    break;
                case 10: //animalpack
                    size[0] = 2;
                    size[1] = 5;
                    label1.Text = "0.00 / 10.00 kg        Current backpack: Animalpack";
                    max_weight = 10.0f;
                    break;
                case 12: //schoolbag
                    size[0] = 3;
                    size[1] = 4;
                    label1.Text = "0.00 / 10.00 kg        Current backpack: Schoolbag";
                    max_weight = 10.0f;
                    break;
                case 16: //travelpack
                    size[0] = 4;
                    size[1] = 4;
                    label1.Text = "0.00 / 12.50 kg        Current backpack: Travelpack";
                    max_weight = 12.5f;
                    break;
                case 20: //coyotepack or alice pack
                    if (relevantBytes[relevantBytes.Length - 2][relevantBytes[relevantBytes.Length - 2].Length - 1] ==
                        0xB1)
                    {
                        size[0] = 4;
                        size[1] = 5;
                        label1.Text = "0.00 / 15.00 kg        Current backpack: Coyotepack";
                        max_weight = 15.0f;
                    }
                    else
                    {
                        size[0] = 5;
                        size[1] = 4;
                        label1.Text = "0.00 / 20.00 kg        Current backpack: Alice pack";
                        max_weight = 20.0f;
                    }
                    break;
                case 25: //rucksack
                    size[0] = 5;
                    size[1] = 5;
                    label1.Text = "0.00 / 17.50 kg        Current backpack: Rucksack";
                    max_weight = 17.5f;
                    break;
            }

            #endregion

            #region Assign items to slots

            processedItems.Reverse();
            int index = 0;
            for (int i = 0; i < size[0]; i++)
                for (int j = 0; j < size[1]; j++)
                {
                    items[i, j] = processedItems[index];
                    index++;
                }

            #endregion
            */

            #endregion

            List<Item> processedItems = new List<Item>();

            string[] contents = current_inventory.Split(new char[] {';'}, StringSplitOptions.RemoveEmptyEntries);

            #region Get inventory specifications

            size[0] = Convert.ToInt32(contents[0]);
            size[1] = Convert.ToInt32(contents[1]);

            if (size[0] > 5 || size[1] > 5)
            {
                MessageBox.Show(
                    "The selected inventory cannot be edited by this program.\nBackpack size is too big: " +
                    size[0].ToString() + "x" + size[1].ToString() +
                    "\nYou can, however, edit it in notepad and then encrypt it on-demand (tools).", "Error");
                return;
            }

            max_weight = (float) Convert.ToDouble(contents[2]);
            max_weight = max_weight/1000f;

            #endregion

            #region Get type of backpack

            switch (size[0]*size[1])
            {
                case 4: //no backpack
                    label1.Text = "0.00 / 5.00 kg        Current backpack: None";
                    max_weight = 5.0f;
                    break;
                case 8: //napsack
                    label1.Text = "0.00 / 7.50 kg        Current backpack: Napsack";
                    max_weight = 7.5f;
                    break;
                case 10: //animalpack
                    label1.Text = "0.00 / 10.00 kg        Current backpack: Animalpack";
                    max_weight = 10.0f;
                    break;
                case 12: //schoolbag
                    label1.Text = "0.00 / 10.00 kg        Current backpack: Schoolbag";
                    max_weight = 10.0f;
                    break;
                case 16: //travelpack
                    label1.Text = "0.00 / 12.50 kg        Current backpack: Travelpack";
                    max_weight = 12.5f;
                    break;
                case 20: //coyotepack or alice pack
                    if (max_weight == 15f)
                    {
                        label1.Text = "0.00 / 15.00 kg        Current backpack: Coyotepack";
                    }
                    else
                    {
                        label1.Text = "0.00 / 20.00 kg        Current backpack: Alice pack";
                    }
                    break;
                case 25: //rucksack
                    label1.Text = "0.00 / 17.50 kg        Current backpack: Rucksack";
                    break;
                default:
                    label1.Text = "0.00 / " + Math.Round(max_weight, 2).ToString() +
                                  " kg        Current backpack: <unknown>";
                    break;
            }

            #endregion

            #region Process all items

            for (int index = 3; index < contents.Length; index++)
            {
                string S = contents[index];
                Item tempItem = new Item();

                string[] values = S.Split(new char[] {':'}, StringSplitOptions.RemoveEmptyEntries);

                #region Normal items and magazines AND toggleable clothings (NV goggles, miner helmet) + no-items

                if (values.Length == 2)
                {
                    tempItem.ID = Convert.ToInt32(values[0]);
                    tempItem.name = Items.getItem(tempItem.ID);
                    tempItem.hasAttachments = false;
                    tempItem.isToggleableOrFillable = false;
                    tempItem.weight = Items.getWeight(tempItem.ID);
                    tempItem.count = Convert.ToInt32(values[1]);
                }
                    #endregion
                    #region Toggleable items, fillable items and weapons

                else
                {
                    tempItem.ID = Convert.ToInt32(values[0]);
                    tempItem.name = Items.getItem(tempItem.ID);
                    tempItem.weight = Items.getWeight(tempItem.ID);
                    tempItem.count = Convert.ToInt32(values[1]);

                    #region fillable/toggleable item

                    if ("febd".Contains(values[2]))
                    {
                        tempItem.isToggleableOrFillable = true;
                        tempItem.hasAttachments = false;

                        if (values[2] == "f" || values[2] == "b")
                            //item is filled/on
                            tempItem.attachments[6] = 1;
                        else
                            //item is empty/off
                            tempItem.attachments[6] = 0;
                    }
                        #endregion
                        #region weapon

                    else
                    {
                        tempItem.isToggleableOrFillable = false;
                        tempItem.hasAttachments = true;

                        string[] attachments = values[2].Split(new char[] {'_'}, StringSplitOptions.RemoveEmptyEntries);

                        tempItem.attachments[5] = Convert.ToInt32(attachments[0]); //bullets
                        tempItem.attachments[4] = Convert.ToInt32(attachments[1]); //magazine type
                        tempItem.attachments[1] = Convert.ToInt32(attachments[2]); //tactical ID
                        tempItem.attachments[2] = Convert.ToInt32(attachments[3]); //barrel ID
                        tempItem.attachments[3] = Convert.ToInt32(attachments[4]); //sight ID
                        tempItem.attachments[0] = Convert.ToInt32(attachments[5]); //mode (0/1/2)
                        if (attachments[6] == "y")
                            tempItem.attachments[6] = 1; //tac light/laser on
                        else
                            tempItem.attachments[6] = 0; //tac light/laser off
                    }

                    #endregion
                }

                #endregion

                processedItems.Add(tempItem);
            }

            #endregion

            #region Assign all items to inventory slots

            int counter = 0;
            for (int i = 0; i < size[1]; i++)
                for (int j = 0; j < size[0]; j++)
                {
                    items[i, j] = processedItems[counter];
                    counter++;
                }

            #endregion

            #region Redraw pictureboxes and enable saving

            redraw();

            saveKeyToolStripMenuItem.Enabled = true;

            #endregion
        }

        #endregion

        #region Open the attachment editor and save changes

        private void button1_Click(object sender, EventArgs e)
        {
            attachForm form = new attachForm(items[x, y].name, items[x, y].attachments);
            form.ShowDialog();

            items[x, y].attachments = form.getAttachments();
        }

        #endregion

        #region Change the ON/OFF or FULL/EMPTY state

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (nochange) return;

            if (checkBox1.Checked)
                items[x, y].attachments[6] = 1;
            else
                items[x, y].attachments[6] = 0;

            redraw();
        }

        #endregion

        #region Generate the inventory string and save the inventory to the current key

        private void saveKeyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure?", "", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                #region OLD ALGORITHM

                /*
                current_inventory = "0_8_" + Encoding.Default.GetString(master_separator);
                string working_string = "";

                for (int i = size[0] - 1; i >= 0; i--)
                    for (int j = size[1] - 1; j >= 0; j--)
                    {
                        working_string = "";
                        if (!items[i, j].hasAttachments)
                        {
                            if (items[i, j].name == "<empty>")
                            {
                                working_string = Encoding.Default.GetString(no_item);
                            }
                            else
                            {
                                working_string += Encoding.Default.GetString(normal_separator);
                                working_string += Encoding.Default.GetString(normal_separator);
                                string aux_working_string = "";
                                for (int k = items[i, j].count.ToString().Length - 1; k >= 0; k--)
                                {
                                    aux_working_string += Encoding.Default.GetString(filler) +
                                                          (char)(items[i, j].count.ToString()[k] - 48 + 0xB0);
                                }
                                working_string += aux_working_string + Encoding.Default.GetString(normal_separator);
                                aux_working_string = "";
                                for (int k = items[i, j].ID.ToString().Length - 1; k >= 0; k--)
                                {
                                    aux_working_string += Encoding.Default.GetString(filler) +
                                                          (char)(items[i, j].ID.ToString()[k] - 48 + 0xB0);
                                }
                                working_string += aux_working_string;
                            }
                        }
                        else
                        {
                            working_string += Encoding.Default.GetString(normal_separator);
                            working_string +=
                                Encoding.Default.GetString(new byte[] { 0xE2, 0x81, 0x9F, 0xE2, 0x81, 0xB9, 0xE2, 0x81, 0x9F });
                            working_string += Encoding.Default.GetString(filler);
                            working_string += (char)(items[i, j].attachments[0] + 0xB0);
                            working_string += Encoding.Default.GetString(attachment_separator);

                            for (int W = 1; W <= 4; W++)
                            {
                                if (items[i, j].attachments[W] != -1)
                                {
                                    string aux_working_string = "";
                                    for (int k = items[i, j].attachments[W].ToString().Length - 1; k >= 0; k--)
                                    {
                                        aux_working_string += Encoding.Default.GetString(filler) +
                                                              (char)
                                                                  (items[i, j].attachments[W].ToString()[k] - 48 + 0xB0);
                                    }
                                    working_string += aux_working_string;
                                }
                                else
                                {
                                    working_string += Encoding.Default.GetString(null_marker);
                                }
                                working_string += Encoding.Default.GetString(attachment_separator);
                            }

                            string auxx_working_string = "";
                            for (int k = items[i, j].attachments[5].ToString().Length - 1; k >= 0; k--)
                            {
                                auxx_working_string += Encoding.Default.GetString(filler) +
                                                       (char)(items[i, j].attachments[5].ToString()[k] - 48 + 0xB0);
                            }
                            working_string += auxx_working_string + Encoding.Default.GetString(normal_separator) +
                                              Encoding.Default.GetString(new byte[] { 0xE2, 0x80, 0xB1 }) +
                                              Encoding.Default.GetString(normal_separator);

                            auxx_working_string = "";
                            for (int k = items[i, j].ID.ToString().Length - 1; k >= 0; k--)
                            {
                                auxx_working_string += Encoding.Default.GetString(filler) +
                                                       (char)(items[i, j].ID.ToString()[k] - 48 + 0xB0);
                            }
                            working_string += auxx_working_string;
                        }
                        current_inventory += working_string;
                        current_inventory += Encoding.Default.GetString(master_separator);
                    }

                working_string = "";

                working_string = ((int)(max_weight * 1000)).ToString();
                working_string = new string((working_string.ToCharArray().Reverse()).ToArray());
                foreach (char C in working_string)
                {
                    current_inventory += Encoding.Default.GetString(filler) + (char)(C - 48 + 0xB0);
                }
                current_inventory += Encoding.Default.GetString(master_separator) +
                                     Encoding.Default.GetString(new byte[] { 0xE2, 0x80 }) +
                                     (char)(size[0] + 0xB0) +
                                     Encoding.Default.GetString(master_separator) +
                                     Encoding.Default.GetString(new byte[] { 0xE2, 0x80 }) +
                                     (char)(size[1] + 0xB0) +
                                     (char)(0x5F);

                RegistryKey finalKey =
                    Registry.CurrentUser.OpenSubKey("Software\\Smartly Dressed Games\\Unturned\\", true);
                finalKey.SetValue(current_key, current_inventory); 
                */

                #endregion

                string finalString = "";

                finalString = size[0].ToString() + ";" + size[1].ToString() + ";" +
                              ((int) (max_weight*1000)).ToString() + ";";

                for (int i = 0; i < size[1]; i++)
                    for (int j = 0; j < size[0]; j++)
                    {
                        string[] to_append = new string[] {"", "", ""};

                        to_append[0] = items[i, j].ID.ToString();
                        to_append[1] = items[i, j].count.ToString();

                        if (items[i, j].isToggleableOrFillable)
                        {
                            if (Items.isFillable(items[i, j].name))
                            {
                                if (items[i, j].attachments[6] == 1)
                                    to_append[2] = "f";
                                else
                                    to_append[2] = "e";
                            }
                            else if (Items.isToggleable(items[i, j].name))
                            {
                                if (items[i, j].attachments[6] == 1)
                                    to_append[2] = "b";
                                else
                                    to_append[2] = "d";
                            }
                        }
                        else if (items[i, j].hasAttachments || Items.isWeapon(items[i, j].name))
                        {
                            to_append[2] += items[i, j].attachments[5].ToString() + "_"; //bullets
                            to_append[2] += items[i, j].attachments[4].ToString() + "_"; //ammo ID
                            to_append[2] += items[i, j].attachments[1].ToString() + "_"; //tactical ID
                            to_append[2] += items[i, j].attachments[2].ToString() + "_"; //barrel ID
                            to_append[2] += items[i, j].attachments[3].ToString() + "_"; //sight ID
                            to_append[2] += items[i, j].attachments[0].ToString() + "_"; //mode
                            if (items[i, j].attachments[6] == 1) // tac light/laser ON/OFF
                                to_append[2] += "y_";
                            else
                                to_append[2] += "n_";
                        }

                        foreach (string S in to_append)
                            finalString += S + ":";
                        finalString += ";";
                    }

                #region Set to forced size

                if (forcedSize[0] != -1 || forcedSize[1] != -1 || forcedSize[2] != -1)
                {
                    int index = finalString.IndexOf(';', finalString.IndexOf(';', finalString.IndexOf(';') + 1) + 1);
                    string toreplace = "";

                    if (forcedSize[0] != -1 || forcedSize[1] != -1)
                    {
                        if (forcedSize[0] != -1)
                        {
                            toreplace += forcedSize[0].ToString() + ";";
                        }
                        else
                        {
                            toreplace += size[0].ToString() + ";";
                        }

                        if (forcedSize[1] != -1)
                        {
                            toreplace += forcedSize[1].ToString() + ";";
                        }
                        else
                        {
                            toreplace += size[1].ToString() + ";";
                        }
                    }
                    else
                    {
                        toreplace += size[0].ToString() + ";" + size[1].ToString() + ";";
                    }

                    if (forcedSize[2] != -1)
                    {
                        toreplace += forcedSize[2].ToString() + ";";
                    }
                    else
                    {
                        toreplace += ((int) max_weight*1000).ToString() + ";";
                    }

                    finalString = toreplace + finalString.Substring(index);

                    int itemCount = finalString.Split(new char[] {';'}, StringSplitOptions.RemoveEmptyEntries).Length -
                                    3;

                    if (forcedSize[0] == -1) forcedSize[0] = size[0];
                    if (forcedSize[1] == -1) forcedSize[1] = size[1];

                    for (int i = itemCount; i < forcedSize[0]*forcedSize[1]; i++)
                    {
                        finalString += "-1:0::;";
                    }

                }

                #endregion

                current_inventory = reObfuscate(finalString);
                current_inventory = "0_8_" + current_inventory + "_";

                RegistryKey finalKey =
                    Registry.CurrentUser.OpenSubKey("Software\\Smartly Dressed Games\\Unturned\\", true);
                finalKey.SetValue(current_key, current_inventory);

                MessageBox.Show("Success!");
            }
        }

        #endregion

        #region Redraw all pictureboxes

        public void redraw()
        {
            foreach (PictureBox P in slots)
            {
                P.Image = null;
                P.BackColor = Color.Gray;
            }
            for (int i = 0; i < size[1]; i++)
                for (int j = 0; j < size[0]; j++)
                {
                    slots[i, j].BackColor = Color.Gainsboro;
                    if (items[i, j].name != "<empty>")
                    {
                        string todraw = items[i, j].name;

                        if (items[i, j].isToggleableOrFillable == false)
                            todraw += "\n\n    x" + items[i, j].count.ToString();
                        else
                        {
                            if (items[i, j].attachments[6] == 1) todraw += "\n\n    ↑";
                            else todraw += "\n\n    ↓";
                        }

                        slots[i, j].Image = DrawText(todraw,
                            new Font("Arial", 25.0f), Color.DarkRed,
                            Color.Transparent);

                        slots[i, j].SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                }
            slots[x, y].BackColor = Color.Khaki;
        }

        #endregion

        #region This function transforms encrypted strings into more readable strings, using a totally different approach

        public string deObfuscate(string input)
        {
            return Encryption.decrypt(input);
        }

        #endregion

        #region ReEncrypt the string

        public string reObfuscate(string input)
        {
            return Encryption.encrypt(input);
        }

        #endregion


        //Tools:


        #region Old Tools

        /*
        #region Give 10,000 skill points (and you earn 2 achievements!)

        private void allSkillsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("This will replace all skills from ALL saves!", "Are you sure?",
                MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                byte[] allskills =
                {
                    0x30, 0x5F, 0x30, 0x5F, 0xE2, 0x80, 0xBB, 0xE2, 0x80, 0xB0, 0xE2, 0x80, 0xBB, 0xE2, 0x80, 0xB0, 0xE2,
                    0x80, 0xBB, 0xE2, 0x80, 0xB0, 0xE2, 0x80, 0xBB, 0xE2, 0x80, 0xB0, 0xE2, 0x80, 0xBB, 0xE2, 0x80, 0xB0,
                    0xE2, 0x80, 0xBB, 0xE2, 0x80, 0xB0, 0xE2, 0x80, 0xBB, 0xE2, 0x80, 0xB0, 0xE2, 0x80, 0xBB, 0xE2, 0x80,
                    0xB0, 0xE2, 0x80, 0xBB, 0xE2, 0x80, 0xB0, 0xE2, 0x80, 0xB0, 0xE2, 0x80, 0xB0, 0xE2, 0x80, 0xB0, 0xE2,
                    0x80, 0xB1, 0x5F
                };

                foreach (string S in KEYS)
                {
                    if (S.StartsWith("skills"))
                    {
                        RegistryKey skillKey =
                            Registry.CurrentUser.OpenSubKey("Software\\Smartly Dressed Games\\Unturned\\", true);
                        //Registry.CurrentUser.OpenSubKey("Software\\Smartly Dressed Games\\Unturned\\").SetValue(S, GetString(allskills));
                        skillKey.SetValue(S, System.Text.Encoding.Default.GetString(allskills));
                    }
                }

                MessageBox.Show("Success!");
            }
        }

        #endregion

        #region Open the backup tool

        private void backupToolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            backupForm form = new backupForm();
            form.ShowDialog();
        }

        #endregion

        #region Reset position of the character

        private void resetPositionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("This will reset your position from the chosen saves!",
                "Are you sure?", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                List<string> topass = new List<string>();
                foreach (string S in KEYS)
                {
                    if (S.StartsWith("position"))
                    {
                        topass.Add(S);
                    }
                }

                openForm form = new openForm(topass.ToArray(), true);
                form.ShowDialog();
                string[] reset = form.getKey().Split('\\');
                foreach (string S in reset)
                {
                    RegistryKey skillKey =
                        Registry.CurrentUser.OpenSubKey("Software\\Smartly Dressed Games\\Unturned\\", true);
                    //Registry.CurrentUser.OpenSubKey("Software\\Smartly Dressed Games\\Unturned\\").SetValue(S, GetString(allskills));
                    skillKey.DeleteValue(S);
                }

                MessageBox.Show("Success!");
            }
        }

        #endregion

        #region Heal and feed the character

        private void fullyRestorehealToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("This will completely heal and feed your character from ALL saves!",
                "Are you sure?", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                foreach (string S in KEYS)
                {
                    if (S.StartsWith("life"))
                    {
                        RegistryKey skillKey =
                            Registry.CurrentUser.OpenSubKey("Software\\Smartly Dressed Games\\Unturned\\", true);
                        //Registry.CurrentUser.OpenSubKey("Software\\Smartly Dressed Games\\Unturned\\").SetValue(S, GetString(allskills));
                        skillKey.DeleteValue(S);
                    }
                }

                MessageBox.Show("Success!");
            }
        }

        #endregion

        #region Reset the chosen inventories

        private void resetInventoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result =
                MessageBox.Show(
                    "This will reset your inventory from the chosen saves!\nYou should use it in case of save corruption.",
                    "Are you sure?", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                List<string> topass = new List<string>();
                foreach (string S in KEYS)
                {
                    if (S.StartsWith("inventory"))
                    {
                        topass.Add(S);
                    }
                }

                openForm form = new openForm(topass.ToArray(), true);
                form.ShowDialog();
                string[] reset = form.getKey().Split('\\');
                foreach (string S in reset)
                {
                    RegistryKey skillKey =
                        Registry.CurrentUser.OpenSubKey("Software\\Smartly Dressed Games\\Unturned\\", true);
                    skillKey.DeleteValue(S);
                }

                MessageBox.Show("Success!");
            }
        }

        #endregion
        */

        #endregion

        #region Open the backup tool

        private void backupToolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            backupForm form = new backupForm();
            form.ShowDialog();
        }

        #endregion

        #region Skill editor

        private void skillsEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<string> temp = new List<string>();
            foreach (string S in KEYS)
            {
                if (S.StartsWith("skills"))
                {
                    temp.Add(S);
                }
            }
            openForm openFrm = new openForm(temp.ToArray(), false);
            openFrm.ShowDialog();

            string key_name = openFrm.getKey();
            if (key_name == "") return;

            string key_value = Registry.CurrentUser.OpenSubKey("Software\\Smartly Dressed Games\\Unturned\\")
                .GetValue(key_name)
                .ToString();

            key_value = key_value.Substring(0, key_value.Length - 1);
            key_value = key_value.Substring(4, key_value.Length - 4);

            skillForm form = new skillForm(Encryption.decrypt(key_value));
            form.ShowDialog();

            key_value = form.getSkills();
            key_value = Encryption.encrypt(key_value);
            key_value = "0_0_" + key_value + "_";

            RegistryKey skillKey =
                Registry.CurrentUser.OpenSubKey("Software\\Smartly Dressed Games\\Unturned\\", true);
            skillKey.SetValue(key_name, key_value);

            MessageBox.Show("Success!");
        }

        #endregion

        #region Inventory resetter

        private void resetInventoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result =
                MessageBox.Show(
                    "This will reset the keys you select!\nYou should use it (only) in case of corruption!",
                    "Are you sure?", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                List<string> topass = new List<string>();
                foreach (string S in KEYS)
                {
                    topass.Add(S);
                }

                openForm form = new openForm(topass.ToArray(), true);
                form.ShowDialog();

                result = MessageBox.Show("You will lose all the data stored in those keys.", "Are you absolutely sure?",
                    MessageBoxButtons.YesNo);
                if (result == DialogResult.No)
                    return;

                string[] reset = form.getKey().Split('\\');
                foreach (string S in reset)
                {
                    RegistryKey skillKey =
                        Registry.CurrentUser.OpenSubKey("Software\\Smartly Dressed Games\\Unturned\\", true);
                    skillKey.DeleteValue(S);
                }

                MessageBox.Show("Success!");
            }
        }

        #endregion

        #region Health / hunger / thirst / disease / effects editor

        private void lifeEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<string> temp = new List<string>();
            foreach (string S in KEYS)
            {
                if (S.StartsWith("life"))
                {
                    temp.Add(S);
                }
            }
            openForm openFrm = new openForm(temp.ToArray(), false);
            openFrm.ShowDialog();

            string key_name = openFrm.getKey();
            if (key_name == "") return;

            string key_value = Registry.CurrentUser.OpenSubKey("Software\\Smartly Dressed Games\\Unturned\\")
                .GetValue(key_name)
                .ToString();

            key_value = key_value.Substring(0, key_value.Length - 1);
            key_value = key_value.Substring(4, key_value.Length - 4);

            lifeForm form = new lifeForm(Encryption.decrypt(key_value));
            form.ShowDialog();

            key_value = form.getLife();
            key_value = Encryption.encrypt(key_value);
            key_value = "0_6_" + key_value + "_";

            RegistryKey skillKey =
                Registry.CurrentUser.OpenSubKey("Software\\Smartly Dressed Games\\Unturned\\", true);
            skillKey.SetValue(key_name, key_value);

            MessageBox.Show("Success!");
        }

        #endregion

        #region Clothes editor

        private void clothesEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<string> temp = new List<string>();
            foreach (string S in KEYS)
            {
                if (S.StartsWith("clothes"))
                {
                    temp.Add(S);
                }
            }
            openForm openFrm = new openForm(temp.ToArray(), false);
            openFrm.ShowDialog();

            string key_name = openFrm.getKey();
            if (key_name == "") return;

            string key_value = Registry.CurrentUser.OpenSubKey("Software\\Smartly Dressed Games\\Unturned\\")
                .GetValue(key_name)
                .ToString();

            key_value = key_value.Substring(0, key_value.Length - 1);
            key_value = key_value.Substring(4, key_value.Length - 4);

            clothesForm form = new clothesForm(Encryption.decrypt(key_value));
            form.ShowDialog();

            key_value = form.getClothes();
            key_value = Encryption.encrypt(key_value);
            key_value = "0_7_" + key_value + "_";

            RegistryKey skillKey =
                Registry.CurrentUser.OpenSubKey("Software\\Smartly Dressed Games\\Unturned\\", true);
            skillKey.SetValue(key_name, key_value);

            MessageBox.Show("Success!");
        }

        #endregion

        #region Teleporter. MAP CREDITS: http://steamcommunity.com/id/liamallan/myworkshopfiles/?section=guides&appid=304930

        private void teleporterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<string> temp = new List<string>();
            foreach (string S in KEYS)
            {
                if (S.StartsWith("position"))
                {
                    temp.Add(S);
                }
            }
            openForm openFrm = new openForm(temp.ToArray(), false);
            openFrm.ShowDialog();

            string key_name = openFrm.getKey();
            if (key_name == "") return;

            string key_value = Registry.CurrentUser.OpenSubKey("Software\\Smartly Dressed Games\\Unturned\\")
                .GetValue(key_name)
                .ToString();

            int index = Convert.ToInt32(key_name.Split('_')[2]);

            key_value = key_value.Substring(0, key_value.Length - 1);
            key_value = key_value.Substring(6, key_value.Length - 6);

            teleporterForm form = new teleporterForm(Encryption.decrypt(key_value));
            form.ShowDialog();

            key_value = form.getPosition();
            key_value = Encryption.encrypt(key_value);
            key_value = "0_5_" + (index - 1).ToString() + "_" + key_value + "_";

            RegistryKey skillKey =
                Registry.CurrentUser.OpenSubKey("Software\\Smartly Dressed Games\\Unturned\\", true);
            skillKey.SetValue(key_name, key_value);

            MessageBox.Show("Success!");
        }

        #endregion

        #region Vehicle editor

        private void vehiclesEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<string> temp = new List<string>();
            foreach (string S in KEYS)
            {
                if (S.StartsWith("vehicles"))
                {
                    temp.Add(S);
                }
            }
            openForm openFrm = new openForm(temp.ToArray(), false);
            openFrm.ShowDialog();

            string key_name = openFrm.getKey();
            if (key_name == "") return;

            string key_value = Registry.CurrentUser.OpenSubKey("Software\\Smartly Dressed Games\\Unturned\\")
                .GetValue(key_name)
                .ToString();

            key_value = key_value.Substring(0, key_value.Length - 1);
            key_value = key_value.Substring(6, key_value.Length - 6);

            int index = Convert.ToInt32(key_name.Split('_')[1]);

            vehicleForm form = new vehicleForm(Encryption.decrypt(key_value));
            form.ShowDialog();

            key_value = form.getVehicles();
            key_value = Encryption.encrypt(key_value);
            key_value = "0_5_" + (index - 1) + "_" + key_value + "_";

            RegistryKey skillKey =
                Registry.CurrentUser.OpenSubKey("Software\\Smartly Dressed Games\\Unturned\\", true);
            skillKey.SetValue(key_name, key_value);

            MessageBox.Show("Success!");
        }

        #endregion

        #region Barricade / entity stuff editor (TBD)

        private void barricadesEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not yet implemented");
        }

        #endregion

        #region Reload all keys

        private void reloadAllKeysToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KEYS =
                new List<string>(
                    Registry.CurrentUser.OpenSubKey("Software\\Smartly Dressed Games\\Unturned").GetValueNames());
            KEYS.Sort();
        }

        #endregion

        #region On-demand encrypter-decrypter

        private void ondemandEncrypterdecrypterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            encrypter_decrypter e_dForm = new encrypter_decrypter();
            e_dForm.Show();
        }

        #endregion

        #region Registry value setter

        private void arbitraryValueSetterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            valueSetter valueForm = new valueSetter();
            valueForm.Show();
        }

        #endregion

        #region Backpack size forcer

        private void forceBackpackSizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            forceSizeForm form = new forceSizeForm();
            form.ShowDialog();

            forcedSize = form.getSize();
        }

        #endregion
    }

    public class Item
    {
        public string name = "<empty>";
        public int ID = -1;
        public float weight = -1;
        public int count = 0;
        public bool hasAttachments = false;
        public bool isToggleableOrFillable = false;

        public int[] attachments = new int[] {0, -1, -1, -1, -1, 0, -1};
        //mode, tactical, barrel, sight, magazine, bullets, tac light/laser ON/OFF
    }

    public class Vehicle
    {
        public string name = "";
        public int health = 0;
        public int gas = 0;
        public float x, y, z;
        public int Rx, Ry, Rz; //rotaion
        public float R, G, B; //color
    }
}