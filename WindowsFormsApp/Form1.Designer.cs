
namespace WindowsFormsApp
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.webView2 = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.SuspendLayout();
            // 
            // webView2
            // 
            this.webView2.CreationProperties = null;
            this.webView2.Location = new System.Drawing.Point(0, 73);
            this.webView2.Name = "webView2";
            this.webView2.Size = new System.Drawing.Size(801, 380);
            this.webView2.Source = new System.Uri("https://www.microsoft.com", System.UriKind.Absolute);
            this.webView2.TabIndex = 0;
            this.webView2.Text = "webView2";
            this.webView2.UseWaitCursor = true;
            this.webView2.ZoomFactor = 1D;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.webView2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Web.WebView2.WinForms.WebView2 webView2;
    }
}

