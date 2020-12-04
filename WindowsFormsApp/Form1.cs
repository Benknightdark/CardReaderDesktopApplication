using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //this.TopMost = true;
            //this.FormBorderStyle = FormBorderStyle.None;
            Task.Run(() =>
            {
                Program.CreateHostBuilder().Build().Run();
            });
            this.WindowState = FormWindowState.Maximized;
            this.webView2.Source = new System.Uri("http://localhost:4000", System.UriKind.Absolute);
            this.webView2.Height = this.Height-20;
            this.webView2.Width = this.Width-20;

            
        }

   
    }
}
