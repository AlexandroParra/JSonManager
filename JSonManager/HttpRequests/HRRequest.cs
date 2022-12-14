using System.Collections.Generic;

namespace JSonManager.SavedHttpRequests
{
    public class HRRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Method { get; set; }
        public string Url { get; set; }
        public List<HRVariable> Variables { get; set; } = new List<HRVariable>();
    }
}