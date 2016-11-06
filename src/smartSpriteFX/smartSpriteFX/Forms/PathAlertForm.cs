using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace smartSuite.smartSpriteFX.Forms
{
    public partial class PathAlertForm : Form
    {
        public PathAlertForm()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void Open(string title, string path)
        {
            this.lblLabel.Text = title;
            this.link.Text = path;
            this.link.Links.Add(0, path.Length, path);
            this.ShowDialog();
        }

        private void link_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Link.LinkData.ToString());
        }

        private void PathAlertForm_Load(object sender, EventArgs e)
        {
            this.Focus();
        }
    }
}
