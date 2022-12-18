using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace JSonManager.SavedHttpRequests
{
    public class HRVariable
    {
        public List<HRVariableValue> _values = new List<HRVariableValue>();

        public string Name { get; set; }

        public string Description { get; set; }

        public string Value 
        { 
            get 
            {
                if (_values.Count > 0)
                {
                    var value = _values.Where(x => x.IsCurrent == true);
                    if (value.Any())
                        return value.First().Value;
                }
                return string.Empty; 
            } 
        }

        public List<string> Values { get { return _values.Select(x => x.Value).ToList(); } }

        public void AddValue(string value, string description = "", bool isCurrent = true)
        {
            var val = _values.Find(x => x.Value == value);

            if (val != null) _values.Remove(val);

            if (isCurrent) foreach (var v in _values) { v.IsCurrent = false; }

            _values.Add(new HRVariableValue { Value = value, Description = description, IsCurrent = isCurrent });
        }


        public void AddValue(HRVariableValue newValue)
        {
            AddValue(newValue.Value, newValue.Description);
        }


        public void SetCurrentValue(string value)
        {
            string desc = string.Empty;

            var val = _values.Find(x => x.Value == value);

            if (val != null)
                desc = val.Description;

            AddValue(value, desc);
        }
    }


}
