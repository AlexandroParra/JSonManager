﻿namespace JSonManager.SavedHttpRequests
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lstHRProjects = new System.Windows.Forms.ListBox();
            this.txtProjectFinder = new System.Windows.Forms.TextBox();
            this.lstHRCollections = new System.Windows.Forms.ListBox();
            this.lstHRRequests = new System.Windows.Forms.ListBox();
            this.dataGridVariables = new System.Windows.Forms.DataGridView();
            this.lstHRVariables = new System.Windows.Forms.ListBox();
            this.txtDecodedRequest = new System.Windows.Forms.TextBox();
            this.btnSelect = new System.Windows.Forms.Button();
            this.btnNewProject = new System.Windows.Forms.Button();
            this.txtCollectionFinder = new System.Windows.Forms.TextBox();
            this.btnNewCollection = new System.Windows.Forms.Button();
            this.btnNewVariable = new System.Windows.Forms.Button();
            this.txtVariableFinder = new System.Windows.Forms.TextBox();
            this.btnNewHRequest = new System.Windows.Forms.Button();
            this.txtRequestFinder = new System.Windows.Forms.TextBox();
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
            this.txtProjectFinder.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProjectFinder.Location = new System.Drawing.Point(12, 11);
            this.txtProjectFinder.Name = "txtProjectFinder";
            this.txtProjectFinder.Size = new System.Drawing.Size(270, 25);
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
            this.lstHRRequests.Location = new System.Drawing.Point(12, 444);
            this.lstHRRequests.Name = "lstHRRequests";
            this.lstHRRequests.Size = new System.Drawing.Size(1343, 238);
            this.lstHRRequests.TabIndex = 3;
            this.lstHRRequests.SelectedIndexChanged += new System.EventHandler(this.lstHRRequests_SelectedIndexChanged);
            // 
            // dataGridVariables
            // 
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridVariables.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridVariables.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridVariables.GridColor = System.Drawing.SystemColors.ActiveCaption;
            this.dataGridVariables.Location = new System.Drawing.Point(1006, 12);
            this.dataGridVariables.Name = "dataGridVariables";
            this.dataGridVariables.Size = new System.Drawing.Size(349, 321);
            this.dataGridVariables.TabIndex = 4;
            this.dataGridVariables.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridVariables_CellValueChanged);
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
            this.lstHRVariables.SelectedIndexChanged += new System.EventHandler(this.lstHRVariables_SelectedIndexChanged);
            // 
            // txtDecodedRequest
            // 
            this.txtDecodedRequest.BackColor = System.Drawing.SystemColors.Info;
            this.txtDecodedRequest.Enabled = false;
            this.txtDecodedRequest.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDecodedRequest.Location = new System.Drawing.Point(12, 360);
            this.txtDecodedRequest.Name = "txtDecodedRequest";
            this.txtDecodedRequest.Size = new System.Drawing.Size(1253, 25);
            this.txtDecodedRequest.TabIndex = 6;
            // 
            // btnSelect
            // 
            this.btnSelect.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelect.Location = new System.Drawing.Point(1280, 359);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(75, 26);
            this.btnSelect.TabIndex = 7;
            this.btnSelect.Text = "Select";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnNewProject
            // 
            this.btnNewProject.Font = new System.Drawing.Font("Cooper Black", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNewProject.Location = new System.Drawing.Point(288, 10);
            this.btnNewProject.Name = "btnNewProject";
            this.btnNewProject.Size = new System.Drawing.Size(37, 26);
            this.btnNewProject.TabIndex = 8;
            this.btnNewProject.Text = "+";
            this.btnNewProject.UseVisualStyleBackColor = true;
            this.btnNewProject.Click += new System.EventHandler(this.btnNewProject_Click);
            // 
            // txtCollectionFinder
            // 
            this.txtCollectionFinder.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCollectionFinder.Location = new System.Drawing.Point(342, 11);
            this.txtCollectionFinder.Name = "txtCollectionFinder";
            this.txtCollectionFinder.Size = new System.Drawing.Size(270, 25);
            this.txtCollectionFinder.TabIndex = 9;
            // 
            // btnNewCollection
            // 
            this.btnNewCollection.Font = new System.Drawing.Font("Cooper Black", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNewCollection.Location = new System.Drawing.Point(618, 10);
            this.btnNewCollection.Name = "btnNewCollection";
            this.btnNewCollection.Size = new System.Drawing.Size(37, 26);
            this.btnNewCollection.TabIndex = 10;
            this.btnNewCollection.Text = "+";
            this.btnNewCollection.UseVisualStyleBackColor = true;
            this.btnNewCollection.Click += new System.EventHandler(this.btnNewCollection_Click);
            // 
            // btnNewVariable
            // 
            this.btnNewVariable.Font = new System.Drawing.Font("Cooper Black", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNewVariable.Location = new System.Drawing.Point(949, 10);
            this.btnNewVariable.Name = "btnNewVariable";
            this.btnNewVariable.Size = new System.Drawing.Size(37, 26);
            this.btnNewVariable.TabIndex = 11;
            this.btnNewVariable.Text = "+";
            this.btnNewVariable.UseVisualStyleBackColor = true;
            this.btnNewVariable.Click += new System.EventHandler(this.btnNewVariable_Click);
            // 
            // txtVariableFinder
            // 
            this.txtVariableFinder.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVariableFinder.Location = new System.Drawing.Point(673, 11);
            this.txtVariableFinder.Name = "txtVariableFinder";
            this.txtVariableFinder.Size = new System.Drawing.Size(270, 25);
            this.txtVariableFinder.TabIndex = 12;
            // 
            // btnNewHRequest
            // 
            this.btnNewHRequest.Font = new System.Drawing.Font("Cooper Black", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNewHRequest.Location = new System.Drawing.Point(1280, 410);
            this.btnNewHRequest.Name = "btnNewHRequest";
            this.btnNewHRequest.Size = new System.Drawing.Size(37, 26);
            this.btnNewHRequest.TabIndex = 13;
            this.btnNewHRequest.Text = "+";
            this.btnNewHRequest.UseVisualStyleBackColor = true;
            this.btnNewHRequest.Click += new System.EventHandler(this.btnNewHRequest_Click);
            // 
            // txtRequestFinder
            // 
            this.txtRequestFinder.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRequestFinder.Location = new System.Drawing.Point(12, 412);
            this.txtRequestFinder.Name = "txtRequestFinder";
            this.txtRequestFinder.Size = new System.Drawing.Size(1253, 25);
            this.txtRequestFinder.TabIndex = 14;
            // 
            // frmSavedHttpRequests
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1372, 692);
            this.Controls.Add(this.txtRequestFinder);
            this.Controls.Add(this.btnNewHRequest);
            this.Controls.Add(this.txtVariableFinder);
            this.Controls.Add(this.btnNewVariable);
            this.Controls.Add(this.btnNewCollection);
            this.Controls.Add(this.txtCollectionFinder);
            this.Controls.Add(this.btnNewProject);
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
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSavedHttpRequests_FormClosing);
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
        private System.Windows.Forms.ListBox lstHRVariables;
        private System.Windows.Forms.TextBox txtDecodedRequest;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Button btnNewProject;
        private System.Windows.Forms.TextBox txtCollectionFinder;
        private System.Windows.Forms.Button btnNewCollection;
        private System.Windows.Forms.Button btnNewVariable;
        private System.Windows.Forms.TextBox txtVariableFinder;
        private System.Windows.Forms.Button btnNewHRequest;
        private System.Windows.Forms.TextBox txtRequestFinder;
    }
}