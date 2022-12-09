using System;
using System.Xml;
using System.Collections.Generic;

namespace JSonManager.SavedHttpRequests
{
    internal class XmlManager
    {
        private string _file = Environment.CurrentDirectory + "//SavedHttpRequest.xml";


        public void Serialize<T>(T ob, string filePath)
        {
            System.Xml.Serialization.XmlSerializer writer =
                new System.Xml.Serialization.XmlSerializer(typeof(T));

            System.IO.FileStream file = System.IO.File.Create(filePath);

            writer.Serialize(file, ob);

            file.Close();
        }

        public T Deserialize<T>(string filePath)
        {
            System.Xml.Serialization.XmlSerializer reader =
                new System.Xml.Serialization.XmlSerializer(typeof(T));

            System.IO.StreamReader file = new System.IO.StreamReader(filePath);

            T obj = (T)reader.Deserialize(file);

            file.Close();

            return obj;
        }

        private List<HRProject> CreateTestProject()
        {
            List<HRProject> hrProjects = new List<HRProject>();

            /**************************************************************************************/

            HRProject hrProject = new HRProject();

            hrProject.Name = "Experticket";
            hrProject.Description = "Solicitudes de la API de Experticket";

            var hRCollection = new HRCollection () { Name = "Catalogo" };
            
            HRRequest hrRequest = new HRRequest();
            hrRequest.Name = "Basic Catalog";
            hrRequest.Method = "GET";
            hrRequest.Url = "{{baseUrl}}/catalog?PartnerId={{PartnerId}}";

            var hRVariable = new HRVariable();
            hRVariable.Name = "{{baseUrl}}";
            hRVariable.AddValue("https://pre-tixalia.api.experticket.com/api", "Entorno de PRE");
            hrRequest.Variables.Add(hRVariable);

            var hRVariable1 = new HRVariable();
            hRVariable1.Name = "{{PartnerId}}";
            hRVariable1.AddValue("973d5f4rygkc4", "Entorno de PRE");
            hrRequest.Variables.Add(hRVariable1);

            hRCollection.Requests.Add(hrRequest);

            hrProject.Collections.Add(hRCollection);

            hrProjects.Add(hrProject);


            /**************************************************************************************/

            HRProject hrProject1 = new HRProject();

            hrProject1.Name = "Proyecto de Pruebas";
            hrProject1.Description = "Para diferenciarlo de el de Experticket";

            hrProject1.Collections.Add(hRCollection);

            hrProjects.Add(hrProject1);

            return hrProjects;
        }

    }
}
