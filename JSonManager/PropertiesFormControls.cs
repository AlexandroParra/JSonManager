using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JSonManager
{
    internal class PropertiesFormControls
    {
        public ListBox _listOfProperties;
        public TextBox _txtEnclosuredProperties;
        public TextBox _txtPrefix;
        public TextBox _txtSuffix;
        public TextBox _txtSeparator;
        public TreeView _treeView;
        private StringBaseClass _BaseClass;

        public PropertiesFormControls( TreeView treeView, ListBox listBoxOfProperties, TextBox txtEnclosureProperties, TextBox txtPrefix, TextBox txtSuffix, TextBox txtSeparator)
        {
            _listOfProperties = listBoxOfProperties;
            _txtEnclosuredProperties = txtEnclosureProperties;
            _txtPrefix = txtPrefix;
            _txtSuffix = txtSuffix;
            _txtSeparator = txtSeparator;
            _treeView = treeView;

            #region Capturando los eventos

            _treeView.AfterSelect += _treeView_AfterSelect;
            _txtPrefix.TextChanged += _txtPrefix_TextChanged;
            _txtSuffix.TextChanged += _txtSuffix_TextChanged;
            _txtSeparator.TextChanged += _txtSeparator_TextChanged;

            #endregion
        }

        private void _treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode tn = (sender as TreeView).SelectedNode;
            if (tn.Tag != null)
                LoadProperties((StringBaseClass)tn.Tag);
            else
                ClearPropertiesControls();

        }


        private void ClearPropertiesControls()
        {
            _listOfProperties.Items.Clear();
            //txtProperties.Clear();
            _txtEnclosuredProperties.Clear();
        }

        private void _txtSeparator_TextChanged(object sender, EventArgs e)
        {
            LoadPropertiesTextBoxes();
        }

        private void _txtSuffix_TextChanged(object sender, EventArgs e)
        {
            LoadPropertiesTextBoxes();
        }

        private void _txtPrefix_TextChanged(object sender, EventArgs e)
        {
            LoadPropertiesTextBoxes();
        }

        internal void LoadProperties(StringBaseClass sbc)
        {
            _BaseClass = sbc;
            LoadPropertiesList();
            LoadPropertiesTextBoxes();
        }

        private void LoadPropertiesList()
        {
            _listOfProperties.Items.Clear();
            foreach (var item in _BaseClass.GetPropertiesManager().GetPropertyNamesSub())
                _listOfProperties.Items.Add(item);
        }


        private void LoadPropertiesTextBoxes()
        {
            string splittedProperties = String.Join(_txtSeparator.Text, _BaseClass.GetPropertiesManager().GetPropertyNamesSub().ToArray());
            _txtEnclosuredProperties.Text = _txtPrefix.Text + splittedProperties + _txtSuffix.Text;
        }
    }
}
