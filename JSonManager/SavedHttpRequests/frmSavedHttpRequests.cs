using JSonManager.HttpRequests;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
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

        private HttpRequestsSavedProjects _savedProjects;


        public frmSavedHttpRequests(HttpRequestsSavedProjects httpRequestsSavedProjects)
        {
            InitializeComponent();
            SetControlsConfiguration();
            //SaveProjects(XmlManager.CreateTestProject(), _locationProjectsFile);
            LoadDataForm(httpRequestsSavedProjects);
        }

        private void LoadDataForm(HttpRequestsSavedProjects httpRequestsSavedProjects)
        {
            _savedProjects = httpRequestsSavedProjects;
            _projects = _savedProjects.HttpRequestsProjects;
            if (_projects != null && _projects.Count > 0) { LoadHRProjects(); }
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

        private void SetControlsConfiguration()
        {
            lstHRProjects.ValueMember = "Name";
            lstHRProjects.DisplayMember = "Name";

            lstHRCollections.ValueMember = "Name";
            lstHRCollections.DisplayMember = "Name";

            lstHRRequests.ValueMember = "Url";
            lstHRRequests.DisplayMember = "Url";

            lstHRVariables.ValueMember = "Name";
            lstHRVariables.DisplayMember = "Name";

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
            ControlsLibrary.ListBoxLoad(lstHRProjects, _projects);
        }

        private void LoadHRCollections()
        {
            ControlsLibrary.ListBoxLoad(lstHRCollections, _currentProject.Collections);
        }

        private void LoadHRRequests()
        {
            ControlsLibrary.ListBoxLoad(lstHRRequests, _currentCollection.Requests);
        }

        private void LoadHRVariables()
        {
            ControlsLibrary.ListBoxLoad(lstHRVariables, _currentRequest.Variables);
        }

        private void LoadHRVariableValue()
        {
            dataGridVariables.Rows.Clear();
            foreach (var value in _currentVariable._values)            
                dataGridVariables.Rows.Add(value.Value, value.Description, value.IsCurrent );            
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

        private void lstHRVariables_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(lstHRVariables.SelectedItems.Count > 0)
            {
                _currentVariable = lstHRVariables.SelectedItem as HRVariable;
                LoadHRVariableValue();
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
            if (_projects.Count > 0) _savedProjects.SaveProjects();
        }


        private void btnNewProject_Click(object sender, EventArgs e)
        {
            string name = txtProjectFinder.Text;

            var projectNames = _projects.Select(x => x.Name).ToList();

            if (name != String.Empty && !projectNames.Contains(name))
            {
                var newProject = CreateNewProject(name);

                if (newProject != null)
                {
                    AddNewProjectToListOfProjects(newProject);
                }
            }                
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


        private void btnNewHRequest_Click(object sender, EventArgs e)
        {
            string name = txtRequestFinder.Text;

            var requestNames = _currentCollection.Requests.Select(x => x.Name).ToList();

            if (name != String.Empty && !requestNames.Contains(name))
            {
                var newRequest = CreateNewRequest(name);

                if (newRequest != null)
                {
                    AddNewRequestToCurrentCollection(newRequest);
                }
            }
        }


        private void btnNewValue_Click(object sender, EventArgs e)
        {
            string value = txtValueFinder.Text;

            var values = _currentVariable.Values;

            if(value != string.Empty && !values.Contains(value))
            {
                var newValue = CreateNewValue(value);

                if (newValue != null)
                {
                    AddNewValueToCurrentVariable(newValue);
                }
            }
        }


        private void btnNewVariable_Click(object sender, EventArgs e)
        {
            string name = txtVariableFinder.Text;

            var variableNames = _currentRequest.Variables.Select(x => x.Name).ToList();

            if (name != String.Empty && !variableNames.Contains(name))
            {
                var newVariable = CreateNewVariable(name);

                if (newVariable != null)
                {
                    AddNewVariableToCurrentRequest(newVariable);
                }
            }
        }


        private void AddNewProjectToListOfProjects(HRProject newProject)
        {
            _projects.Add(newProject);
            LoadHRProjects();            
            ControlsLibrary.TextBoxClean(txtProjectFinder);
        }


        private void AddNewValueToCurrentVariable(HRVariableValue newValue)
        {
            _currentVariable.AddValue(newValue);
            LoadHRVariableValue();            
            ControlsLibrary.TextBoxClean(txtValueFinder);
            DecodeRequest();
        }


        private void AddNewCollectionToCurrentProject(HRCollection newCollection)
        {
            _currentProject.Collections.Add(newCollection);
            LoadHRCollections();
            ControlsLibrary.TextBoxClean(txtCollectionFinder);
        }


        private void AddNewRequestToCurrentCollection(HRRequest newRequest)
        {
            _currentCollection.Requests.Add(newRequest);
            LoadHRRequests();
            ControlsLibrary.TextBoxClean(txtRequestFinder);
        }


        private void AddNewVariableToCurrentRequest(HRVariable newVariable)
        {
            _currentRequest.Variables.Add(newVariable);
            LoadHRVariables();
            ControlsLibrary.TextBoxClean(txtVariableFinder);
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


        private HRVariableValue CreateNewValue(string value)
        {
            HRVariableValue hrVariableValue = new HRVariableValue();
            hrVariableValue.Value = value;

            frmHREntityEdition frmHREE = new frmHREntityEdition(EntityType.Value);
            frmHREE.Name = value;

            var result = frmHREE.ShowDialog();
            if (result == DialogResult.OK)
            {
                hrVariableValue.Description = frmHREE.Entity.Description;
                return hrVariableValue;
            }

            return null;
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

        private void dataGridVariables_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (_currentRequest != null)
            {
                _currentVariable._values = GetValuesFromDataGridView(dataGridVariables);
                DecodeRequest();
            }            
        }

        private void dataGridVariables_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            if (_currentRequest != null)
            {
                _currentVariable._values = GetValuesFromDataGridView(dataGridVariables);
                DecodeRequest();
            }
        }

        private List<HRVariableValue> GetValuesFromDataGridView( DataGridView dgv)
        {
            var listOfValues = new List<HRVariableValue>();
            foreach (DataGridViewRow dgvRow in dgv.Rows)
            {
                var valor = new HRVariableValue();
                valor.Value = dgvRow.Cells[0].Value.ToString();
                valor.Description = dgvRow.Cells[1].Value.ToString();
                valor.IsCurrent = (bool)dgvRow.Cells[2].Value;
                listOfValues.Add(valor);
            }
            return listOfValues;
        }


    }
}
