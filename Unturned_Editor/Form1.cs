using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using Microsoft.Win32;

// 5x5 17.5 kg

namespace Unturned_Editor
{
    public partial class Form1 : Form
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

        #endregion

        #region Global separators and markers

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

        #endregion

        public Form1()
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

            slots[x, y].BackColor = Color.Gainsboro;

            x = ((PictureBox) sender).TabIndex/5;
            y = ((PictureBox) sender).TabIndex%5 - 1;
            if (y == -1)
            {
                x--;
                y = 4;
            }

            slots[x, y].BackColor = Color.Khaki;
            numericUpDown1.Value = items[x, y].count;

            if (items[x, y].hasAttachments) button1.Enabled = true;
            else numericUpDown1.Enabled = true;

            textBox1.Text += " ";
            textBox1.Clear();

            if (ITEMS.IndexOf(items[x, y].name) == -1)
            {
                if (items[x, y].name == "<empty>")
                    listBox1.SelectedIndex = 0;
                else
                    listBox1.SelectedIndex = 1;
            }
            else
            {
                listBox1.SelectedIndex = ITEMS.IndexOf(items[x, y].name) + 2;
            }

            nochange = false;
        }

        #endregion

        #region Change the item to the selected one in the listbox

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (nochange) return;
            try
            {
                if (listBox1.SelectedItem.ToString() == "<unknown>")
                {
                    MessageBox.Show("No. Just no.", "Really now?");
                    return;
                }
            }
            catch (Exception)
            {
                return;
            }

            button1.Enabled = false;
            numericUpDown1.Enabled = true;
            Item temp = new Item();

            temp.name = listBox1.SelectedItem.ToString();
            temp.ID = Items.getID(temp.name);
            temp.count = 1;
            numericUpDown1.Value = 1;

            List<string> weapons = new List<string>
            {
                "berette",
                "colt",
                "magnum",
                "lever action",
                "double barrel",
                "novuh",
                "mosen",
                "outfield",
                "maplestrike",
                "swissgewehr",
                "zubeknakov",
                "uzy",
                "proninety",
                "matamorez",
                "timberwolf",
                "longbow",
                "compound bow",
                "crossbow"
            };
            if (weapons.Contains(temp.name.ToLower()))
            {
                temp.hasAttachments = true;
                button1.Enabled = true;
                numericUpDown1.Enabled = false;
            }

            items[x, y] = temp;
            redraw();
        }

        #endregion

        #region Change item count

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
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
            listBox1.Items.Add("<unknown>");
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

            #region Redraw pictureboxex

            redraw();

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

        #region Generate the inventory string and save the inventory to the current key

        private void saveKeyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure?", "", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
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
                                                          (char) (items[i, j].count.ToString()[k] - 48 + 0xB0);
                                }
                                working_string += aux_working_string + Encoding.Default.GetString(normal_separator);
                                aux_working_string = "";
                                for (int k = items[i, j].ID.ToString().Length - 1; k >= 0; k--)
                                {
                                    aux_working_string += Encoding.Default.GetString(filler) +
                                                          (char) (items[i, j].ID.ToString()[k] - 48 + 0xB0);
                                }
                                working_string += aux_working_string;
                            }
                        }
                        else
                        {
                            working_string += Encoding.Default.GetString(normal_separator);
                            working_string +=
                                Encoding.Default.GetString(new byte[]
                                {0xE2, 0x81, 0x9F, 0xE2, 0x81, 0xB9, 0xE2, 0x81, 0x9F});
                            working_string += Encoding.Default.GetString(filler);
                            working_string += (char) (items[i, j].attachments[0] + 0xB0);
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
                                                       (char) (items[i, j].attachments[5].ToString()[k] - 48 + 0xB0);
                            }
                            working_string += auxx_working_string + Encoding.Default.GetString(normal_separator) +
                                              Encoding.Default.GetString(new byte[] {0xE2, 0x80, 0xB1}) +
                                              Encoding.Default.GetString(normal_separator);

                            auxx_working_string = "";
                            for (int k = items[i, j].ID.ToString().Length - 1; k >= 0; k--)
                            {
                                auxx_working_string += Encoding.Default.GetString(filler) +
                                                       (char) (items[i, j].ID.ToString()[k] - 48 + 0xB0);
                            }
                            working_string += auxx_working_string;
                        }
                        current_inventory += working_string;
                        current_inventory += Encoding.Default.GetString(master_separator);
                    }

                working_string = "";

                working_string = ((int) (max_weight*1000)).ToString();
                working_string = new string((working_string.ToCharArray().Reverse()).ToArray());
                foreach (char C in working_string)
                {
                    current_inventory += Encoding.Default.GetString(filler) + (char) (C - 48 + 0xB0);
                }
                current_inventory += Encoding.Default.GetString(master_separator) +
                                     Encoding.Default.GetString(new byte[] {0xE2, 0x80}) +
                                     (char) (size[0] + 0xB0) +
                                     Encoding.Default.GetString(master_separator) +
                                     Encoding.Default.GetString(new byte[] {0xE2, 0x80}) +
                                     (char) (size[1] + 0xB0) +
                                     (char) (0x5F);

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
            for (int i = 0; i < size[0]; i++)
                for (int j = 0; j < size[1]; j++)
                {
                    slots[i, j].BackColor = Color.Gainsboro;
                    if (items[i, j].name != "<empty>")
                        slots[i, j].Image = DrawText(items[i, j].name + "\n\n    x" + items[i, j].count.ToString(),
                            new Font("Arial", 25.0f), Color.DarkRed,
                            Color.Transparent);
                    slots[i, j].SizeMode = PictureBoxSizeMode.StretchImage;
                }
        }

        #endregion


        //Miscellaneous (tools):


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
    }

    public class Item
    {
        public string name = "";
        public int ID = 0;
        public float weight = 0;
        public int count = 1;
        public bool hasAttachments = false;
        public int[] attachments = new int[] {0, -1, -1, -1, -1, 0, 0};
    }
}