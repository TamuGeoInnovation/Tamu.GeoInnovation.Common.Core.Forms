using System.Drawing;
using System.Windows.Forms;

namespace USC.GISResearchLab.Common.Utils.Images
{
	/// <summary>
	/// Summary description for ImageUtils.
	/// </summary>
	public class ImageUtils
	{
		public ImageUtils()
		{
		}

		public static Image getImage(int index, ImageList imageList)
		{
			return imageList.Images[index];
		}
	}
}
