using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Capture_SelectedScreenCopy
{
	public partial class frmCapture : Form
	{

		int selectX;
		int selectY;
		int selectWidth;
		int selectHeight;
		public Pen selectPen;
		public Bitmap ScreenShot;

		bool start = false;

		public frmCapture()
		{
			InitializeComponent();
			//form properties
			this.BackColor = Color.Black; this.FormBorderStyle = FormBorderStyle.None;
			this.WindowState = FormWindowState.Maximized;
			this.StartPosition = FormStartPosition.Manual;
			this.Top = 0;
			this.Left = 0;

		}

		private void frmCapture_Load(object sender, EventArgs e)
		{
			//Hide the form
			this.Hide();

			//Create the Bitmap
			Bitmap printscreen = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);

			//Create the graphic variable with screen Dimensions
			Graphics graphics = Graphics.FromImage(printscreen as Image);

			//Copy Image from the screen
			graphics.CopyFromScreen(0, 0, 0, 0, printscreen.Size);

			//Create a temporal memory stream for the image
			using (MemoryStream s = new MemoryStream())
			{
				//save graphic variable into memory
				printscreen.Save(s, ImageFormat.Bmp);
				picCapture.Size = new System.Drawing.Size(this.Width, this.Height);

				//set the picture box with temporary stream
				picCapture.Image = Image.FromStream(s);
			}

			//Show Form
			this.Show();

			//Cross Cursor
			Cursor = Cursors.Cross;
		}

		private void picCapture_MouseDown(object sender, MouseEventArgs e)
		{
			if (!start)
			{
				if (e.Button == System.Windows.Forms.MouseButtons.Left)
				{
					//starts coordinates for rectangle
					selectX = e.X;
					selectY = e.Y;
					selectPen = new Pen(Color.PeachPuff, 5);
					selectPen.DashStyle = DashStyle.DashDotDot;
				}

				//refresh picture box
				picCapture.Refresh();

				//start control vriable for draw rectangle
				start = true;


			}
		}

		private void picCapture_MouseMove(object sender, MouseEventArgs e)
		{
			//validate if there is an image 
			if (picCapture.Image == null)
				return;

			//validate if right-click was trigger
			if (start)
			{
				//refresh picture box
				picCapture.Refresh();

				//set corner square to mouse coordinates
				selectWidth = e.X - selectX;
				selectHeight = e.Y - selectY;

				//draw dotted rectangle
				picCapture.CreateGraphics().SmoothingMode= SmoothingMode.AntiAlias;
				picCapture.CreateGraphics().DrawRectangle(selectPen, selectX, selectY, selectWidth, selectHeight);
			}
		}

		private void picCapture_MouseUp(object sender, MouseEventArgs e)
		{
			//validate if there is an image 
			if(picCapture.Image == null) return;

			//same functionality when mouse if over
			if(e.Button == System.Windows.Forms.MouseButtons.Left)
			{
				picCapture.Refresh();
				selectWidth = e.X - selectX;
				selectHeight = e.Y - selectY;

				picCapture.CreateGraphics().DrawRectangle(selectPen, selectX, selectY, selectWidth, selectHeight);
			}
			start = false;
			//function save image to clipboard
			SaveToClipboard();
		}

		private void SaveToClipboard()
		{
			//validate if something selected
			if(selectWidth > 0)
			{
				Rectangle rect = new Rectangle(selectX, selectY, selectWidth, selectHeight);

				//create bitmap with original dimensions
				Bitmap OriginalImage = new Bitmap(picCapture.Image, picCapture.Width, picCapture.Height);

				//create bitmap with selected dimensions
				Bitmap _img = new Bitmap(selectWidth, selectHeight);

				//create graphic variable
				Graphics g = Graphics.FromImage(_img);

				//set graphic attribute
				g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
				g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
				g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
				g.DrawImage(OriginalImage,0, 0, rect, GraphicsUnit.Pixel);

				//insert image stream into clipboard
				Clipboard.SetImage(_img);
			}
			this.Close();
		}
	}
}
