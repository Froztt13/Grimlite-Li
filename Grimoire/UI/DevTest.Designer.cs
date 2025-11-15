
namespace Grimoire.UI
{
	partial class DevTest
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DevTest));
			this.btnCall = new DarkUI.Controls.DarkButton();
			this.darkLabel6 = new DarkUI.Controls.DarkLabel();
			this.tbGameFunction = new DarkUI.Controls.DarkTextBox();
			this.tbArgs = new DarkUI.Controls.DarkTextBox();
			this.darkLabel1 = new DarkUI.Controls.DarkLabel();
			this.chkArgs = new DarkUI.Controls.DarkCheckBox();
			this.tbLogs = new DarkUI.Controls.DarkTextBox();
			this.radGrimoire = new System.Windows.Forms.RadioButton();
			this.radGame = new System.Windows.Forms.RadioButton();
			this.radGetGame = new System.Windows.Forms.RadioButton();
			this.btnCopy = new DarkUI.Controls.DarkButton();
			this.btnFormat = new DarkUI.Controls.DarkButton();
			this.SuspendLayout();
			// 
			// btnCall
			// 
			this.btnCall.Checked = false;
			this.btnCall.Location = new System.Drawing.Point(10, 178);
			this.btnCall.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btnCall.Name = "btnCall";
			this.btnCall.Size = new System.Drawing.Size(352, 35);
			this.btnCall.TabIndex = 169;
			this.btnCall.Text = "Call";
			this.btnCall.Click += new System.EventHandler(this.btnCall_Click_1);
			// 
			// darkLabel6
			// 
			this.darkLabel6.AutoSize = true;
			this.darkLabel6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.darkLabel6.Location = new System.Drawing.Point(6, 45);
			this.darkLabel6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.darkLabel6.Name = "darkLabel6";
			this.darkLabel6.Size = new System.Drawing.Size(71, 20);
			this.darkLabel6.TabIndex = 170;
			this.darkLabel6.Text = "Function";
			// 
			// tbGameFunction
			// 
			this.tbGameFunction.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbGameFunction.Location = new System.Drawing.Point(10, 72);
			this.tbGameFunction.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.tbGameFunction.Multiline = true;
			this.tbGameFunction.Name = "tbGameFunction";
			this.tbGameFunction.Size = new System.Drawing.Size(352, 33);
			this.tbGameFunction.TabIndex = 171;
			this.tbGameFunction.Text = "world";
			this.tbGameFunction.WordWrap = false;
			// 
			// tbArgs
			// 
			this.tbArgs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbArgs.Location = new System.Drawing.Point(10, 132);
			this.tbArgs.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.tbArgs.Multiline = true;
			this.tbArgs.Name = "tbArgs";
			this.tbArgs.Size = new System.Drawing.Size(320, 33);
			this.tbArgs.TabIndex = 173;
			this.tbArgs.Text = "arg1,arg2...";
			this.tbArgs.WordWrap = false;
			// 
			// darkLabel1
			// 
			this.darkLabel1.AutoSize = true;
			this.darkLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.darkLabel1.Location = new System.Drawing.Point(6, 108);
			this.darkLabel1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.darkLabel1.Name = "darkLabel1";
			this.darkLabel1.Size = new System.Drawing.Size(42, 20);
			this.darkLabel1.TabIndex = 172;
			this.darkLabel1.Text = "Args";
			// 
			// chkArgs
			// 
			this.chkArgs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chkArgs.AutoSize = true;
			this.chkArgs.Checked = true;
			this.chkArgs.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkArgs.Location = new System.Drawing.Point(340, 139);
			this.chkArgs.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.chkArgs.Name = "chkArgs";
			this.chkArgs.Size = new System.Drawing.Size(22, 21);
			this.chkArgs.TabIndex = 174;
			this.chkArgs.CheckedChanged += new System.EventHandler(this.chkArgs_CheckedChanged);
			// 
			// tbLogs
			// 
			this.tbLogs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbLogs.Location = new System.Drawing.Point(10, 223);
			this.tbLogs.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.tbLogs.Multiline = true;
			this.tbLogs.Name = "tbLogs";
			this.tbLogs.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.tbLogs.Size = new System.Drawing.Size(352, 273);
			this.tbLogs.TabIndex = 175;
			this.tbLogs.WordWrap = false;
			// 
			// radGrimoire
			// 
			this.radGrimoire.AutoSize = true;
			this.radGrimoire.Checked = true;
			this.radGrimoire.ForeColor = System.Drawing.Color.Gainsboro;
			this.radGrimoire.Location = new System.Drawing.Point(9, 9);
			this.radGrimoire.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.radGrimoire.Name = "radGrimoire";
			this.radGrimoire.Size = new System.Drawing.Size(94, 24);
			this.radGrimoire.TabIndex = 177;
			this.radGrimoire.TabStop = true;
			this.radGrimoire.Text = "Grimoire";
			this.radGrimoire.UseVisualStyleBackColor = true;
			// 
			// radGame
			// 
			this.radGame.AutoSize = true;
			this.radGame.ForeColor = System.Drawing.Color.Gainsboro;
			this.radGame.Location = new System.Drawing.Point(112, 9);
			this.radGame.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.radGame.Name = "radGame";
			this.radGame.Size = new System.Drawing.Size(100, 24);
			this.radGame.TabIndex = 178;
			this.radGame.Text = "Call Func";
			this.radGame.UseVisualStyleBackColor = true;
			// 
			// radGetGame
			// 
			this.radGetGame.AutoSize = true;
			this.radGetGame.ForeColor = System.Drawing.Color.Gainsboro;
			this.radGetGame.Location = new System.Drawing.Point(222, 9);
			this.radGetGame.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.radGetGame.Name = "radGetGame";
			this.radGetGame.Size = new System.Drawing.Size(140, 24);
			this.radGetGame.TabIndex = 179;
			this.radGetGame.Text = "Get/Set Object";
			this.radGetGame.UseVisualStyleBackColor = true;
			this.radGetGame.CheckedChanged += new System.EventHandler(this.radGetGame_CheckedChanged);
			// 
			// btnCopy
			// 
			this.btnCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnCopy.Checked = false;
			this.btnCopy.Location = new System.Drawing.Point(189, 509);
			this.btnCopy.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btnCopy.Name = "btnCopy";
			this.btnCopy.Size = new System.Drawing.Size(174, 35);
			this.btnCopy.TabIndex = 180;
			this.btnCopy.Text = "Copy";
			this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
			// 
			// btnFormat
			// 
			this.btnFormat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnFormat.Checked = false;
			this.btnFormat.Location = new System.Drawing.Point(10, 509);
			this.btnFormat.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btnFormat.Name = "btnFormat";
			this.btnFormat.Size = new System.Drawing.Size(174, 35);
			this.btnFormat.TabIndex = 181;
			this.btnFormat.Text = "Format JSON";
			this.btnFormat.Click += new System.EventHandler(this.btnFormat_Click);
			// 
			// DevTest
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(381, 560);
			this.Controls.Add(this.btnFormat);
			this.Controls.Add(this.btnCopy);
			this.Controls.Add(this.radGetGame);
			this.Controls.Add(this.radGame);
			this.Controls.Add(this.radGrimoire);
			this.Controls.Add(this.tbLogs);
			this.Controls.Add(this.chkArgs);
			this.Controls.Add(this.tbArgs);
			this.Controls.Add(this.darkLabel1);
			this.Controls.Add(this.tbGameFunction);
			this.Controls.Add(this.darkLabel6);
			this.Controls.Add(this.btnCall);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.Name = "DevTest";
			this.Text = "Dev Test";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DevTest_FormClosing);
			this.Load += new System.EventHandler(this.DevTest_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DarkUI.Controls.DarkButton btnCall;
		private DarkUI.Controls.DarkLabel darkLabel6;
		private DarkUI.Controls.DarkTextBox tbGameFunction;
		private DarkUI.Controls.DarkTextBox tbArgs;
		private DarkUI.Controls.DarkLabel darkLabel1;
		public DarkUI.Controls.DarkCheckBox chkArgs;
		private DarkUI.Controls.DarkTextBox tbLogs;
		private System.Windows.Forms.RadioButton radGrimoire;
		private System.Windows.Forms.RadioButton radGame;
		private System.Windows.Forms.RadioButton radGetGame;
		private DarkUI.Controls.DarkButton btnCopy;
		private DarkUI.Controls.DarkButton btnFormat;
	}
}