using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using TreeView = System.Windows.Forms.TreeView;

namespace JSonManager
{
    /// <summary>
    /// Clase que tiene como propiedad una estructura que representa a una clase de un lenguaje orientado a objetos y
    /// extendiende, a partir de esta, métodos que a través de controles de formulario, gestionan la información. 
    /// contiene
    /// </summary>
    internal class SBClassFormControls
    {
        private StringBaseClass _stringBaseClass;
        internal SBClassFormControls(StringBaseClass stringBaseClass)
        {
            _stringBaseClass = stringBaseClass;
        }

        internal void LoadTreeView(TreeView treeView)
        {
            treeView.Nodes.Clear();
            EscrituraTreeView(treeView.Nodes.Add(_stringBaseClass.Name), _stringBaseClass);
        }


        private static void EscrituraTreeView(TreeNode treeNode, StringBaseClass bClass)
        {
            treeNode.Tag = bClass;
            int contador = 0;

            foreach (StringBaseProperty property in bClass.Properties)
                treeNode.Nodes.Add(property.ToString());

            foreach (StringBaseClass basicClass in bClass.insideClasses)
            {
                string formattedName = basicClass.Name;
                if (formattedName == "{") { formattedName = bClass.Name + "[" + contador + "]"; }
                EscrituraTreeView(treeNode.Nodes.Add(formattedName), basicClass);
                contador++;
            }
        }
    }


}
