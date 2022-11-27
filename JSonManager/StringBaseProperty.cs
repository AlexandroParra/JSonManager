using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSonManager
{
    internal class StringBaseProperty
    {
        public readonly StringBaseClass Parent;
        public string Name { get; set; }
        public string Value { get; set; }
        public Type Type { get; set; }
        public string Separator { get; set; }

        public StringBaseProperty(StringBaseClass parent, string name, string value, Type type, string separator)
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
}
