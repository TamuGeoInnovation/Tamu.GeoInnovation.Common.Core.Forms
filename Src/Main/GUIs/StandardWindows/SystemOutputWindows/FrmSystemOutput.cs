using System.Windows.Forms;

namespace USC.GISResearchLab.Common.Core.GUIs.StandardWindows.SystemOutputWindows
{
    public partial class FrmSystemOutput : Form
    {
        public FrmSystemOutput()
        {
            InitializeComponent();
        }

        private void FrmSystemOutput_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }
    }
}
