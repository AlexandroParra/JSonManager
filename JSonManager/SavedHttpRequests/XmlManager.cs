using System;
using System.Xml;

namespace JSonManager.SavedHttpRequests
{
    internal class XmlManager
    {
        XmlDocument doc = new XmlDocument();

        public XmlManager()
        {

            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement root = doc.DocumentElement;
            doc.InsertBefore(xmlDeclaration, root);
                XmlElement element1 = doc.CreateElement(string.Empty, "cuerpo", string.Empty);
            doc.AppendChild(element1);
                XmlElement element2 = doc.CreateElement(string.Empty, "nivel1", string.Empty);
            element1.AppendChild(element2);
                XmlElement element3 = doc.CreateElement(string.Empty, "nivel2", string.Empty);
            XmlText text1 = doc.CreateTextNode("texto");
            element3.AppendChild(text1);
                element2.AppendChild(element3);
                XmlElement element4 = doc.CreateElement(string.Empty, "nivel3", string.Empty);
            XmlText text2 = doc.CreateTextNode("más texto");
            element4.AppendChild(text2);
                element2.AppendChild(element4);
                doc.Save("C://ruta//xml_ejemplo.xml");
        }

    }
}
