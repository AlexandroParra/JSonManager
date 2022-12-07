using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace JSonManager.SavedHttpRequests
{
    /// <summary>
    /// Http Requests Project
    ///     Será el contenedor principal (o más superficial de las peticiones http a gestionar.
    /// </summary>
    public class HRProject
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<HRCollection> Collections { get; set; } = new List<HRCollection>();
    }
}
