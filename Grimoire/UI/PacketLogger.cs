using DarkUI.Controls;
using DarkUI.Forms;
using Grimoire.FlashTools;
using Grimoire.Networking;
using Grimoire.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grimoire.UI
{
    public class PacketLogger : DarkForm
    {
        private IContainer components;

        private DarkTextBox txtPackets;

        private DarkButton btnStart;

        private DarkButton btnStop;

        private DarkButton btnCopy;
        private TableLayoutPanel tableLayoutPanel1;
        private Label x;
        private DarkButton btnSpam;
        private DarkButton btnSendOnce;
        private DarkTextBox textToSend;
        private DarkNumericUpDown numSpamTimes;
        private DarkNumericUpDown numSpamDelay;
        private Label label1;
        private Label label2;
        private DarkButton btnClear;

        public static PacketLogger Instance
        {
            get;
        } = new PacketLogger();

        private PacketLogger()
        {
            InitializeComponent();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtPackets.Clear();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (txtPackets.Text.Length > 0)
            {
                Clipboard.SetText(txtPackets.Text);
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            //Proxy.Instance.ReceivedFromClient -= PacketCaptured;
            btnStart.Enabled = true;
            Flash.FlashCall -= FlashUtil_FlashCall;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            //Proxy.Instance.ReceivedFromClient += PacketCaptured;
            btnStart.Enabled = false;
            Flash.FlashCall += FlashUtil_FlashCall;
        }

        private void FlashUtil_FlashCall(AxShockwaveFlashObjects.AxShockwaveFlash flash, string function, params object[] args)
        {
            if (function == "packetFromClient")
            {
                string packet = args[0].ToString();
                txtPackets.Invoke((Action)delegate
                {
                    txtPackets.AppendText(packet + Environment.NewLine);
                });
            }
        }

        private void PacketCaptured(Networking.Message msg)
        {
            if (msg.RawContent != null && msg.RawContent.Contains("%xt%zm%"))
            {
                txtPackets.Invoke((Action)delegate
                {
                    txtPackets.AppendText(msg.RawContent + Environment.NewLine);
                });
            }
        }

        private void PacketLogger_FormClosing(object sender, FormClosingEventArgs e)
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
			this.txtPackets = new DarkUI.Controls.DarkTextBox();
			this.btnStart = new DarkUI.Controls.DarkButton();
			this.btnStop = new DarkUI.Controls.DarkButton();
			this.btnCopy = new DarkUI.Controls.DarkButton();
			this.btnClear = new DarkUI.Controls.DarkButton();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.x = new System.Windows.Forms.Label();
			this.btnSpam = new DarkUI.Controls.DarkButton();
			this.btnSendOnce = new DarkUI.Controls.DarkButton();
			this.textToSend = new DarkUI.Controls.DarkTextBox();
			this.numSpamTimes = new DarkUI.Controls.DarkNumericUpDown();
			this.numSpamDelay = new DarkUI.Controls.DarkNumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.tableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numSpamTimes)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numSpamDelay)).BeginInit();
			this.SuspendLayout();
			// 
			// txtPackets
			// 
			this.txtPackets.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtPackets.Location = new System.Drawing.Point(18, 18);
			this.txtPackets.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.txtPackets.MaxLength = 2147483647;
			this.txtPackets.Multiline = true;
			this.txtPackets.Name = "txtPackets";
			this.txtPackets.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtPackets.Size = new System.Drawing.Size(640, 359);
			this.txtPackets.TabIndex = 15;
			this.txtPackets.WordWrap = false;
			// 
			// btnStart
			// 
			this.btnStart.Checked = false;
			this.btnStart.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnStart.Location = new System.Drawing.Point(484, 5);
			this.btnStart.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btnStart.Name = "btnStart";
			this.btnStart.Size = new System.Drawing.Size(152, 36);
			this.btnStart.TabIndex = 16;
			this.btnStart.Text = "Start";
			this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
			// 
			// btnStop
			// 
			this.btnStop.Checked = false;
			this.btnStop.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnStop.Location = new System.Drawing.Point(324, 5);
			this.btnStop.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btnStop.Name = "btnStop";
			this.btnStop.Size = new System.Drawing.Size(152, 36);
			this.btnStop.TabIndex = 17;
			this.btnStop.Text = "Stop";
			this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
			// 
			// btnCopy
			// 
			this.btnCopy.Checked = false;
			this.btnCopy.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnCopy.Location = new System.Drawing.Point(164, 5);
			this.btnCopy.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btnCopy.Name = "btnCopy";
			this.btnCopy.Size = new System.Drawing.Size(152, 36);
			this.btnCopy.TabIndex = 18;
			this.btnCopy.Text = "Copy";
			this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
			// 
			// btnClear
			// 
			this.btnClear.Checked = false;
			this.btnClear.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnClear.Location = new System.Drawing.Point(4, 5);
			this.btnClear.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new System.Drawing.Size(152, 36);
			this.btnClear.TabIndex = 19;
			this.btnClear.Text = "Clear";
			this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel1.ColumnCount = 4;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel1.Controls.Add(this.btnClear, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.btnStart, 3, 0);
			this.tableLayoutPanel1.Controls.Add(this.btnStop, 2, 0);
			this.tableLayoutPanel1.Controls.Add(this.btnCopy, 1, 0);
			this.tableLayoutPanel1.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(18, 388);
			this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(640, 46);
			this.tableLayoutPanel1.TabIndex = 20;
			// 
			// x
			// 
			this.x.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.x.AutoSize = true;
			this.x.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.x.Location = new System.Drawing.Point(403, 529);
			this.x.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.x.Name = "x";
			this.x.Size = new System.Drawing.Size(16, 20);
			this.x.TabIndex = 10;
			this.x.Text = "x";
			// 
			// btnSpam
			// 
			this.btnSpam.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSpam.Checked = false;
			this.btnSpam.Location = new System.Drawing.Point(425, 519);
			this.btnSpam.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btnSpam.Name = "btnSpam";
			this.btnSpam.Size = new System.Drawing.Size(116, 37);
			this.btnSpam.TabIndex = 17;
			this.btnSpam.Text = "Spam";
			this.btnSpam.Click += new System.EventHandler(this.btnSpam_Click);
			// 
			// btnSendOnce
			// 
			this.btnSendOnce.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSendOnce.Checked = false;
			this.btnSendOnce.Location = new System.Drawing.Point(543, 519);
			this.btnSendOnce.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btnSendOnce.Name = "btnSendOnce";
			this.btnSendOnce.Size = new System.Drawing.Size(116, 37);
			this.btnSendOnce.TabIndex = 18;
			this.btnSendOnce.Text = "Send Once";
			this.btnSendOnce.Click += new System.EventHandler(this.btnSendOnce_Click);
			// 
			// textToSend
			// 
			this.textToSend.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textToSend.Location = new System.Drawing.Point(18, 483);
			this.textToSend.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.textToSend.Multiline = true;
			this.textToSend.Name = "textToSend";
			this.textToSend.Size = new System.Drawing.Size(640, 33);
			this.textToSend.TabIndex = 31;
			this.textToSend.WordWrap = false;
			// 
			// numSpamTimes
			// 
			this.numSpamTimes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.numSpamTimes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
			this.numSpamTimes.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
			this.numSpamTimes.Location = new System.Drawing.Point(323, 524);
			this.numSpamTimes.LoopValues = false;
			this.numSpamTimes.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.numSpamTimes.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
			this.numSpamTimes.Name = "numSpamTimes";
			this.numSpamTimes.Size = new System.Drawing.Size(78, 28);
			this.numSpamTimes.TabIndex = 37;
			// 
			// numSpamDelay
			// 
			this.numSpamDelay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.numSpamDelay.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
			this.numSpamDelay.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
			this.numSpamDelay.Location = new System.Drawing.Point(18, 523);
			this.numSpamDelay.LoopValues = false;
			this.numSpamDelay.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.numSpamDelay.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
			this.numSpamDelay.Name = "numSpamDelay";
			this.numSpamDelay.Size = new System.Drawing.Size(96, 28);
			this.numSpamDelay.TabIndex = 38;
			this.numSpamDelay.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label1.AutoSize = true;
			this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.label1.Location = new System.Drawing.Point(117, 531);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(30, 20);
			this.label1.TabIndex = 39;
			this.label1.Text = "ms";
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label2.AutoSize = true;
			this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.label2.Location = new System.Drawing.Point(18, 457);
			this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(128, 20);
			this.label2.TabIndex = 40;
			this.label2.Text = "Packet spammer";
			// 
			// PacketLogger
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(676, 569);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.numSpamDelay);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Controls.Add(this.numSpamTimes);
			this.Controls.Add(this.txtPackets);
			this.Controls.Add(this.textToSend);
			this.Controls.Add(this.btnSendOnce);
			this.Controls.Add(this.x);
			this.Controls.Add(this.btnSpam);
			this.Icon = global::Properties.Resources.GrimoireIcon;
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.MinimizeBox = false;
			this.Name = "PacketLogger";
			this.Text = "Packet logger";
			this.TopMost = true;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PacketLogger_FormClosing);
			this.tableLayoutPanel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.numSpamTimes)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numSpamDelay)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        private async void btnSendOnce_Click(object sender, EventArgs e)
        {
            if (textToSend.TextLength > 0)
            {
                btnSendOnce.Enabled = false;
                await Proxy.Instance.SendToServer(textToSend.Text);
                btnSendOnce.Enabled = true;
            }
        }

		private bool usingSpammer = true;
        private async void btnSpam_Click(object sender, EventArgs e)
        {
            if (btnSpam.Text.Equals("Spam"))
			{
				if (textToSend.TextLength < 1) return;
				btnSpam.Text = "Stop";
                btnSendOnce.Enabled = false;
                List<string> listPackets = new List<string>();
                int spamTimes = Decimal.ToInt32(numSpamTimes.Value);
                int spamDelay = Decimal.ToInt32(numSpamDelay.Value);
                if (textToSend.TextLength > 0)
                {
                    listPackets.Add(textToSend.Text);
                    if (spamTimes > 0)
					{
						usingSpammer = false;
						int i = 1;
						while (i <= spamTimes && btnSpam.Text.Equals("Stop"))
						{
							await Proxy.Instance.SendToServer(textToSend.Text);
							await Task.Delay(spamDelay);
							i++;
						}
						btnSpam.Text = "Spam";
						btnSendOnce.Enabled = true;
					}
                    else
                    {
						usingSpammer = true;
						Spammer.Instance.Start(listPackets, spamDelay);
                    }
                }
            }
            else
			{
				btnSpam.Text = "Spam";
				btnSendOnce.Enabled = true;
				if (usingSpammer) Spammer.Instance.Stop();
			}
        }
	}
}