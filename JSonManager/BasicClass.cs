using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace JSonManager
{
    public class Property
    {
        public readonly BasicClass Parent; 
        public string Name { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }
        public string Separator { get; set; }

        public Property(BasicClass parent, string name, string value, string type, string separator)
        {
            Parent = parent;
            Name = name;
            Value = value;
            Type = type;
            Separator = separator;
        }

        public override string ToString()
        {
            return Name + Separator + Value;
        }
    }


    public class BasicClass
    {
        public readonly BasicClass Parent;
        public string Name { get; private set; }
        public List<Property> Properties { get; set; }
        public List<BasicClass> insideClasses { get; set; }

        public BasicClass(string name, BasicClass parent)
        {
            Properties = new List<Property>();
            insideClasses = new List<BasicClass>();
            Name = name;
            Parent = parent;
        }

    }


}
