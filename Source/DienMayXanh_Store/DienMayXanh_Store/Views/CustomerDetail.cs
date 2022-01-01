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

namespace DienMayXanh_Store.Views
{
    public partial class CustomerDetail : Form
    {
        private ContextDB context = Program.context;
        private object _customer;
        public CustomerDetail()
        {
            InitializeComponent();
        }
        public CustomerDetail(object customer)
        {
            InitializeComponent();
            _customer = customer;
        }
        private void closePanel(object sender, EventArgs e)
        {
            FormMenu.instance.openChildForm(new FormCustomer());
            this.Dispose();
        }



        private void CustomerDetail_Load(object sender, EventArgs e)

        {
            dgv_ListInvoice.Columns["CreateAt"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
            FromDate.CustomFormat = "dd/MM/yyyy";
            ToDate.CustomFormat = "dd/MM/yyyy";

            System.Reflection.PropertyInfo _ID = _customer.GetType().GetProperty("_ID");
            string _customerID = (String)_ID.GetValue(_customer, null);
            lb_CustomerID.Text = _customerID;
            System.Reflection.PropertyInfo _Name = _customer.GetType().GetProperty("_Name");
            lb_CustomerName.Text = (String)_Name.GetValue(_customer, null);
            System.Reflection.PropertyInfo _Phone = _customer.GetType().GetProperty("_Phone");
            lb_CustomerPhone.Text = (String)_Phone.GetValue(_customer, null);
            System.Reflection.PropertyInfo _Address = _customer.GetType().GetProperty("_Address");
            lb_CusotmerAddress.Text = (String)_Address.GetValue(_customer, null);

            var Orders = context.RECIEPTS
                .Where(item => item.CustomerID == _customerID)
                .AsEnumerable()
                .Select((item, index) => new 
                {
                    No = ++index,
                    item.RecieptID,
                    item.STAFF.Name,
                    item.CreateAt,
                    item.PaymentMethod,
                    item.Total
                }).ToList();
            dgv_ListInvoice.DataSource = Orders;

            int totalQuantity = context.RECIEPTS
                .Where(item => item.CustomerID.Equals(_customerID))
                .Count();
            decimal totalPayment = context.RECIEPTS
                .Where(item => item.CustomerID.Equals(_customerID))
                .Sum(item => item.Total);

            lb_totalPrice.Text = totalPayment.ToString();
            lb_totalQuantity.Text = totalQuantity.ToString();


        }

        private void btn_invoiceDetail_Click(object sender, DataGridViewCellEventArgs e)
        {
            var dataGridView = (DataGridView)sender;
            if (dataGridView.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
               Dialogs.InvoiceDetail invoiceDetail = new Dialogs.InvoiceDetail();
                invoiceDetail.ShowDialog();
            }
        }
    }
}
