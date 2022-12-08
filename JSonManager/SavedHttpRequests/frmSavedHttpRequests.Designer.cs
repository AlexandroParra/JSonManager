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
            this.lstHRProjects = new System.Windows.Forms.ListBox();
            this.txtProjectFinder = new System.Windows.Forms.TextBox();
            this.lstHRCollections = new System.Windows.Forms.ListBox();
            this.lstHRRequests = new System.Windows.Forms.ListBox();
            this.dataGridVariables = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lstHRVariables = new System.Windows.Forms.ListBox();
            this.txtDecodedRequest = new System.Windows.Forms.TextBox();
            this.btnSelect = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridVariables)).BeginInit();
            this.SuspendLayout();
            // 
            // lstHRProjects
            // 
            this.lstHRProjects.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstHRProjects.FormattingEnabled = true;
            this.lstHRProjects.ItemHeight = 18;
            this.lstHRProjects.Location = new System.Drawing.Point(12, 41);
            this.lstHRProjects.Name = "lstHRProjects";
            this.lstHRProjects.Size = new System.Drawing.Size(313, 292);
            this.lstHRProjects.TabIndex = 0;
            this.lstHRProjects.SelectedIndexChanged += new System.EventHandler(this.lstHRProjects_SelectedIndexChanged);
            // 
            // txtProjectFinder
            // 
            this.txtProjectFinder.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProjectFinder.Location = new System.Drawing.Point(12, 11);
            this.txtProjectFinder.Name = "txtProjectFinder";
            this.txtProjectFinder.Size = new System.Drawing.Size(313, 24);
            this.txtProjectFinder.TabIndex = 1;
            this.txtProjectFinder.TextChanged += new System.EventHandler(this.txtProjectFinder_TextChanged);
            // 
            // lstHRCollections
            // 
            this.lstHRCollections.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstHRCollections.FormattingEnabled = true;
            this.lstHRCollections.ItemHeight = 18;
            this.lstHRCollections.Location = new System.Drawing.Point(342, 41);
            this.lstHRCollections.Name = "lstHRCollections";
            this.lstHRCollections.Size = new System.Drawing.Size(313, 292);
            this.lstHRCollections.TabIndex = 2;
            this.lstHRCollections.SelectedIndexChanged += new System.EventHandler(this.lstHRCollections_SelectedIndexChanged);
            // 
            // lstHRRequests
            // 
            this.lstHRRequests.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstHRRequests.FormattingEnabled = true;
            this.lstHRRequests.ItemHeight = 18;
            this.lstHRRequests.Location = new System.Drawing.Point(12, 411);
            this.lstHRRequests.Name = "lstHRRequests";
            this.lstHRRequests.Size = new System.Drawing.Size(1437, 238);
            this.lstHRRequests.TabIndex = 3;
            this.lstHRRequests.SelectedIndexChanged += new System.EventHandler(this.lstHRRequests_SelectedIndexChanged);
            // 
            // dataGridVariables
            // 
            this.dataGridVariables.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridVariables.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            this.dataGridVariables.Location = new System.Drawing.Point(1043, 41);
            this.dataGridVariables.Name = "dataGridVariables";
            this.dataGridVariables.Size = new System.Drawing.Size(406, 292);
            this.dataGridVariables.TabIndex = 4;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Variable";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Valor";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Actual";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // lstHRVariables
            // 
            this.lstHRVariables.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstHRVariables.FormattingEnabled = true;
            this.lstHRVariables.ItemHeight = 18;
            this.lstHRVariables.Location = new System.Drawing.Point(673, 41);
            this.lstHRVariables.Name = "lstHRVariables";
            this.lstHRVariables.Size = new System.Drawing.Size(313, 292);
            this.lstHRVariables.TabIndex = 5;
            // 
            // txtDecodedRequest
            // 
            this.txtDecodedRequest.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDecodedRequest.Location = new System.Drawing.Point(12, 360);
            this.txtDecodedRequest.Name = "txtDecodedRequest";
            this.txtDecodedRequest.Size = new System.Drawing.Size(1347, 25);
            this.txtDecodedRequest.TabIndex = 6;
            // 
            // btnSelect
            // 
            this.btnSelect.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelect.Location = new System.Drawing.Point(1373, 360);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(75, 26);
            this.btnSelect.TabIndex = 7;
            this.btnSelect.Text = "Select";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // frmSavedHttpRequests
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1461, 661);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.txtDecodedRequest);
            this.Controls.Add(this.lstHRVariables);
            this.Controls.Add(this.dataGridVariables);
            this.Controls.Add(this.lstHRRequests);
            this.Controls.Add(this.lstHRCollections);
            this.Controls.Add(this.txtProjectFinder);
            this.Controls.Add(this.lstHRProjects);
            this.Name = "frmSavedHttpRequests";
            this.Text = "HTTP Requests manager";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridVariables)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstHRProjects;
        private System.Windows.Forms.TextBox txtProjectFinder;
        private System.Windows.Forms.ListBox lstHRCollections;
        private System.Windows.Forms.ListBox lstHRRequests;
        private System.Windows.Forms.DataGridView dataGridVariables;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.ListBox lstHRVariables;
        private System.Windows.Forms.TextBox txtDecodedRequest;
        private System.Windows.Forms.Button btnSelect;
    }
}