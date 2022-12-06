using System.Collections.Generic;

namespace JSonManager.SavedHttpRequests
{
    public class HRRequest
    {
        public string Method { get; set; }
        public string Url { get; set; }
        public List<string> Variables { get; set; }
    }
}