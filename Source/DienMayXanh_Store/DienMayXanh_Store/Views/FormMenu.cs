using DienMayXanh_Store.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DienMayXanh_Store.Views
{
    public partial class FormMenu : Form
    {
        private ContextDB context = Program.context;
        private STAFF info = FormLogin.info;
        public FormMenu()
        {
            InitializeComponent();
        }

        private void FormMenu_Load(object sender, EventArgs e)
        {
            ptbAvatar.ImageLocation = string.Format(@"..\..\Images\" + info.StaffID + ".jpg");
            lblFullName.Text = info.Name;
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            btnHome.Checked = btnHome.Checked ? false : true;
        }
    }
}
