using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Capture_SelectedScreenCopy
{
	public partial class frmMain : Form
	{
		frmCapture frmCapture = new frmCapture();

		public frmMain()
		{
			InitializeComponent();
		}

		private void picScreenshot_Click(object sender, EventArgs e)
		{
			this.Hide();
			Thread.Sleep(200);
			frmCapture.ShowDialog();
			Image tempImage = Clipboard.GetImage();
			picScreenshot.Image = tempImage;
			this.Show();

		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.Filter = "Jpeg File|*.jpg";
			saveFileDialog.FileName = "Untitled";

			if (saveFileDialog.ShowDialog() == DialogResult.Cancel)
				return;
			try
			{
				if (picScreenshot.Image != null)
				{
					picScreenshot.Image.Save(saveFileDialog.FileName, ImageFormat.Jpeg);

					MessageBox.Show(this, $"Saved Screenshot to {saveFileDialog.FileName}", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
	}
}
