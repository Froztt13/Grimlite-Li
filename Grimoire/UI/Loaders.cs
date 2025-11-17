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
		
		private DarkTextBox txtSearchGrab;
		
		private DarkButton btnSearchGrab;
		
		private DarkButton btnGrab;
		
		private DarkButton btnSave;
		private DarkButton btnForceAccept;
		private DarkNumericUpDown numTQuests;
        private DarkComboBox cbOrderBy;
        private TreeView treeGrabbed;
        private DarkCheckBox cbGhost;

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

        public static Grabber.OrderBy order = Grabber.OrderBy.Name;
        
		// Search state for treeGrabbed
		private int _grabSearchIndex = 0;
		private string _grabSearchKeyword = "";
		private readonly List<TreeNode> _grabSearchResults = new List<TreeNode>();

		private void btnGrab_Click(object sender, EventArgs e)
		{
			treeGrabbed.BeginUpdate();
			treeGrabbed.Nodes.Clear();
			_grabSearchResults.Clear();
			_grabSearchKeyword = "";
			_grabSearchIndex = 0;

            if (cbOrderBy.SelectedItem != null && cbOrderBy.SelectedItem.ToString() == "Id")
                order = Grabber.OrderBy.Id;
            else
                order = Grabber.OrderBy.Name;


            try
            {
            switch (cbGrab.SelectedIndex)
            {
                case 0:
                    Grabber.GrabShopItems(treeGrabbed);
                    break;

                case 1:
                    Grabber.GrabQuestIds(treeGrabbed, order);
                    break;

                case 2:
                    Grabber.GrabQuests(treeGrabbed, order);
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
            }
            catch (Exception ex)
            {
                LogForm.Instance.devDebug($"[Fatal Error] : {ex}");
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

		private void btnSearchGrab_Click(object sender, EventArgs e)
		{
			string keyword = txtSearchGrab.Text;
			if (string.IsNullOrWhiteSpace(keyword) || treeGrabbed.Nodes.Count == 0)
				return;

			// If keyword changed, rebuild result list
			if (!string.Equals(keyword, _grabSearchKeyword, StringComparison.OrdinalIgnoreCase))
			{
				_grabSearchKeyword = keyword;
				_grabSearchResults.Clear();
				_grabSearchIndex = 0;
				CollectGrabSearchNodes(treeGrabbed.Nodes, keyword);
			}

			if (_grabSearchResults.Count == 0)
				return;

			if (_grabSearchIndex >= _grabSearchResults.Count)
				_grabSearchIndex = 0;

			TreeNode node = _grabSearchResults[_grabSearchIndex];
			treeGrabbed.SelectedNode = node;
			node.EnsureVisible();
			_grabSearchIndex++;
		}

		private void CollectGrabSearchNodes(TreeNodeCollection nodes, string keyword)
		{
			foreach (TreeNode node in nodes)
			{
				if (node.Text.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
				{
					_grabSearchResults.Add(node);
				}
				if (node.Nodes.Count > 0)
				{
					CollectGrabSearchNodes(node.Nodes, keyword);
				}
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
            this.txtSearchGrab = new DarkUI.Controls.DarkTextBox();
            this.btnSearchGrab = new DarkUI.Controls.DarkButton();
            this.btnGrab = new DarkUI.Controls.DarkButton();
            this.btnSave = new DarkUI.Controls.DarkButton();
            this.treeGrabbed = new System.Windows.Forms.TreeView();
            this.btnForceAccept = new DarkUI.Controls.DarkButton();
            this.numTQuests = new DarkUI.Controls.DarkNumericUpDown();
            this.cbOrderBy = new DarkUI.Controls.DarkComboBox();
            this.cbGhost = new DarkUI.Controls.DarkCheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.numTQuests)).BeginInit();
            this.SuspendLayout();
            // 
            // txtLoaders
            // 
            this.txtLoaders.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLoaders.Location = new System.Drawing.Point(12, 12);
            this.txtLoaders.Name = "txtLoaders";
            this.txtLoaders.Size = new System.Drawing.Size(156, 20);
            this.txtLoaders.TabIndex = 29;
            // 
            // cbLoad
            // 
            this.cbLoad.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbLoad.FormattingEnabled = true;
            this.cbLoad.Items.AddRange(new object[] {
            "Hair shop",
            "Shop",
            "Quest",
            "Armor customizer"});
            this.cbLoad.Location = new System.Drawing.Point(12, 38);
            this.cbLoad.Name = "cbLoad";
            this.cbLoad.Size = new System.Drawing.Size(156, 21);
            this.cbLoad.TabIndex = 30;
            this.cbLoad.SelectedIndexChanged += new System.EventHandler(this.cbLoad_SelectedIndexChanged);
            // 
            // btnLoad
            // 
            this.btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoad.Checked = false;
            this.btnLoad.Location = new System.Drawing.Point(12, 65);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(156, 23);
            this.btnLoad.TabIndex = 31;
            this.btnLoad.Text = "Load";
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // cbGrab
            // 
            this.cbGrab.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbGrab.FormattingEnabled = true;
            this.cbGrab.Items.AddRange(new object[] {
            "Shop items",
            "Quest IDs",
            "Quest items, drop rates",
            "Inventory items",
            "Temp inventory items",
            "Bank items",
            "Monsters",
            "All Monsters"});
            this.cbGrab.Location = new System.Drawing.Point(12, 306);
            this.cbGrab.Name = "cbGrab";
            this.cbGrab.Size = new System.Drawing.Size(174, 21);
            this.cbGrab.TabIndex = 33;
            this.cbGrab.SelectedIndexChanged += new System.EventHandler(this.cbGrab_SelectedIndexChanged);
            // 
            // txtSearchGrab
            // 
            this.txtSearchGrab.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearchGrab.Location = new System.Drawing.Point(12, 280);
            this.txtSearchGrab.Name = "txtSearchGrab";
            this.txtSearchGrab.Size = new System.Drawing.Size(174, 20);
            this.txtSearchGrab.TabIndex = 32;
            this.txtSearchGrab.Text = "Search";
            // 
            // btnSearchGrab
            // 
            this.btnSearchGrab.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearchGrab.Checked = false;
            this.btnSearchGrab.Location = new System.Drawing.Point(192, 278);
            this.btnSearchGrab.Name = "btnSearchGrab";
            this.btnSearchGrab.Size = new System.Drawing.Size(67, 23);
            this.btnSearchGrab.TabIndex = 36;
            this.btnSearchGrab.Text = "Search";
            this.btnSearchGrab.Click += new System.EventHandler(this.btnSearchGrab_Click);
            // 
            // btnGrab
            // 
            this.btnGrab.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGrab.Checked = false;
            this.btnGrab.Location = new System.Drawing.Point(134, 333);
            this.btnGrab.Name = "btnGrab";
            this.btnGrab.Size = new System.Drawing.Size(125, 26);
            this.btnGrab.TabIndex = 34;
            this.btnGrab.Text = "Grab";
            this.btnGrab.Click += new System.EventHandler(this.btnGrab_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Checked = false;
            this.btnSave.Location = new System.Drawing.Point(12, 333);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(121, 26);
            this.btnSave.TabIndex = 35;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // treeGrabbed
            // 
            this.treeGrabbed.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeGrabbed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(56)))));
            this.treeGrabbed.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treeGrabbed.ForeColor = System.Drawing.Color.Gainsboro;
            this.treeGrabbed.LabelEdit = true;
            this.treeGrabbed.Location = new System.Drawing.Point(12, 94);
            this.treeGrabbed.Name = "treeGrabbed";
            this.treeGrabbed.Size = new System.Drawing.Size(247, 180);
            this.treeGrabbed.TabIndex = 38;
            // 
            // btnForceAccept
            // 
            this.btnForceAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnForceAccept.Checked = false;
            this.btnForceAccept.Enabled = false;
            this.btnForceAccept.Location = new System.Drawing.Point(192, 38);
            this.btnForceAccept.Name = "btnForceAccept";
            this.btnForceAccept.Size = new System.Drawing.Size(67, 23);
            this.btnForceAccept.TabIndex = 44;
            this.btnForceAccept.Text = "F Accept";
            this.btnForceAccept.Click += new System.EventHandler(this.btnForceAccept_Click_1);
            // 
            // numTQuests
            // 
            this.numTQuests.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numTQuests.Enabled = false;
            this.numTQuests.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.numTQuests.Location = new System.Drawing.Point(192, 12);
            this.numTQuests.LoopValues = false;
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
            this.numTQuests.Size = new System.Drawing.Size(67, 20);
            this.numTQuests.TabIndex = 168;
            this.numTQuests.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // cbOrderBy
            // 
            this.cbOrderBy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbOrderBy.Enabled = false;
            this.cbOrderBy.FormattingEnabled = true;
            this.cbOrderBy.Items.AddRange(new object[] {
            "Name",
            "Id"});
            this.cbOrderBy.Location = new System.Drawing.Point(192, 306);
            this.cbOrderBy.Name = "cbOrderBy";
            this.cbOrderBy.Size = new System.Drawing.Size(67, 21);
            this.cbOrderBy.TabIndex = 169;
            // 
            // cbGhost
            // 
            this.cbGhost.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbGhost.AutoSize = true;
            this.cbGhost.Checked = true;
            this.cbGhost.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbGhost.Location = new System.Drawing.Point(192, 67);
            this.cbGhost.Name = "cbGhost";
            this.cbGhost.Size = new System.Drawing.Size(54, 17);
            this.cbGhost.TabIndex = 170;
            this.cbGhost.Text = "Ghost";
            // 
            // Loaders
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(271, 367);
            this.Controls.Add(this.cbGhost);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnGrab);
            this.Controls.Add(this.cbOrderBy);
            this.Controls.Add(this.numTQuests);
            this.Controls.Add(this.btnForceAccept);
            this.Controls.Add(this.treeGrabbed);
            this.Controls.Add(this.cbGrab);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.cbLoad);
            this.Controls.Add(this.btnSearchGrab);
            this.Controls.Add(this.txtLoaders);
            this.Controls.Add(this.txtSearchGrab);
            this.Icon = global::Properties.Resources.GrimoireIcon;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Loaders";
            this.Text = "Loaders and grabbers";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Loaders_FormClosing);
            this.Load += new System.EventHandler(this.Loaders_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numTQuests)).EndInit();
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
            string boxValue = cbGrab.SelectedItem.ToString();
            cbOrderBy.Enabled = enableGrabOn.Any(filter => boxValue.IndexOf(filter,StringComparison.OrdinalIgnoreCase) >= 0); 
            //cbGrab.SelectedIndex == 1 || cbGrab.SelectedIndex == 2 || cbGrab.SelectedIndex == 6;
        }
        string[] enableGrabOn = {"Monster","Quest"};

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
            await acceptBatchAsync(listQuests,cbGhost.Checked);
            btnForceAccept.Enabled = true;
        }

        private async Task acceptBatchAsync(List<int> listQuest, bool ghost)
        {
            Player.Quests.Load(listQuest);
            await Task.Delay(1000);
            for (int i = 0; i < listQuest.Count; i++)
            {
                if (!Player.Quests.IsInProgress(listQuest[i]))
                {
                    if (Player.Quests.Quest(listQuest[i]) != null)
                    {
                        if (ghost)
                            Player.Quests.Accept(listQuest[i].ToString());
                        else
                        {
                            Player.Quests.Accept(listQuest[i]);
                            await Task.Delay(400);
                        }
                    }
                    await Task.Delay(600);
                }
            }
        }
    }
}