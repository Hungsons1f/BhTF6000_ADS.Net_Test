using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using TwinCAT.Ads;

namespace ADS_Form
{
    public partial class Form1 : Form
    {
        private int hValue;
        private TcAdsClient client;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            client = new TcAdsClient();
            client.Connect(851);

            try
            {
                hValue = client.CreateVariableHandle("MAIN.value");
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);             
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                client.DeleteVariableHandle(hValue);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
            client.Dispose();
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            try
            {
                tbRead.Text = ((uint)client.ReadAny(hValue, typeof(uint))).ToString();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            tbRead.Text = null;
        }

        private void btnWrite_Click(object sender, EventArgs e)
        {
            try
            {
                client.WriteAny(hValue, Convert.ToUInt16(tbWrite.Text));
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
    }
}
