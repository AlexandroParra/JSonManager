using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace JSonManager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            JSonReader jSonReader = new JSonReader(@"D:\Herramientas\C#\JSonManager\JSonManager\JSonFiles\ProductId.json");
            JSonNode nodo = jSonReader.Read();

            //EscribeFichero(nodo, @"D:\Herramientas\C#\JSonManager\JSonManager\JSonFiles\ContenidoNodo.txt");
            
            JSonOperator Joperator = new JSonOperator(nodo);
            List<BasicClass> basicClasses = Joperator.GetBasicClasses();

            treeView1 = EscrituraTreeView(treeView1, basicClasses[0]);

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
            sw.Write(string.Empty.PadLeft(tabulador,'\t') + nodo.Content);
            sw.Write("\r\n");

            if (nodo.Valor != null)               
                EscrituraRecursiva(nodo.Valor, sw, ++tabulador);

            if (nodo.HermanoMenor != null)
                EscrituraRecursiva(nodo.HermanoMenor, sw, miTabulador);
        }

        private static TreeView EscrituraTreeView(TreeView treeView, BasicClass bClass)
        {
            // Creamos un nodo con el nombre de la clase.
            TreeNode myTreeNode = new TreeNode(bClass.Name);
            //myTreeNode.Tag = bClass;
            treeView.Nodes.Add(myTreeNode);

            treeView.SelectedNode = myTreeNode;

            // Añadimos sus propiedades.
            treeView = EscribePropertiesList(treeView, bClass.Properties);

            foreach (BasicClass basicClass in bClass.insideClasses)
                treeView = EscrituraTreeView(treeView, basicClass);

            return treeView;
        }

        private static TreeView EscribePropertiesList(TreeView treeView, List<Property> properties)
        {
            foreach (Property property in properties)
                treeView.Nodes.Add(property.ToString());

            return treeView;
        }

        
    }
}
