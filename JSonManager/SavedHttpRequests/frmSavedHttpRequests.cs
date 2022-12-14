using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
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

        private HRVariable _currentVariable;

        private XmlManager _xmlManager = new XmlManager();

        private string _locationProjectsFile; 

        public frmSavedHttpRequests()
        {
            InitializeComponent();
            LoadConfiguration();
            //SaveProjects(XmlManager.CreateTestProject(), _locationProjectsFile);
            LoadDataForm();
        }

        private void LoadDataForm()
        {
            LoadHRProjectsFromFile();
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
            var nombreFichero = ConfigurationManager.AppSettings.Get("LocationProjectsFile");
            _locationProjectsFile = Environment.CurrentDirectory;

            _locationProjectsFile += nombreFichero;

            lstHRProjects.ValueMember = "Name";
            lstHRProjects.DisplayMember = "Name";

            lstHRCollections.ValueMember = "Name";
            lstHRCollections.DisplayMember = "Name";

            lstHRRequests.ValueMember = "Url";
            lstHRRequests.DisplayMember = "Url";

            lstHRVariables.ValueMember = "Name";
            lstHRVariables.DisplayMember = "Name";
        }

        private void LoadHRProjectsFromFile()
        {
            _projects = _xmlManager.Deserialize<List<HRProject>>(_locationProjectsFile);
            if (_projects != null && _projects.Count > 0)
            {
                LoadHRProjects();
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

        private void LoadHRProjects()
        {
            lstHRProjects.DataSource = _projects;
            lstHRProjects.Refresh();
        }

        private void LoadHRCollections()
        {            
            lstHRCollections.DataSource = _currentProject.Collections;
            lstHRCollections.Refresh();
        }

        private void LoadHRRequests()
        {
            lstHRRequests.DataSource = _currentCollection.Requests;
            lstHRRequests.Refresh();
        }

        private void LoadHRVariables()
        {
            lstHRVariables.DataSource = _currentRequest.Variables;
            lstHRVariables.Refresh();
        }

        private void LoadHRVariableValue()
        {
            dataGridVariables.DataSource = _currentVariable._values;
        }

        private void lstHRRequests_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstHRRequests.SelectedItems.Count > 0)
            {
                _currentRequest = lstHRRequests.SelectedItem as HRRequest;
                LoadHRVariables();
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



        private void frmSavedHttpRequests_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveProjects(_projects,_locationProjectsFile);
        }



        private void btnNewProject_Click(object sender, EventArgs e)
        {
            string name = txtProjectFinder.Text;

            var projectNames = _projects.Select(x => x.Name).ToList();

            if (name != String.Empty && !projectNames.Contains(name))
            {
                var newProject = CreateNewProject(name);

                if (newProject != null)
                    AddNewProjectToListOfProjects(newProject);
            }                
        }

        private void AddNewProjectToListOfProjects(HRProject newProject)
        {
            _projects.Add(newProject);
            LoadHRProjects();
        }



        private void SaveProjects(List<HRProject> projects, string file)
        {
            _xmlManager.Serialize<List<HRProject>>(_projects, file);
        }

        private void btnNewCollection_Click(object sender, EventArgs e)
        {
            string name = txtCollectionFinder.Text;

            var collectionNames = _currentProject.Collections.Select(x => x.Name).ToList();

            if (name != String.Empty && !collectionNames.Contains(name))
            {
                var newCollection = CreateNewCollection(name);

                if (newCollection != null)
                {
                    AddNewCollectionToCurrentProject(newCollection);
                }
            }
        }

        private void AddNewCollectionToCurrentProject(HRCollection newCollection)
        {
            _currentProject.Collections.Add(newCollection);
            LoadHRCollections();
        }


        private void btnNewHRequest_Click(object sender, EventArgs e)
        {
            string name = txtRequestFinder.Text;

            var requestNames = _currentCollection.Requests.Select(x => x.Name).ToList();

            if (name != String.Empty && !requestNames.Contains(name))
            {
                var newRequest = CreateNewRequest(name);

                if (newRequest != null)
                    AddNewRequestToCurrentCollection(newRequest);
            }
        }

        private void AddNewRequestToCurrentCollection(HRRequest newRequest)
        {
            _currentCollection.Requests.Add(newRequest);
            LoadHRRequests();
        }

        private HRRequest CreateNewRequest(string name)
        {
            HRRequest hrRequest = new HRRequest();
            hrRequest.Name = name;

            frmHREntityEdition frmHREE = new frmHREntityEdition(EntityType.Request);
            frmHREE.Text = name;

            var result = frmHREE.ShowDialog();
            if (result == DialogResult.OK)
            {
                hrRequest.Description = frmHREE.Entity.Description;
                hrRequest.Method = frmHREE.Entity.Method;
                hrRequest.Url = frmHREE.Entity.Url;
                return hrRequest;
            }

            return null;

        }

        private HRProject CreateNewProject(string name)
        {
            HRProject hrProject = new HRProject();
            hrProject.Name = name;

            frmHREntityEdition frmHREE = new frmHREntityEdition(EntityType.Project);
            frmHREE.Text = name;

            var result = frmHREE.ShowDialog();
            if (result == DialogResult.OK)
            {
                hrProject.Description = frmHREE.Entity.Description;
                return hrProject;
            }

            return null;
        }

        private HRCollection CreateNewCollection(string name)
        {
            HRCollection hrCollection = new HRCollection();
            hrCollection.Name = name;

            frmHREntityEdition frmHREE = new frmHREntityEdition(EntityType.Collection);
            frmHREE.Text = name;

            var result = frmHREE.ShowDialog();
            if (result == DialogResult.OK)
            {
                hrCollection.Description = frmHREE.Entity.Description;
                return hrCollection;
            }

            return null;
        }

        private void btnNewVariable_Click(object sender, EventArgs e)
        {
            string name = txtVariableFinder.Text;

            var variableNames = _currentRequest.Variables.Select(x => x.Name).ToList();

            if (name != String.Empty && !variableNames.Contains(name))
            {
                var newVariable = CreateNewVariable(name);

                if (newVariable != null)
                    AddNewVariableToCurrentRequest(newVariable);
            }
        }

        private void AddNewVariableToCurrentRequest(HRVariable newVariable)
        {
            _currentRequest.Variables.Add(newVariable);
            LoadHRVariables();
        }


        private HRVariable CreateNewVariable(string name)
        {
            HRVariable hrVariable = new HRVariable();
            hrVariable.Name = name;

            frmHREntityEdition frmHREE = new frmHREntityEdition(EntityType.Variable);
            frmHREE.Text = name;

            var result = frmHREE.ShowDialog();
            if (result == DialogResult.OK)
            {
                hrVariable.Description = frmHREE.Entity.Description;
                return hrVariable;
            }

            return null;
        }
    }
}
