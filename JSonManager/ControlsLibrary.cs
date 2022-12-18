using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JSonManager
{
    public static class ControlsLibrary
    {
        public delegate void funcEachRow<T>(T item);

        public static void TextBoxClean(TextBox textBox)
        {
            textBox.Text = String.Empty;
        }

        public static void ListBoxLoad<T>(ListBox listBox, List<T> listOfItems)
        {            
            listBox.Items.Clear();
            foreach (T item in listOfItems) listBox.Items.Add(item);
        }
    }
}
