using DarkUI.Controls;
using DarkUI.Forms;
using Grimoire.Botting.Commands.Misc.Statements;
using Grimoire.Botting.Commands.Quest;
using Grimoire.Game;
using Grimoire.Game.Data;
using Grimoire.Properties;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Grimoire.UI
{
    public class UserFriendlyCommandEditor : DarkForm
    {
        private IContainer components;
        private DarkButton btnOK;
        private ToolStripContainer toolStripContainer1;
        private SplitContainer splitContainer1;
        private DarkButton btnRawCommand;
        private DarkButton btnCancel;
        private DarkButton btnGetInfo;

        private static object cmdObj
        {
            get;
            set;
        }

        private static UserFriendlyCommandEditor commandEditor
        {
            get;
            set;
        }

        public static string cmd
        {
            get;
            set;
        }

        private static readonly JsonSerializerSettings _questSerializerSettings = new JsonSerializerSettings
        {
            DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = NullValueHandling.Include,
            TypeNameHandling = TypeNameHandling.All
        };

        private static readonly JsonSerializerSettings _serializerSettings = new JsonSerializerSettings
        {
            DefaultValueHandling = DefaultValueHandling.Include,
            NullValueHandling = NullValueHandling.Include,
            TypeNameHandling = TypeNameHandling.All
        };

        private List<StatementCommand> statementCommands;

        private UserFriendlyCommandEditor()
        {
            InitializeComponent();
            statementCommands = JsonConvert.DeserializeObject<List<StatementCommand>>(Resources.statementcmds, _serializerSettings);
        }

        private void RawCommandEditor_Load(object sender, EventArgs e)
        {

        }

        private void txtCmd_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Return:
                    btnOK.PerformClick();
                    break;

                case Keys.Escape:
                    btnCancel.PerformClick();
                    break;
            }
        }

        public static string Show(object obj)
        {
            cmdObj = obj;
            var serializer = obj.GetType() == typeof(CmdCompleteQuest) || obj.GetType() == typeof(CmdAcceptQuest) ? _questSerializerSettings : _serializerSettings;
            //MessageBox.Show(obj.GetType().ToString());
            JObject content = JObject.Parse(JsonConvert.SerializeObject(obj, serializer));
            using (commandEditor = new UserFriendlyCommandEditor())
            {
                int currentY = 13;
                int count = 0;
                string[] skip = { "Tag", "Description1", "Description2", "$type" };
                Dictionary<string, KeyValuePair<DarkLabel, DarkTextBox>> currentVars = new Dictionary<string, KeyValuePair<DarkLabel, DarkTextBox>>();
                foreach (KeyValuePair<string, JToken> item in content)
                {

                    if (!string.IsNullOrEmpty(item.Key) && Array.IndexOf(skip, item.Key) == -1 && commandEditor.statementCommands.Find((StatementCommand s) => s.GetType() == content.GetType())?.Text != item.Key)
                    {
                        string lblText = item.Key;
                        string tbText = item.Value.ToString();
                        switch (item.Key)
                        {
                            case "Value1":
                                lblText = commandEditor.statementCommands.Find((StatementCommand s) => s.GetType() == obj.GetType()).Description1;
                                tbText = tbText == lblText ? "" : tbText;
                                break;
                            case "Value2":
                                lblText = commandEditor.statementCommands.Find((StatementCommand s) => s.GetType() == obj.GetType()).Description2;
                                tbText = tbText == lblText ? "" : tbText;
                                break;
                            case "Quest":
                                var qObj = JsonConvert.DeserializeObject<Quest>(item.Value.ToString());
                                lblText = "Quest ID"; 
                                tbText = qObj.Id.ToString(); 
                                break;
                        }
                        currentVars.Add(item.Key, new KeyValuePair<DarkLabel, DarkTextBox>(
                            new DarkLabel()
                            {
                                Name = $"lbl{item.Key}{count}",
                                Text = lblText,
                                Size = new System.Drawing.Size(90, 33),
                                Location = new System.Drawing.Point(25, currentY + 2),
                                Anchor = AnchorStyles.Left | AnchorStyles.Top
                            },
                            new DarkTextBox()
                            {
                                Name = $"tb{item.Key}{count}",
                                Text = tbText,
                                Size = new System.Drawing.Size(220, 33),
                                Location = new System.Drawing.Point(125, currentY),
                                Anchor = AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Left
                            }));
                        commandEditor.Controls.Add(currentVars[item.Key].Key);
                        commandEditor.Controls.Add(currentVars[item.Key].Value);

                        // Make key handler for each textbox
                        currentVars[item.Key].Value.KeyDown += commandEditor.txtCmd_KeyDown;
                        count++;
                        currentY += 36;
                    }
                    //honestly i have no fucking idea how to implement this properly
                }
                bool hasMapProps = currentVars.Keys.Any(k =>
    k.IndexOf("cell", StringComparison.OrdinalIgnoreCase) >= 0 ||
    k.IndexOf("map", StringComparison.OrdinalIgnoreCase) >= 0 ||
    k.IndexOf("pad", StringComparison.OrdinalIgnoreCase) >= 0);

                if (!hasMapProps)
                {
                    commandEditor.btnGetInfo.Visible = false;
                    commandEditor.Size = new Size(commandEditor.Size.Width, commandEditor.Size.Height + currentY - 13);
                }
                else
                {
                    commandEditor.btnGetInfo.Visible = true;
                    commandEditor.Size = new Size(commandEditor.Size.Width, commandEditor.Size.Height + currentY + 10);
                }
                DialogResult results = commandEditor.ShowDialog();
                bool dialog = results == DialogResult.OK;
                bool dialog2 = results == DialogResult.Abort;
                if (dialog)
                {
                    foreach (KeyValuePair<string, JToken> item in content)
                    {
                        if (currentVars.ContainsKey(item.Key))
                        {
                            if (item.Key == "Quest")
                                continue;
                            content[item.Key] = currentVars[item.Key].Value.Text;
                        }
                    }
                    if (currentVars.ContainsKey("Quest"))
                    {
                        if (int.TryParse(currentVars["Quest"].Value.Text, out int newId))
                        {
                            JObject questObj = (JObject)content["Quest"];
                            questObj["QuestID"] = newId; // update hanya field QuestID
                        }
                    }
                    var serialized = JsonConvert.SerializeObject(content, Formatting.Indented, _serializerSettings);
                    return serialized;
                }
                else if (dialog2)
                    return RawCommandEditor.Show(JsonConvert.SerializeObject(cmdObj, Formatting.Indented, serializer));
                else return null;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserFriendlyCommandEditor));
			this.btnOK = new DarkUI.Controls.DarkButton();
			this.btnCancel = new DarkUI.Controls.DarkButton();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.btnRawCommand = new DarkUI.Controls.DarkButton();
			this.btnGetInfo = new DarkUI.Controls.DarkButton();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnOK
			// 
			this.btnOK.Checked = false;
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnOK.Location = new System.Drawing.Point(0, 0);
			this.btnOK.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(206, 35);
			this.btnOK.TabIndex = 0;
			this.btnOK.Text = "OK";
			// 
			// btnCancel
			// 
			this.btnCancel.Checked = false;
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnCancel.Location = new System.Drawing.Point(0, 0);
			this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(211, 35);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "Cancel";
			// 
			// splitContainer1
			// 
			this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.splitContainer1.Location = new System.Drawing.Point(18, 108);
			this.splitContainer1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.btnCancel);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.btnOK);
			this.splitContainer1.Size = new System.Drawing.Size(423, 35);
			this.splitContainer1.SplitterDistance = 211;
			this.splitContainer1.SplitterWidth = 6;
			this.splitContainer1.TabIndex = 2;
			// 
			// btnRawCommand
			// 
			this.btnRawCommand.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnRawCommand.Checked = false;
			this.btnRawCommand.DialogResult = System.Windows.Forms.DialogResult.Abort;
			this.btnRawCommand.Location = new System.Drawing.Point(18, 63);
			this.btnRawCommand.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btnRawCommand.Name = "btnRawCommand";
			this.btnRawCommand.Size = new System.Drawing.Size(423, 35);
			this.btnRawCommand.TabIndex = 3;
			this.btnRawCommand.Text = "Raw Command Editor";
			// 
			// btnGetInfo
			// 
			this.btnGetInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnGetInfo.Checked = false;
			this.btnGetInfo.Location = new System.Drawing.Point(18, 19);
			this.btnGetInfo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btnGetInfo.Name = "btnGetInfo";
			this.btnGetInfo.Size = new System.Drawing.Size(423, 35);
			this.btnGetInfo.TabIndex = 4;
			this.btnGetInfo.Text = "Get Cell / Map / Pad";
			this.btnGetInfo.Click += new System.EventHandler(this.BtnGetInfo_Click);
			// 
			// UserFriendlyCommandEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(462, 162);
			this.Controls.Add(this.btnGetInfo);
			this.Controls.Add(this.btnRawCommand);
			this.Controls.Add(this.splitContainer1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.MaximizeBox = false;
			this.Name = "UserFriendlyCommandEditor";
			this.Text = "Command Editor";
			this.TopMost = true;
			this.Load += new System.EventHandler(this.RawCommandEditor_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UserFriendlyCommandEditor_KeyDown);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);

        }
        private void BtnGetInfo_Click(object sender, EventArgs e)
        {
            try
            {
                //Player current location
                string cell = Player.Cell;
                string map = Player.Map;
                string pad = Player.Pad;

                // Fill relevant textbox
                foreach (Control ctrl in this.Controls)
                {
                    if (ctrl is DarkTextBox tb)
                    {
                        if (tb.Name.IndexOf("swf", StringComparison.OrdinalIgnoreCase) >= 0 &&
                            tb.Name.IndexOf("max", StringComparison.OrdinalIgnoreCase) >= 0)
                            continue;
                        else if (tb.Name.IndexOf("Cell", StringComparison.OrdinalIgnoreCase) >= 0)
                            tb.Text = cell;
                        else if (tb.Name.IndexOf("Map", StringComparison.OrdinalIgnoreCase) >= 0)
                            tb.Text = map;
                        else if (tb.Name.IndexOf("Pad", StringComparison.OrdinalIgnoreCase) >= 0)
                            tb.Text = pad;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal mengambil info dari Player.\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void UserFriendlyCommandEditor_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Return:
                    btnOK.PerformClick();
                    break;

                case Keys.Escape:
                    btnCancel.PerformClick();
                    break;
            }
        }
    }
}