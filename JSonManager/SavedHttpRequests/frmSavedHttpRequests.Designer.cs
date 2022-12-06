namespace JSonManager.SavedHttpRequests
{
    partial class frmSavedHttpRequests
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
            this.lstHttpRequests = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // lstHttpRequests
            // 
            this.lstHttpRequests.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstHttpRequests.FormattingEnabled = true;
            this.lstHttpRequests.ItemHeight = 18;
            this.lstHttpRequests.Location = new System.Drawing.Point(319, 168);
            this.lstHttpRequests.Name = "lstHttpRequests";
            this.lstHttpRequests.Size = new System.Drawing.Size(665, 166);
            this.lstHttpRequests.TabIndex = 0;
            // 
            // frmSavedHttpRequests
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1461, 977);
            this.Controls.Add(this.lstHttpRequests);
            this.Name = "frmSavedHttpRequests";
            this.Text = "frmSavedHttpRequests";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstHttpRequests;
    }
}