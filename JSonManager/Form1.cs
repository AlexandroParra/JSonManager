﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using TreeView = System.Windows.Forms.TreeView;

namespace JSonManager
{
    public partial class Form1 : Form
    {

        private OpenFileDialog openFileDialog1;
        public Form1()
        {
            InitializeComponent();

            #region openFileDialog1

            openFileDialog1 = new OpenFileDialog();

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


        private void EscribeFichero(JSonNode nodo, string nombreFichero)
        {

            int tabulador = 0;
            try
            {
                //Pass the filepath and filename to the StreamWriter Constructor
                StreamWriter sw = new StreamWriter(nombreFichero);
                EscrituraRecursiva(nodo, sw, tabulador);
                //Close the file
                sw.Close();
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

        private static void EscrituraRecursiva(JSonNode nodo, StreamWriter sw, int tabulador)
        {
            int miTabulador = tabulador;
            sw.Write(string.Empty.PadLeft(tabulador, '\t') + nodo.Content);
            sw.Write("\r\n");

            if (nodo.Valor != null)
                EscrituraRecursiva(nodo.Valor, sw, ++tabulador);

            if (nodo.HermanoMenor != null)
                EscrituraRecursiva(nodo.HermanoMenor, sw, miTabulador);
        }

        private static void EscrituraTreeView(TreeNode treeNode, BasicClass bClass)
        {
            treeNode.Tag = bClass;

            foreach (Property property in bClass.Properties)
                treeNode.Nodes.Add(property.ToString());

            foreach (BasicClass basicClass in bClass.insideClasses)
                EscrituraTreeView(treeNode.Nodes.Add(basicClass.Name), basicClass);
        }


        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode tn = (sender as TreeView).SelectedNode;
            if (tn.Tag != null)
            {
                LoadPropertiesList((BasicClass)tn.Tag);
                LoadPropertiesTextBoxes((BasicClass)tn.Tag);
            }
        }

        private void LoadPropertiesList(BasicClass basicClass)
        {
            lstProperties.Items.Clear();
            foreach (Property property in basicClass.GetPropertiesSub())
                lstProperties.Items.Add(property.Name);
        }

        private void LoadPropertiesTextBoxes(BasicClass basicClass)
        {
            txtProperties.Clear();
            txtEnclosuredProperties.Clear();
            txtProperties.Text = String.Join(",", basicClass.GetPropertiesSub().Select(x => x.Name).ToArray());
            txtEnclosuredProperties.Text = txtPrefix.Text + txtProperties.Text + txtSufix.Text;
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
                        LecturaFichero();
                    }

                }
                catch (SecurityException ex)
                {
                    MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" +
                    $"Details:\n\n{ex.StackTrace}");
                }
            }
        }


        private void LecturaFichero()
        {
            if (txtFile.Text != String.Empty)
            {
                JSonReader jSonReader = new JSonReader(txtFile.Text);
                JSonNode nodo = jSonReader.Read();

                //EscribeFichero(nodo, @"D:\Herramientas\C#\JSonManager\JSonManager\JSonFiles\ContenidoNodo.txt");

                JSonOperator Joperator = new JSonOperator(nodo);
                List<BasicClass> basicClasses = Joperator.GetBasicClasses();

                treeView1.Nodes.Clear();
                EscrituraTreeView(treeView1.Nodes.Add(basicClasses[0].Name), basicClasses[0]);
            }
        }
    }
}
