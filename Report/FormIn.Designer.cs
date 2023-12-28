namespace QLSieuThiBHX.GUI
{
    partial class FormIn
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.inNV = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // inNV
            // 
            this.inNV.ActiveViewIndex = -1;
            this.inNV.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inNV.Cursor = System.Windows.Forms.Cursors.Default;
            this.inNV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inNV.Location = new System.Drawing.Point(0, 0);
            this.inNV.Name = "inNV";
            this.inNV.Size = new System.Drawing.Size(1297, 751);
            this.inNV.TabIndex = 0;
            // 
            // FormInNV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1297, 751);
            this.Controls.Add(this.inNV);
            this.Name = "FormInNV";
            this.Text = "Prints";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        public CrystalDecisions.Windows.Forms.CrystalReportViewer inNV;
    }
}