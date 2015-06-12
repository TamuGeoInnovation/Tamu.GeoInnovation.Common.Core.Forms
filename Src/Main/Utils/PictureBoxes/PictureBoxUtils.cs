using System.Collections;
using System.Windows.Forms;

namespace USC.GISResearchLab.Common.Forms.Utils.PictureBoxes
{
	/// <summary>
	/// Summary description for PictureBoxUtils.
	/// </summary>
	public class PictureBoxUtils
	{
		public PictureBoxUtils()
		{
		}

		public static PictureBox getPictureBox(int index, ArrayList pictureBoxList)
		{
			PictureBox ret = new PictureBox();
			ret.Image = ((PictureBox) pictureBoxList[index]).Image;
			ret.Size = ((PictureBox) pictureBoxList[index]).Size;
			return ret;
		}
	}
}
