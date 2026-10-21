using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BTL_LTTQ_2.GUI.Forms.DanhSachHangHoa;

namespace BTL_LTTQ_2.GUI.Forms
{
    public partial class frmDanhSachHangHoa : Form
    {
        public frmDanhSachHangHoa()
        {
            InitializeComponent();
        }

        private void btnHangHoa_Click(object sender, EventArgs e)
        {
            using (frmHangHoa frm = new frmHangHoa())
            {
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog(this);
            }
        }
    }
}
