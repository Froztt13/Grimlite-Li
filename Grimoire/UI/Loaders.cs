using DarkUI.Controls;
using DarkUI.Forms;
using Grimoire.Botting;
using Grimoire.Game;
using Grimoire.Game.Data;
using Grimoire.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Grimoire.UI
{
    public class Loaders : DarkForm
    {
        public enum Type
        {
            ShopItems,
            QuestIDs,
            Quests,
            InventoryItems,
            TempItems,
            BankItems,
            Monsters
        }

        private IContainer components;

        private DarkTextBox txtLoaders;

        private DarkComboBox cbLoad;

        private DarkButton btnLoad;

        private DarkComboBox cbGrab;

        private DarkButton btnGrab;

        private DarkButton btnSave;
        private DarkButton btnForceAccept;
        private DarkNumericUpDown numTQuests;
        private DarkComboBox cbOrderBy;
        private TreeView treeGrabbed;
		private SplitContainer splitContainer1;
		private DarkGroupBox darkGroupBox1;


		public static Loaders Instance
        {
            get;
        } = new Loaders();

        public static Type TreeType
        {
            get;
            set;
        }

        private Loaders()
        {
            InitializeComponent();
            toolTip = new System.Windows.Forms.ToolTip();
            toolTip.SetToolTip(btnForceAccept, "Ghost accept the quest");
        }
        private System.Windows.Forms.ToolTip toolTip;

        private void btnLoad_Click(object sender, EventArgs e)
        {
            int result;
            switch (cbLoad.SelectedIndex)
            {
                case 0:
                    if (int.TryParse(txtLoaders.Text, out result))
                    {
                        Shop.LoadHairShop(result);
                    }
                    break;

                case 1:
                    if (int.TryParse(txtLoaders.Text, out result))
                    {
                        Shop.Load(result);
                    }
                    break;

                case 2:
                    if (this.txtLoaders.Text.Contains(","))
                    {
                        this.LoadQuests(this.txtLoaders.Text);

                        return;
                    }
                    int id;
                    if (int.TryParse(this.txtLoaders.Text, out id))
                    {
                        int questId = Int32.Parse(txtLoaders.Text);
                        string quests = "";
                        int increament = (int)numTQuests.Value;
                        if (increament > 0)
                        {
                            for (int i = 0; i < increament; i++)
                            {
                                quests += questId + (i < increament - 1 && increament != 1 ? "," : "");
                                questId++;
                            }
                            Console.WriteLine("quests: " + quests);
                            LoadQuests(quests);
                        }
                        else
                        {
                            Player.Quests.Load(id);
                        }
                        return;
                    }
                    break;

                case 3:
                    Shop.LoadArmorCustomizer();
                    break;
            }
        }

        private void LoadQuests(string str)
        {
            string[] source = str.Split(',');
            if (source.All((string s) => s.All(char.IsDigit)))
            {
                Player.Quests.Load(source.Select(int.Parse).ToList());
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Title = "Save grabber data",
                CheckFileExists = false,
                Filter = "XML files|*.xml"
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //using (Stream file = File.Open(openFileDialog.FileName, FileMode.Create))
                //{
                //    BinaryFormatter bf = new BinaryFormatter();
                //    bf.Serialize(file, treeGrabbed.Nodes.Cast<TreeNode>().ToList());
                //}
                XmlTextWriter textWriter = new XmlTextWriter(openFileDialog.FileName, System.Text.Encoding.ASCII)
                {
                    // set formatting style to indented
                    Formatting = Formatting.Indented
                };
                // writing the xml declaration tag
                textWriter.WriteStartDocument();
                // format it with new lines
                textWriter.WriteRaw("\r\n");
                // writing the main tag that encloses all node tags
                textWriter.WriteStartElement("TreeView");
                // save the nodes, recursive method
                SaveNodes(treeGrabbed.Nodes, textWriter);

                textWriter.WriteEndElement();

                textWriter.Close();
            }
        }

        private const string XmlNodeTag = "n";
        private const string XmlNodeTextAtt = "t";
        private const string XmlNodeTagAtt = "tg";
        private const string XmlNodeImageIndexAtt = "imageindex";

        private void SaveNodes(TreeNodeCollection nodesCollection, XmlTextWriter textWriter)
        {
            for (int i = 0; i < nodesCollection.Count; i++)
            {
                TreeNode node = nodesCollection[i];
                textWriter.WriteStartElement(XmlNodeTag);
                try
                {
                    string toadd = "";
                    for (int times = node.Text.Split(':')[0].Length; 9 > times; times++)
                        toadd += " ";
                    textWriter.WriteAttributeString(XmlNodeTextAtt, $"{node.Text.Split(':')[0]}:{toadd}{node.Text.Split(':')[1]}");
                }
                catch
                {
                    //string toadd = "";
                    //for (int times = node.Text.Split(':')[0].Length; 15 > times; times++)
                    //    toadd += "-";
                    textWriter.WriteAttributeString(XmlNodeTextAtt, $"{node.Text}");
                }
                //textWriter.WriteAttributeString(node.Text.Split(':')[0], node.Text.Split(':')[1]);
                //textWriter.WriteAttributeString(XmlNodeImageIndexAtt, node.ImageIndex.ToString());
                if (node.Tag != null)
                    textWriter.WriteAttributeString(XmlNodeTagAtt, node.Tag.ToString());
                // add other node properties to serialize here  
                if (node.Nodes.Count > 0)
                {
                    SaveNodes(node.Nodes, textWriter);
                }
                textWriter.WriteEndElement();
            }
        }

        private void btnGrab_Click(object sender, EventArgs e)
        {
            treeGrabbed.BeginUpdate();
            treeGrabbed.Nodes.Clear();

            Grabber.OrderBy orderBy = Grabber.OrderBy.Name;
            switch (cbOrderBy.SelectedIndex)
            {
                case 0:
                    orderBy = Grabber.OrderBy.Name;
                    break;
                case 1:
                    orderBy = Grabber.OrderBy.Id;
                    break;
            }

            switch (cbGrab.SelectedIndex)
            {
                case 0:
                    Grabber.GrabShopItems(treeGrabbed);
                    break;

                case 1:
                    Grabber.GrabQuestIds(treeGrabbed, orderBy);
                    break;

                case 2:
                    Grabber.GrabQuests(treeGrabbed, orderBy);
                    break;

                case 3:
                    Grabber.GrabInventoryItems(treeGrabbed);
                    break;

                case 4:
                    Grabber.GrabTempItems(treeGrabbed);
                    break;

                case 5:
                    Grabber.GrabBankItems(treeGrabbed);
                    break;

                case 6:
                    Grabber.GrabMonsters(treeGrabbed);
                    break;
                case 7:
                    Grabber.GrabAllMonsters(treeGrabbed);
                    break;
            }
            treeGrabbed.EndUpdate();
        }

        private void Loaders_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
			this.txtLoaders = new DarkUI.Controls.DarkTextBox();
			this.cbLoad = new DarkUI.Controls.DarkComboBox();
			this.btnLoad = new DarkUI.Controls.DarkButton();
			this.cbGrab = new DarkUI.Controls.DarkComboBox();
			this.btnGrab = new DarkUI.Controls.DarkButton();
			this.btnSave = new DarkUI.Controls.DarkButton();
			this.treeGrabbed = new System.Windows.Forms.TreeView();
			this.btnForceAccept = new DarkUI.Controls.DarkButton();
			this.numTQuests = new DarkUI.Controls.DarkNumericUpDown();
			this.cbOrderBy = new DarkUI.Controls.DarkComboBox();
			this.darkGroupBox1 = new DarkUI.Controls.DarkGroupBox();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			((System.ComponentModel.ISupportInitialize)(this.numTQuests)).BeginInit();
			this.darkGroupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// txtLoaders
			// 
			this.txtLoaders.Location = new System.Drawing.Point(9, 15);
			this.txtLoaders.Multiline = true;
			this.txtLoaders.Name = "txtLoaders";
			this.txtLoaders.Size = new System.Drawing.Size(233, 33);
			this.txtLoaders.TabIndex = 29;
			this.txtLoaders.WordWrap = false;
			// 
			// cbLoad
			// 
			this.cbLoad.FormattingEnabled = true;
			this.cbLoad.ItemHeight = 27;
			this.cbLoad.Items.AddRange(new object[] {
            "Hair shop",
            "Shop",
            "Quest",
            "Armor customizer"});
			this.cbLoad.Location = new System.Drawing.Point(9, 51);
			this.cbLoad.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.cbLoad.Name = "cbLoad";
			this.cbLoad.Size = new System.Drawing.Size(233, 33);
			this.cbLoad.TabIndex = 30;
			this.cbLoad.SelectedIndexChanged += new System.EventHandler(this.cbLoad_SelectedIndexChanged);
			// 
			// btnLoad
			// 
			this.btnLoad.Checked = false;
			this.btnLoad.Location = new System.Drawing.Point(9, 88);
			this.btnLoad.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btnLoad.Name = "btnLoad";
			this.btnLoad.Size = new System.Drawing.Size(234, 33);
			this.btnLoad.TabIndex = 31;
			this.btnLoad.Text = "Load";
			this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
			// 
			// cbGrab
			// 
			this.cbGrab.FormattingEnabled = true;
			this.cbGrab.ItemHeight = 27;
			this.cbGrab.Items.AddRange(new object[] {
            "Shop items",
            "Quest IDs",
            "Quest items, drop rates",
            "Inventory items",
            "Temp inventory items",
            "Bank items",
            "Monsters",
            "All Monsters"});
			this.cbGrab.Location = new System.Drawing.Point(9, 506);
			this.cbGrab.Name = "cbGrab";
			this.cbGrab.Size = new System.Drawing.Size(246, 33);
			this.cbGrab.TabIndex = 33;
			this.cbGrab.SelectedIndexChanged += new System.EventHandler(this.cbGrab_SelectedIndexChanged);
			// 
			// btnGrab
			// 
			this.btnGrab.Checked = false;
			this.btnGrab.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnGrab.Location = new System.Drawing.Point(0, 0);
			this.btnGrab.Name = "btnGrab";
			this.btnGrab.Size = new System.Drawing.Size(176, 33);
			this.btnGrab.TabIndex = 34;
			this.btnGrab.Text = "Grab";
			this.btnGrab.Click += new System.EventHandler(this.btnGrab_Click);
			// 
			// btnSave
			// 
			this.btnSave.Checked = false;
			this.btnSave.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnSave.Location = new System.Drawing.Point(0, 0);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(180, 33);
			this.btnSave.TabIndex = 35;
			this.btnSave.Text = "Save";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// treeGrabbed
			// 
			this.treeGrabbed.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.treeGrabbed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(56)))));
			this.treeGrabbed.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.treeGrabbed.ForeColor = System.Drawing.Color.Gainsboro;
			this.treeGrabbed.LabelEdit = true;
			this.treeGrabbed.Location = new System.Drawing.Point(9, 127);
			this.treeGrabbed.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.treeGrabbed.Name = "treeGrabbed";
			this.treeGrabbed.Size = new System.Drawing.Size(359, 375);
			this.treeGrabbed.TabIndex = 38;
			// 
			// btnForceAccept
			// 
			this.btnForceAccept.Checked = false;
			this.btnForceAccept.Enabled = false;
			this.btnForceAccept.Location = new System.Drawing.Point(8, 64);
			this.btnForceAccept.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btnForceAccept.Name = "btnForceAccept";
			this.btnForceAccept.Size = new System.Drawing.Size(100, 35);
			this.btnForceAccept.TabIndex = 44;
			this.btnForceAccept.Text = "Accept";
			this.btnForceAccept.Click += new System.EventHandler(this.btnForceAccept_Click_1);
			// 
			// numTQuests
			// 
			this.numTQuests.Enabled = false;
			this.numTQuests.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
			this.numTQuests.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
			this.numTQuests.Location = new System.Drawing.Point(8, 29);
			this.numTQuests.LoopValues = false;
			this.numTQuests.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.numTQuests.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
			this.numTQuests.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numTQuests.Name = "numTQuests";
			this.numTQuests.Size = new System.Drawing.Size(100, 28);
			this.numTQuests.TabIndex = 168;
			this.numTQuests.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// cbOrderBy
			// 
			this.cbOrderBy.Enabled = false;
			this.cbOrderBy.FormattingEnabled = true;
			this.cbOrderBy.ItemHeight = 27;
			this.cbOrderBy.Items.AddRange(new object[] {
            "Id",
            "Name"});
			this.cbOrderBy.Location = new System.Drawing.Point(263, 506);
			this.cbOrderBy.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.cbOrderBy.Name = "cbOrderBy";
			this.cbOrderBy.Size = new System.Drawing.Size(106, 33);
			this.cbOrderBy.TabIndex = 169;
			// 
			// darkGroupBox1
			// 
			this.darkGroupBox1.Controls.Add(this.numTQuests);
			this.darkGroupBox1.Controls.Add(this.btnForceAccept);
			this.darkGroupBox1.Location = new System.Drawing.Point(248, 10);
			this.darkGroupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.darkGroupBox1.Name = "darkGroupBox1";
			this.darkGroupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.darkGroupBox1.Size = new System.Drawing.Size(120, 108);
			this.darkGroupBox1.TabIndex = 172;
			this.darkGroupBox1.TabStop = false;
			this.darkGroupBox1.Text = "Increament";
			// 
			// splitContainer1
			// 
			this.splitContainer1.Location = new System.Drawing.Point(9, 543);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.btnSave);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.btnGrab);
			this.splitContainer1.Size = new System.Drawing.Size(360, 33);
			this.splitContainer1.SplitterDistance = 180;
			this.splitContainer1.TabIndex = 173;
			// 
			// Loaders
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(379, 590);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.darkGroupBox1);
			this.Controls.Add(this.cbOrderBy);
			this.Controls.Add(this.treeGrabbed);
			this.Controls.Add(this.cbGrab);
			this.Controls.Add(this.btnLoad);
			this.Controls.Add(this.cbLoad);
			this.Controls.Add(this.txtLoaders);
			this.Icon = global::Properties.Resources.GrimoireIcon;
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Loaders";
			this.Text = "Loaders and grabbers";
			this.TopMost = true;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Loaders_FormClosing);
			this.Load += new System.EventHandler(this.Loaders_Load);
			((System.ComponentModel.ISupportInitialize)(this.numTQuests)).EndInit();
			this.darkGroupBox1.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        private readonly string font = Config.Load(Application.StartupPath + "\\config.cfg").Get("font");
        private readonly float? fontSize = float.Parse(Config.Load(Application.StartupPath + "\\config.cfg").Get("fontSize") ?? "8.25", System.Globalization.CultureInfo.InvariantCulture.NumberFormat);

        private void Loaders_Load(object sender, EventArgs e)
        {
            this.FormClosing += this.Loaders_FormClosing;
            if (font != null && fontSize != null)
            {
                this.Font = new Font(font, (float)fontSize, FontStyle.Regular, GraphicsUnit.Point, 0);
            }
        }

        private void cbLoad_SelectedIndexChanged(object sender, EventArgs e)
        {
            numTQuests.Enabled = cbLoad.SelectedIndex == 2;
            btnForceAccept.Enabled = cbLoad.SelectedIndex == 2;
        }

        private void cbGrab_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbOrderBy.Enabled = cbGrab.SelectedIndex == 1 || cbGrab.SelectedIndex == 2;
        }

        private void btnForceAccept_Click(object sender, EventArgs e)
        {
            try
            {
                Player.Quests.Accept(int.Parse(txtLoaders.Text));
            }
            catch { }
        }

        private async void btnForceAccept_Click_1(object sender, EventArgs e)
        {
            btnForceAccept.Enabled = false;
            if (txtLoaders.Text == null) return;
            int questId = Int32.Parse(txtLoaders.Text);
            List<int> listQuests = new List<int>();
            for (int i = 0; i < (int)numTQuests.Value; i++)
            {
                listQuests.Add(questId);
                questId++;
            }
            await acceptBatchAsync(listQuests);
            btnForceAccept.Enabled = true;
        }

        private async Task acceptBatchAsync(List<int> listQuest)
        {
            Player.Quests.Get(listQuest);
            await Task.Delay(1000);
            for (int i = 0; i < listQuest.Count; i++)
            {
                if (!Player.Quests.IsInProgress(listQuest[i]))
                {
                    if (Player.Quests.Quest(listQuest[i]) != null)
                    {
                        Player.Quests.Accept(listQuest[i]);
                    }
                    await Task.Delay(600);
                }
            }
        }
    }
}