﻿using DienMayXanh_Store.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DienMayXanh_Store.Views.Dialogs
{
    public partial class addNewStaff : Form
    {
        private ContextDB context = Program.context;
        private Image img;
        private string selectedFile;
        public addNewStaff()
        {
            InitializeComponent();
            avatar.AllowDrop = true;
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void addNewStaff_Load(object sender, EventArgs e)
        {
            cb_Position.SelectedIndex = 0;
            btn_Female.Checked = true;
        }

        private void btn_addStaff_Click(object sender, EventArgs e)
        {
            if (tb_password.Text.Equals(tb_repassword.Text))
            {
                STAFF newStaff = new STAFF();
                newStaff.StaffID = "NV" + tb_StaffPhone.Text;
                newStaff.Name = tb_StaffName.Text;
                newStaff.Address = tb_StaffAddress.Text;
                newStaff.Phone = tb_StaffPhone.Text;
                newStaff.Gender = btn_Female.Checked ? true : false;
                newStaff.Position = cb_Position.Text;
                context.STAFFS.Add(newStaff);
                ACCOUNT newAccount = new ACCOUNT();
                newAccount.StaffID = newStaff.StaffID;
                newAccount.LoginName = tb_LoginName.Text;
                newAccount.Password = FormLogin.MD5Hash(FormLogin.Base64Encode(tb_password.Text));
                newAccount.Permission = cb_Position.Text;
                context.ACCOUNTS.Add(newAccount);

                context.SaveChanges();
                string FileExtension = System.IO.Path.GetExtension(selectedFile);
                string resultFileCopy = @"..\..\Images\" + newStaff.StaffID + FileExtension;
                System.IO.File.Copy(selectedFile, resultFileCopy);
                MessageBox.Show("Thêm Mới Thành Công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                FormMenu.instance.openChildForm(new formStaff());
                this.Dispose();
                return;


            }
            MessageBox.Show("Mật Khẩu Không Khớp!\nVui Lòng Nhập Lại", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
           
        }

        private void btn_addAvatar_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "Chọn Ảnh";
                dlg.Filter = "Image Files (*.bmp;*.jpg;*.jpeg,*.png)|*.BMP;*.JPG;*.JPEG;*.PNG";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    selectedFile = dlg.FileName;
                    avatar.ImageLocation = dlg.FileName;
                }    
            }
        }

        private void avatar_DragDrop(object sender, DragEventArgs e)
        {
            foreach (string pic in ((string[])e.Data.GetData(DataFormats.FileDrop)))
            {
                selectedFile = pic;
                img = Image.FromFile(pic);
                avatar.Image = img;
            }
        }

        private void avatar_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }
    }
}
