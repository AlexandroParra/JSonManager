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
            
            EscrituraTreeView(treeView1.Nodes.Add(basicClasses[0].Name), basicClasses[0]);

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

        private static void EscrituraTreeView(TreeNode treeNode, BasicClass bClass)
        {
            //myTreeNode.Tag = bClass;            

            foreach (Property property in bClass.Properties)
                treeNode.Nodes.Add(property.ToString());

            foreach (BasicClass basicClass in bClass.insideClasses)                      
                EscrituraTreeView(treeNode.Nodes.Add(basicClass.Name), basicClass);
        }

        private static void EscribePropertiesList(TreeNode treeNode, List<Property> properties)
        {
            
        }

        
    }
}
