using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace JSonManager
{
    internal class StringBaseClass
    {
        public readonly StringBaseClass Parent;
        public string Name { get; private set; }
        public List<StringBaseProperty> Properties { get; set; }
        public List<StringBaseClass> insideClasses { get; set; }

        public StringBaseClass(string name, StringBaseClass parent)
        {
            Properties = new List<StringBaseProperty>();
            insideClasses = new List<StringBaseClass>();
            Name = name;
            Parent = parent;
        }

        public SBPropertyManager GetPropertiesManager()
        {
            return new SBPropertyManager(this);
        }

        public SBClassFormControls GetSBClassFormControls()
        {
            return new SBClassFormControls(this);
        }

    }




}
