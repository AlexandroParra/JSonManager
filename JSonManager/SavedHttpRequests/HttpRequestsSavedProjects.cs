using System.IO;
using JSonManager.SavedHttpRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;
using File = System.IO.File;

namespace JSonManager.HttpRequests
{
    public class HttpRequestsSavedProjects
    {
        private XmlManager _xmlManager = new XmlManager();

        public string SavedHRFilePath { get; set; }

        public List<HRProject> HttpRequestsProjects { get; set; }

        public bool IsEnabled { get { return HttpRequestsProjects != null; } }

        public HttpRequestsSavedProjects(string locationSavedFile) { SavedHRFilePath = locationSavedFile; }

        public bool SavedFileExists() { return File.Exists(SavedHRFilePath);  }

        public void InitializeProjects() { HttpRequestsProjects = new List<HRProject>();}

        public void LoadProjects() { HttpRequestsProjects = _xmlManager.Deserialize<List<HRProject>>(SavedHRFilePath); }

        public void SaveProjects() { _xmlManager.Serialize<List<HRProject>>(HttpRequestsProjects, SavedHRFilePath); }
        

    }
}
