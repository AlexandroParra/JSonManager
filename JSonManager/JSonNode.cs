using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace JSonManager
{

    public class JSonNode
    {

        public enum eNodeType
        {
            NoDefined,
            Value,
            Property,
            Array,
            Object,
            EndOfArray,
            EndOfObject
        }
        
        public eNodeType NodeType { get; set; }

        private StringBuilder sbContent;

        private int separatorPosition;

        public string Content { get { return sbContent.ToString().TrimEnd().TrimStart(); } }

        public JSonNode Valor   { get; set; }

        public JSonNode HermanoMenor { get; set; }

        [Obsolete("This property is deprecated.",false)]
        public bool IsValidated { get; set; }

        public bool Contains(string value)
        {
            return sbContent.ToString().Contains(value);
        }

        public string LiteralName()
        {
            separatorPosition = sbContent.ToString().IndexOf(':');                        
            return (separatorPosition > -1)?sbContent.ToString().Substring(0,separatorPosition): sbContent.ToString();
        }

        public string LiteralValue()
        {
            return sbContent.ToString().Substring(separatorPosition + 1);
        }

        public eNodeType GetNodeType()
        {
            if (Contains(":"))
            {
                string[] splittedContent = sbContent.ToString().Split(':');
                if (splittedContent.Length > 1)
                {
                    if (splittedContent[1] == "{")
                        return eNodeType.Object;
                    if (splittedContent[1] == "[")
                        return eNodeType.Array;
                    else
                        return eNodeType.Property;
                }
            }
            else
            {
                if (Contains("}"))
                    return eNodeType.EndOfObject;
                if (Contains("]"))
                    return eNodeType.EndOfArray;
                else
                    return eNodeType.Value;
            }
            return eNodeType.NoDefined;
        }

        public JSonNode()
        {
            sbContent = new StringBuilder();
            IsValidated = false;
        }

        public void Append(char newChar)
        {
            sbContent.Append(newChar);            
        }
    }
}
