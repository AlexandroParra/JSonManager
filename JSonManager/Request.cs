using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSonManager
{
    public class Request
    {
        public Uri Url { get; set; }
        public string Method { get; set; }
        public string Body { get; set; }
    }
}
