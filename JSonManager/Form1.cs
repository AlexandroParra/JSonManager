using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            JSonReader jSonReader = new JSonReader(@"C:\Users\laparra\Documents\JSones\ProductId.json");
            JSonNode nodo = jSonReader.Read();

            JSonOperator Joperator = new JSonOperator(nodo);
            List<BasicClass> basicClasses = Joperator.GetBasicClasses();


        }
    }
}
