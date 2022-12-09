using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JSonManager.SavedHttpRequests
{
    public partial class frmHREntitiesEdition : Form
    {
        public string Description;
        public frmHREntitiesEdition()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Description = txtDescription.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
