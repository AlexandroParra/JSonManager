using System.Collections.Generic;

namespace JSonManager.SavedHttpRequests
{
    public class HRCollection
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<HRRequest> Requests { get; set; }
    }
}