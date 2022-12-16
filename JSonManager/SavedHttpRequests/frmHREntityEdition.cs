using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JSonManager.SavedHttpRequests
{

    public enum EntityType
    {
        Project,
        Collection,
        Request,
        Variable,
        Value
    }

    public partial class frmHREntityEdition : Form
    {

        private const string HTTP_METHODS = "GET;PUT;POST";
        public ReturnedEntityValues Entity;

        public frmHREntityEdition(EntityType entityType)
        {
            InitializeComponent();
            Entity = new ReturnedEntityValues();
            EntitiesFormAdapter(entityType);
            LoadMethodsCombo();
        }

        private void LoadMethodsCombo()
        {
            var items = HTTP_METHODS.Split(';').Select(x => new ComboItem() { Name = x, Value = x }).ToList();

            cmbMethods.DataSource = items;
            cmbMethods.DisplayMember = "Name";
            cmbMethods.ValueMember = "Value";
        }

        private void EntitiesFormAdapter(EntityType entityType)
        {            
            switch(entityType)
            {
                case EntityType.Project:
                case EntityType.Collection:
                case EntityType.Variable:
                case EntityType.Value:
                    ShowNameAndDescriptionControls();
                    break;
                case EntityType.Request:
                    ShowRequestControls();
                    break;
            }        
        }

        private void ShowNameAndDescriptionControls()
        {
            lblDescription.Location = new Point(12, 9);
            txtDescription.Location = new Point(12, 36);
            txtDescription.Size = new Size(352, 222);
            RequestControlsVisible(false);
        }

        private void ShowRequestControls()
        {
            lblDescription.Location = new Point(12, 116);
            txtDescription.Location = new Point(12, 141);
            txtDescription.Size = new Size(352, 121);
            RequestControlsVisible(true);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            LoadReturnedEntity();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void LoadReturnedEntity()
        {
            Entity.Description = txtDescription.Text;
            Entity.Method = cmbMethods.Text;
            Entity.Url = txtUrl.Text;

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void RequestControlsVisible(bool visible)
        {
            lblMethod.Visible = visible;
            lblUrl.Visible = visible;
            txtUrl.Visible = visible;
            cmbMethods.Visible = visible;
        }
    }


    public class ReturnedEntityValues
    {
        public string Description { get; set; }
        public string Url { get; set; }
        public string Method { get; set; }
    }

    public class ComboItem
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
