namespace Capture_SelectedScreenCopy
{
	partial class frmCapture
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
			this.picCapture = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.picCapture)).BeginInit();
			this.SuspendLayout();
			// 
			// picCapture
			// 
			this.picCapture.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.picCapture.Location = new System.Drawing.Point(-1, 0);
			this.picCapture.Name = "picCapture";
			this.picCapture.Size = new System.Drawing.Size(100, 100);
			this.picCapture.TabIndex = 0;
			this.picCapture.TabStop = false;
			this.picCapture.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picCapture_MouseDown);
			this.picCapture.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picCapture_MouseMove);
			this.picCapture.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picCapture_MouseUp);
			// 
			// frmCapture
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.picCapture);
			this.Name = "frmCapture";
			this.Text = "frmCapture";
			this.Load += new System.EventHandler(this.frmCapture_Load);
			((System.ComponentModel.ISupportInitialize)(this.picCapture)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private PictureBox picCapture;
	}
}