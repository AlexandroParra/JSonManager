using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JSonManager.SavedHttpRequests
{
    public partial class frmSavedHttpRequests : Form
    {

        public string HttpRequestSelected;

        private List<HRProject> _projects;

        private HRProject _currentProject;

        private HRCollection _currentCollection;

        private HRRequest _currentRequest;

        private XmlManager _xmlManager = new XmlManager(); 

        private string _locationProjectsFile = Environment.CurrentDirectory;

        public frmSavedHttpRequests()
        {
            InitializeComponent();
            LoadConfiguration();
            LoadDataForm();
        }

        private void LoadDataForm()
        {
            LoadHRProjects();
        }

        private void txtProjectFinder_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtProjectFinder.Text))
                ApplyHRProjectsListFilter(txtProjectFinder.Text);
            else
                ApplyHRProjectsListFilter(txtProjectFinder.Text);
        }

        private void ApplyHRProjectsListFilter(string conditions)
        {
         
        }

        private void LoadConfiguration()
        {
            _locationProjectsFile += ConfigurationManager.AppSettings.Get("LocationProjectsFile");

            lstHRProjects.ValueMember = "Name";
            lstHRProjects.DisplayMember = "Name";

            lstHRCollections.ValueMember = "Name";
            lstHRCollections.DisplayMember = "Name";

            lstHRRequests.ValueMember = "Url";
            lstHRRequests.DisplayMember = "Url";
        }

        private void LoadHRProjects()
        {
            _projects = _xmlManager.Deserialize<List<HRProject>>(_locationProjectsFile);
            if (_projects != null && _projects.Count > 0)
            {                
                lstHRProjects.DataSource = _projects;
            }
        }

        private void lstHRProjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstHRProjects.SelectedItems.Count > 0)
            {
                _currentProject = lstHRProjects.SelectedItem as HRProject;
                LoadHRCollections();
            }
        }

        private void lstHRCollections_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstHRCollections.SelectedItems.Count > 0)
            {
                _currentCollection = lstHRCollections.SelectedItem as HRCollection;
                LoadHRRequests();
            }
        }

        private void LoadHRCollections()
        {            
            lstHRCollections.DataSource = _currentProject.Collections;
        }

        private void LoadHRRequests()
        {
            lstHRRequests.DataSource = _currentCollection.Requests;
        }

        private void LoadHRVariableValue()
        {
            //foreach (DataRow row in table.Rows)
            //{
            //    var data = new Modelo { nombre = row["Nombre"].ToString(), apellido = row["Apellidos"].ToString(), sexo = row["Sexo"].ToString(), edad = row["Edad"].ToString() };
            //    dataGridVariables.Items.Add(data);
            //}
        }

        private void lstHRRequests_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstHRRequests.SelectedItems.Count > 0)
            {
                _currentRequest = lstHRRequests.SelectedItem as HRRequest;
                DecodeRequest();
            }
        }

        private void DecodeRequest()
        {
            string decodeReq = _currentRequest.Url;

            foreach(var v in _currentRequest.Variables)
                decodeReq = decodeReq.Replace(v.Name, v.Value);

            txtDecodedRequest.Text = decodeReq;
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            HttpRequestSelected = txtDecodedRequest.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnNewProject_Click(object sender, EventArgs e)
        {
            string name = txtProjectFinder.Text;

            var projectNames = _projects.Select(x => x.Name).ToList();

            if (name != String.Empty && !projectNames.Contains(name))
                _projects.Add(CreateNewProject(name));
        }

        public HRProject CreateNewProject(string Name)
        {
            HRProject hrProject = new HRProject();
            hrProject.Name = Name;

            frmHREntitiesEdition frmHREE = new frmHREntitiesEdition();
            frmHREE.Text = Name;
            
            var result = frmHREE.ShowDialog();
            if (result == DialogResult.OK)
                hrProject.Description = frmHREE.Description;

            return hrProject;
        }

        private void frmSavedHttpRequests_FormClosing(object sender, FormClosingEventArgs e)
        {
            _xmlManager.Serialize<List<HRProject>>(_projects,_locationProjectsFile);
        }
    }
}
