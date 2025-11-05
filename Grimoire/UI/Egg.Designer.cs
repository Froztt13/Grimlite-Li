
namespace Grimoire.UI
{
	partial class Egg
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Egg));
			this.imgAstolfo = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.imgAstolfo)).BeginInit();
			this.SuspendLayout();
			// 
			// imgAstolfo
			// 
			this.imgAstolfo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.imgAstolfo.Image = ((System.Drawing.Image)(resources.GetObject("imgAstolfo.Image")));
			this.imgAstolfo.InitialImage = null;
			this.imgAstolfo.Location = new System.Drawing.Point(0, 0);
			this.imgAstolfo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.imgAstolfo.Name = "imgAstolfo";
			this.imgAstolfo.Size = new System.Drawing.Size(1924, 1078);
			this.imgAstolfo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.imgAstolfo.TabIndex = 172;
			this.imgAstolfo.TabStop = false;
			this.imgAstolfo.MouseClick += new System.Windows.Forms.MouseEventHandler(this.imgAstolfo_MouseClick);
			// 
			// Egg
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1924, 1078);
			this.ControlBox = false;
			this.Controls.Add(this.imgAstolfo);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.Name = "Egg";
			this.Text = "Heheh...";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			((System.ComponentModel.ISupportInitialize)(this.imgAstolfo)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox imgAstolfo;
	}
}