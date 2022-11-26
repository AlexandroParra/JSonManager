using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSonManager
{
    /// <summary>
    /// Esta clase será la encargada de realizar las llamadas a la clase StringBaseClass y
    /// almacenar los valores que devuelva. 
    /// La idea es que sea esta clase la que alimente al formulario y evite que haya código
    /// de la gestión del árbol en él.
    /// </summary>
    public class SBPropertyManager
    {
        StringBaseClass _sbc;
        public SBPropertyManager(StringBaseClass SBC)
        {
            _sbc = SBC;
        }

        public List<StringBaseProperty> GetPropertiesSub()
        {
            return GetPropertiesSub(_sbc, new List<StringBaseProperty>());
        }

        public List<StringBaseProperty> GetPropertiesTop()
        {
            return GetPropertiesTop( _sbc, new List<StringBaseProperty>());
        }

        public List<string> GetPropertyNamesSub()
        {
            return GetPropertiesSub().Select(x => x.Name).ToList();
        }

        public List<string> GetPropertyNamesTop()
        {
            return GetPropertiesTop().Select(x => x.Name).ToList();
        }

        public List<string> GetPropertyValuesSub()
        {
            return GetPropertiesSub().Select(x => x.Value).ToList();
        }

        public List<string> GetPropertyValuesTop()
        {
            return GetPropertiesTop().Select(x => x.Value).ToList();
        }



        private List<StringBaseProperty> GetPropertiesSub(StringBaseClass basicClass, List<StringBaseProperty> properties)
        {
            properties.AddRange(basicClass.Properties);

            foreach (StringBaseClass basicClassInside in basicClass.insideClasses)
                GetPropertiesSub(basicClassInside, properties);

            return properties;
        }

        private List<StringBaseProperty> GetPropertiesTop(StringBaseClass stringBaseClass, List<StringBaseProperty> properties)
        {
            properties.AddRange(stringBaseClass.Properties);

            GetPropertiesTop(stringBaseClass.Parent, properties);

            return properties;
        }
    }
}
