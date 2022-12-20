﻿using JSonManager.HttpRequests;
using JSonManager.SavedHttpRequests;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;
using static System.Windows.Forms.AxHost;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using File = System.IO.File;
using TreeView = System.Windows.Forms.TreeView;

namespace JSonManager
{
    public partial class frmMain : Form
    {
        private enum APIResponseType { JSON, XML }

        private const string MSG_SAVED_HTTP_REQUESTS_FILE_NO_EXIST = "Configuration file {File} not found.";
        private const string MSG_OFFER_NEW_HTTP_REQUESTS_FILE = "Do you want continue creating a new one? (file {File} will be created)";
        private const string TITLE_SAVED_HTTP_REQUESTS_FILE_NO_EXIST = "Error Accessing Configuration File";

        private OpenFileDialog openFileDialog1;

        private JSonReader jSonReader;

        public frmMain()
        {
            InitializeComponent();

            #region openFileDialog1

            openFileDialog1 = new OpenFileDialog();

            #endregion

            #region Asignación de los controles a la estructura de clases que soporta el comportamiento a nivel de formulario.

            //********************************************************************************************
            //
            // El objeto treeView1 será la fuente de datos para el objeto que gestiona las propiedades.
            // El código de este formulario deberá contener la siguiente instrucción:
            //
            //          .SBClassFormControls().LoadTreeView(treeView1);
            //
            //********************************************************************************************

            _ = new PropertiesFormControls(treeView1, lstProperties, txtEnclosuredProperties, txtPrefix, txtSufix, txtSeparator);

            #endregion

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void LeeFichero(JSonNode nodo, string nombreFichero)
        {
            String line;
            try
            {

                //Pass the file path and file name to the StreamReader constructor
                StreamReader sr = new StreamReader(nombreFichero);
                //Read the first line of text
                line = sr.ReadLine();
                //Continue to read until you reach end of file
                while (line != null)
                {
                    //write the line to console window
                    Console.WriteLine(line);
                    //Read the next line
                    line = sr.ReadLine();
                }
                //close the file
                sr.Close();
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }
        }


        private void btnCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(txtEnclosuredProperties.Text);
        }


        private void btnSearchFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    txtFile.Clear();

                    foreach (string file in openFileDialog1.FileNames)
                    {
                        txtFile.Text = file;
                        ReadFile(txtFile.Text);
                    }
                }
                catch (SecurityException ex)
                {
                    MessageBox.Show($"Error reading file.\n\nError message: {ex.Message}\n\n" +
                    $"Details:\n\n{ex.StackTrace}");
                }
            }
        }


        private void btnRequest_Click(object sender, EventArgs e)
        {
            if (txtUrl.Text == string.Empty)
                ManagesSavedHttpRequests();
            else
                ManagesHttpRequests();
        }

        private void ManagesSavedHttpRequests()
        {
            var hRSavedProjects = CreateHttpRequestsSavedProjects();

            if (hRSavedProjects.IsEnabled)
                OpenSavedHttpRequestsForm(hRSavedProjects);
        }

        private HttpRequestsSavedProjects CreateHttpRequestsSavedProjects()
        {
            var hRSavedProjects = new HttpRequestsSavedProjects(LoadLocationSavedHttpRequestsFileFromConfiguration());

            if (hRSavedProjects.SavedFileExists())
                hRSavedProjects.LoadProjects();
            else
            {
                MsgSavedHttpRequestsFileNoExist(hRSavedProjects.SavedHRFilePath);
                var resp = MsgOfferingNewHttpRequestsFile(hRSavedProjects.SavedHRFilePath);
                if (resp == DialogResult.Yes)
                    hRSavedProjects.InitializeProjects();
            }
            return hRSavedProjects;
        }

        private DialogResult MsgSavedHttpRequestsFileNoExist(string savedHttpRequestsFilePath)
        {
            var msg = MSG_SAVED_HTTP_REQUESTS_FILE_NO_EXIST.Replace("{File}", savedHttpRequestsFilePath);
            var title = TITLE_SAVED_HTTP_REQUESTS_FILE_NO_EXIST;
            return MessageBox.Show(msg, title, MessageBoxButtons.OK);
        }

        private DialogResult MsgOfferingNewHttpRequestsFile(string savedHttpRequestsFilePath)
        {
            var msg = MSG_OFFER_NEW_HTTP_REQUESTS_FILE.Replace("{File}", savedHttpRequestsFilePath);
            var title = TITLE_SAVED_HTTP_REQUESTS_FILE_NO_EXIST;
            return MessageBox.Show(msg, title, MessageBoxButtons.YesNo);
        }

        private string LoadLocationSavedHttpRequestsFileFromConfiguration() 
        {
            return Environment.CurrentDirectory + "\\" + ConfigurationManager.AppSettings.Get("SavedProjectsFileName");                        
        }

        private void OpenSavedHttpRequestsForm(HttpRequestsSavedProjects httpRequestsSavedProjects)
        {
            using (var frm = new frmSavedHttpRequests(httpRequestsSavedProjects))
            {
                var result = frm.ShowDialog();
                if (result == DialogResult.OK)
                    txtUrl.Text = frm.HttpRequestSelected;
            }
        }

        private async void RequestAPI(Uri url)
        {
            string dato = await ApiConnector.Connect(url);
            ShowFormattedContent(dato);
        }


        private void ShowFormattedContent(string content)
        {
            APIResponseType responseType = GetAPIResponseType(content);
            switch(responseType)
            {
                case APIResponseType.JSON:
                    ShowJSONResponse(content);
                    break;
                case APIResponseType.XML:
                    ShowXMLResponse(content);
                    break;
            }
        }

        private void ShowJSONResponse(string response)
        {
            jSonReader = new JSonReader(response);
            LoadTreeViewFromJSonData(jSonReader);
        }

        private void ShowXMLResponse(string response)
        {
            // TODO: Gestionar la respuesta XML
            jSonReader = new JSonReader(response);
            LoadTreeViewFromJSonData(jSonReader);
        }

        private APIResponseType GetAPIResponseType(string content)
        {
            // TODO: Hacer la comprobación del tipo de la respuesta.
            return APIResponseType.XML;
        }

        private void ReadFile(string filePath)
        {
            if (filePath != String.Empty)
            {
                jSonReader = new JSonReader(new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete));
                LoadTreeViewFromJSonData(jSonReader);
            }
        }


        private void LoadTreeViewFromJSonData(JSonReader jSonReader)
        {
            JSonReader.GetJSonOperator(jSonReader).SBClassFormControls().LoadTreeView(treeView1);
        }

        private void ManagesHttpRequests()
        {
            Uri uriResult;
            bool result = Uri.TryCreate(txtUrl.Text, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            if (result)
                RequestAPI(uriResult);
            else
                MessageBox.Show("No se pudo validar la Url como una petición HTTP.");
        }
    }
}
