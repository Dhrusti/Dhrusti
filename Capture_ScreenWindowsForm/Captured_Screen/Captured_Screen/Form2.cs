using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Captured_Screen
{
	public partial class Form2 : Form
	{
		public Form2()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			//for (int i = 0; i < 5; i++)
			while (true) 
			{
				Thread.Sleep(2000);
				CaptureAtDefinedIntervals();
			}
		}

		public void CaptureAtDefinedIntervals()
		{
			Bitmap screenshot = new Bitmap(SystemInformation.VirtualScreen.Width,
										   SystemInformation.VirtualScreen.Height,
										   PixelFormat.Format32bppArgb);
			Graphics screenGraph = Graphics.FromImage(screenshot);
			screenGraph.CopyFromScreen(SystemInformation.VirtualScreen.X,
									   SystemInformation.VirtualScreen.Y,
									   0,
									   0,
									   SystemInformation.VirtualScreen.Size,
									   CopyPixelOperation.SourceCopy);

			screenshot.Save(DateTime.Now.ToString("ddMMyyyyhhmmss") + ".png", System.Drawing.Imaging.ImageFormat.Png);
			//screenshot.Save("Screenshot.png", System.Drawing.Imaging.ImageFormat.Png);
		}
	}
}
